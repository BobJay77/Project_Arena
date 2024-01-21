using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public bool tapInput = false;

    public int damage;

    public float shotsPerSecond;

    public GameObject hitVFX;

    //Change this
    [Header("Ammo")]
    public int mag_ = 5;
    public int ammo_ = 30;
    public int magAmmo_ = 30;

    [Header("UI")]
    [SerializeField] private TMP_Text _magText;
    [SerializeField] private TMP_Text _ammoText;

    [Header("Animation")]
    private Animation _animation;
    [SerializeField] private AnimationClip _reload;

    //rework needed on recoil
    [Header("Recoil")]
    //[Range(0, 1)]
    //public float recoilPercentage = .3f;
    [Range(0, 2)]
    public float recoverPercentage = .7f;
    public float recoilUp = 1f;
    public float recoilBack = 0f;

    private float _recoilLength;
    private float _recoverLength;
    private Vector3 _originalPosition;
    private Vector3 _recoilVelocity = Vector3.zero;

    private bool _recoiling;
    private bool _recovering;


    private float _timeBetweenShots;
    private FPSPlayerStats _playerStats;
    private bool _useTapInput => tapInput ? _playerStats._PlayerInputNew.FireInputTap : _playerStats._PlayerInputNew.FireInput;
    private bool _reloading => _playerStats._PlayerInputNew.ReloadInput;

    private void Awake()
    {
        _playerStats = GetComponentInParent<FPSPlayerStats>();
        _animation = GetComponent<Animation>();

        _originalPosition = transform.localPosition;

        _recoilLength = 0;
        _recoverLength = 1 / shotsPerSecond * recoverPercentage;

        UpdateAmmoAndMagText();
    }

    private void Update()
    {
        if (_timeBetweenShots > 0)
        {
            _timeBetweenShots -= Time.deltaTime;
        }

        if (_useTapInput && _timeBetweenShots <= 0 && ammo_ > 0 && !_animation.isPlaying) 
        {
            _timeBetweenShots = 1 / shotsPerSecond;

            ammo_--;

            UpdateAmmoAndMagText();

            Fire();
        }

        if (_reloading)
        {
            Reload();
        }

        if(_recoiling)
        {
            Recoil();
        }

        if(_recovering)
        {
            Recover();
        }

    }

    private void Fire()
    {
        _recoiling = true;
        _recovering = false;

        Ray ray = new Ray(_playerStats._Camera.transform.position, _playerStats._Camera.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);

            if(hit.transform.gameObject.GetComponent<FPSPlayerHealth>()) 
            {
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("GotHit", RpcTarget.All, damage, ray.origin.x, 
                                                                                                         ray.origin.y,
                                                                                                         ray.origin.z,
                                                                                                         ray.direction.x,
                                                                                                         ray.direction.y,
                                                                                                         ray.direction.z);            }
        }
    }

    //change code to make it better 
    private void Reload()
    {
        if (mag_ > 0 && ammo_ < magAmmo_) 
        {
            _animation.Play(_reload.name);

            mag_--;
            ammo_ = magAmmo_;
        }

        UpdateAmmoAndMagText();
    }

    void Recoil()
    {
        Vector3 finalPosition = new Vector3(_originalPosition.x, _originalPosition.y + recoilUp, _originalPosition.z - recoilBack);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref _recoilVelocity, _recoilLength);

        if (transform.localPosition == finalPosition)
        {
            _recoiling = false;
            _recovering = true;
        }
    }

    void Recover()
    {
        Vector3 finalPosition = _originalPosition;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref _recoilVelocity, _recoverLength);

        if (transform.localPosition == finalPosition)
        {
            _recoiling = false;
            _recovering = false;
        }
    }

    private void UpdateAmmoAndMagText()
    {
        _magText.text = "Mag: " + mag_.ToString();
        _ammoText.text = "Ammo" + ammo_.ToString() + "/" + magAmmo_.ToString();
    }
}
