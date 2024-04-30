using UnityEngine;
using TMPro;

public class FPSPlayerHealth : MonoBehaviour
{
    private int _health;
    [SerializeField] private TMP_Text _healthText = null;
    private FPSPlayerStats _playerStats => GetComponent<FPSPlayerStats>();
    public int _Health
    {
        get { return _health; }
    } 

    public void TakeDamage(int damage, Ray rayOfPreviousBullet)
    {
        _health -= damage;

        if (_healthText != null) 
        _healthText.text = "Health: " + _health.ToString();

        if (_health <= 0)
        {
            if (_playerStats._IsLocalPlayer)
                RoomManager.instance.SpawnPlayer();

            //Application.Quit();
            //watch death here.
            Destroy(gameObject);
        }
    }
}
