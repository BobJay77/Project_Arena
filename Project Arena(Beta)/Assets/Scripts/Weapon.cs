using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;

    public float fireRate;

    public Camera camera;

    public GameObject hitVFX;

    private float nextFire;

    private void Update()
    {
        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }

        if (Input.GetMouseButton((int)MouseButton.Left) && nextFire <= 0) 
        {
            nextFire = 1 / fireRate;

            Fire();
        }
        
    }

    //This is where the game system would watch and check the numbers
    private void Fire()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);

            if(hit.transform.gameObject.GetComponent<Health>()) 
            {
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }
}
