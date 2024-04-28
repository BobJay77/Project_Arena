using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    private int _selectedWeapon = 0;
    [SerializeField] private Animation _animation;
    [SerializeField] private AnimationClip _drawClip;

    private void Start()
    {
        _animation = GetComponent<Animation>();
        _drawClip = _animation.clip;
        SelectWeapon();
    }

    //Change this to new input
    private void Update()
    {
        int previousSelectedWeapon = _selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            _selectedWeapon = 0;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _selectedWeapon = 1;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _selectedWeapon = 2;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _selectedWeapon = 3;
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
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

        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
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
