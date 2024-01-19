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

    private float _timeBetweenShots;
    private FPSPlayerStats _playerStats;
    private bool _useTapInput => tapInput ? _playerStats._PlayerInputNew.FireInputTap : _playerStats._PlayerInputNew.FireInput;
    private bool _reloading => _playerStats._PlayerInputNew.ReloadInput;

    private void Awake()
    {
        _playerStats = GetComponentInParent<FPSPlayerStats>();
        _animation = GetComponent<Animation>();

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

    }

    private void Fire()
    {
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

    private void UpdateAmmoAndMagText()
    {
        _magText.text = "Mag: " + mag_.ToString();
        _ammoText.text = "Ammo" + ammo_.ToString() + "/" + magAmmo_.ToString();
    }
}
