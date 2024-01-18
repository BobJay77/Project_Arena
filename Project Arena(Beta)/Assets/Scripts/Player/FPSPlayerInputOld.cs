using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerInputOld : MonoBehaviour
{
    public const string jumpListKey = "Jump";
    public const string sprintListKey = "Sprint";
    public const string crouchListKey = "Crouch";
    public const string pAbilityListKey = "PAbility";
    public const string sAbilityListKey = "SAbility";
    public const string tAbilityListKey = "TAbility";
    public const string uAbilityListKey = "UAbility";
    public const string reloadListKey = "Reload";
    public const string dropListKey = "Drop";
    public const string interactListKey = "Interact";
    public const string closeGameListKey = "Close";

    private bool _changeKeysInitiated = false;
    private string _changeListKey = string.Empty;
    private Dictionary<string, KeyCode> _inputKeysList = new Dictionary<string, KeyCode>
    {
        {jumpListKey, KeyCode.Space},
        {sprintListKey, KeyCode.LeftShift},
        {crouchListKey, KeyCode.LeftControl},
        {pAbilityListKey, KeyCode.E},
        {sAbilityListKey, KeyCode.Q},
        {tAbilityListKey, KeyCode.C},
        {uAbilityListKey, KeyCode.X},
        {reloadListKey, KeyCode.R},
        {dropListKey, KeyCode.G},
        {interactListKey, KeyCode.F},
        {closeGameListKey, KeyCode.Escape }
    };


    #region Properties
    public KeyCode _JumpingKey
    {
        get { return _inputKeysList[jumpListKey]; }
    }

    public KeyCode _SprintingKey
    {
        get { return _inputKeysList[sprintListKey]; }
    }

    public KeyCode _CrouchingKey
    {
        get { return _inputKeysList[crouchListKey]; }
    }

    public KeyCode _PrimaryAbilityKey
    {
        get { return _inputKeysList[pAbilityListKey]; }
    }

    public KeyCode _SecondaryAbilityKey
    {
        get { return _inputKeysList[sAbilityListKey]; }
    }

    public KeyCode _TertiaryAbilityKey
    {
        get { return _inputKeysList[tAbilityListKey]; }
    }

    public KeyCode _UltimateAbilityKey
    {
        get { return _inputKeysList[uAbilityListKey]; }
    }
    public KeyCode _ReloadWeaponKey
    {
        get { return _inputKeysList[reloadListKey]; }
    }

    public KeyCode _DropItemKey
    {
        get { return _inputKeysList[dropListKey]; }
    }

    public KeyCode _InteractItemKey
    {
        get { return _inputKeysList[interactListKey]; }
    }
    #endregion

    private void Update()
    {
        if (Input.GetKey(_inputKeysList[closeGameListKey]))
        {
            Application.Quit();
        }

        if(_changeKeysInitiated)
        {
            foreach(KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if(Input.GetKey(keycode))
                {
                    if (!_inputKeysList.ContainsValue(keycode))
                    {
                        ChangeKey(_changeListKey, keycode);
                    }
                }
               
            }
        }
    }
    public void InitiateChangeKeys(string key)
    {
        if (_inputKeysList.ContainsKey(key)) 
        {
            _changeKeysInitiated = true;
            _changeListKey = key;
        }
    }
    private void ChangeKey(string listKey, KeyCode newKey)
    {
        //Swap values if they are equal in the future
        if (!_inputKeysList.ContainsValue(newKey))
        {
            _inputKeysList[listKey] = newKey;
        }

        _changeKeysInitiated = false;
        _changeListKey = string.Empty;
    }

}
