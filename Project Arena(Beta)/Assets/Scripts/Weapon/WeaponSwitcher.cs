using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    private FPSPlayerStats _playerStats;

    private int _selectedWeapon = 0;
    [SerializeField] private Animation _animation;
    [SerializeField] private AnimationClip _drawClip;

    #region Properties

    private bool _PrimaryWeaponSelectInput => _playerStats.useOldInput_ ? Input.GetKeyDown(KeyCode.Alpha1) : _playerStats._PlayerInputNew._PrimaryWeaponSelectInput;
    private bool _SecondaryWeaponSelectInput => _playerStats.useOldInput_ ? Input.GetKeyDown(KeyCode.Alpha2) : _playerStats._PlayerInputNew._SecondaryWeaponSelectInput;
    private bool _TertiaryWeaponSelectInput => _playerStats.useOldInput_ ? Input.GetKeyDown(KeyCode.Alpha3) : _playerStats._PlayerInputNew._TertiaryWeaponSelectInput;
    private bool _OtherWeaponSelectInput => _playerStats.useOldInput_ ? Input.GetKeyDown(KeyCode.Alpha4) : _playerStats._PlayerInputNew._OtherWeaponSelectInput;
    private float _ScrollInput => _playerStats.useOldInput_ ? Input.GetAxis("Mouse ScrollWheel") : _playerStats._PlayerInputNew._ScrollWheelInput;

    #endregion

    private void Start()
    {
        _playerStats = GetComponentInParent<FPSPlayerStats>();
        _animation = GetComponent<Animation>();
        _drawClip = _animation.clip;
        SelectWeapon();
    }

    //Change this to new input
    private void Update()
    {
        int previousSelectedWeapon = _selectedWeapon;

        if (_PrimaryWeaponSelectInput) 
        {
            _selectedWeapon = 0;
        }

        else if (_SecondaryWeaponSelectInput)
        {
            _selectedWeapon = 1;
        }

        else if (_TertiaryWeaponSelectInput)
        {
            _selectedWeapon = 2;
        }

        else if (_OtherWeaponSelectInput)
        {
            _selectedWeapon = 3;
        }

        if(_ScrollInput > 0)
        {
            if(_selectedWeapon >= transform.childCount -1) 
            {
                _selectedWeapon = 0;
            }

            else
            {
                _selectedWeapon++;
            }
        }

        else if(_ScrollInput < 0)
        {
            if (_selectedWeapon <= 0)
            {
                _selectedWeapon = transform.childCount - 1;
            }

            else
            {
                _selectedWeapon--;
            }
        }

        if (previousSelectedWeapon != _selectedWeapon)
        {
            SelectWeapon();
        }

    }

    private void SelectWeapon()
    {

        if(_selectedWeapon >= transform.childCount)
        {
            _selectedWeapon = transform.childCount - 1;
        }

        _animation.Stop();
        _animation.Play(_drawClip.name);

        int i = 0;

        foreach(Transform weapon in transform)
        {
            if (i == _selectedWeapon) 
            {
                weapon.gameObject.SetActive(true);
            }

            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
