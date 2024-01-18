using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool tapInput = false;

    public int damage;

    public float shotsPerSecond;

    public GameObject hitVFX;

    private float _timeBetweenShots;
    private FPSPlayerStats _playerStats;
    private bool _useTapInput => tapInput ? _playerStats._PlayerInputNew.FireInputTap : _playerStats._PlayerInputNew.FireInput;

    private void Awake()
    {
        _playerStats = GetComponentInParent<FPSPlayerStats>();
    }

    private void Update()
    {
        if (_timeBetweenShots > 0)
        {
            _timeBetweenShots -= Time.deltaTime;
        }

        if (_useTapInput && _timeBetweenShots <= 0) 
        {
            _timeBetweenShots = 1 / shotsPerSecond;

            Fire();
        }
        
    }

    //This is where the game system would watch and check the numbers
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
}
