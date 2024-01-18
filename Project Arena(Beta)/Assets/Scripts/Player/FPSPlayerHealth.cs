using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class FPSPlayerHealth : MonoBehaviour
{
    private int _health;
    private TMP_Text _healthText = null;

    public void TakeDamage(int damage, Ray rayOfPreviousBullet)
    {
        _health -= damage;

        if (_healthText != null) 
        _healthText.text = "Health: " + _health.ToString();

        if (_health <= 0)
        {
            Application.Quit();
            //watch death here.
            Destroy(gameObject);
        }
    }

    public void SetPlayerText()
    {
        _healthText = GetComponentInChildren<TMP_Text>();
    }
}
