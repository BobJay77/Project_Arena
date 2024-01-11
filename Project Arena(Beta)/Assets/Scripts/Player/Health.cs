using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviour
{
    public int health;
    public TMP_Text healthText;

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        healthText.text = "Health: " + health.ToString();

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
