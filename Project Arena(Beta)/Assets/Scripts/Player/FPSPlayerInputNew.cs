using Photon.Pun.Demo.SlotRacer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerInputNew : MonoBehaviour
{
    private PlayerControls _playerControls;

    [Header("Camera change")]
    private bool _viewInput = false;

    [Header("Camera Movement Input")]
    private Vector2 _cameraInput;
    private float _cameraVerticalInput;
    private float _cameraHorizontalInput;

    [Header("Movement Input")]
    private Vector2 _movementInput;
    private float _verticalInput;
    private float _horizontalInput;
    private float _moveAmount;

    [Header("Player Action Input")]
    private bool _sprintInput = false;
    private bool _fireInput = false;
    private bool _altFireInput = false;
    private bool _reloadInput = false;
    private bool _jumpInput = false;
    private bool _crouchInput = false;
    private bool _dropInput = false;
    private bool _interactInput = false;

    [Header("Player UI Input")]
    private bool _invetoryInput = false;
    private bool _mapInput = false;
    private bool _pauseInput = false;
    private bool _leaderBoardInput = false;

    [Header("Player Ability Input")]
    private bool _primaryAbiliyInput = false;
    private bool _secondaryAbiliyInput = false;
    private bool _tertiaryAbiliyInput = false;
    private bool _ultimateAbiliyInput = false;

    [Header("Player Weapon Select Input")]
    private bool _primaryWeaponSelectInput = false;
    private bool _secondaryWeaponSelectInput = false;
    private bool _tertiaryWeaponSelectInput = false;
    private bool _otherWeaponSelectInput = false;
    [SerializeField] private Vector2 _scrollWheelInput;

    #region Properties

    public Vector2 _MovementInput
    {
        get { return _movementInput; }
    }

    public Vector2 _CameraInput
    {
        get { return _cameraInput; }
    }

    public float _MoveAmout
    {
        get { return _moveAmount; }
    }

    public bool _SprintInput
    {
        get { return _sprintInput; }
    }

    public bool _FireInput
    {
        get { return _fireInput; }
    }

    public bool _FireInputTap
    {
        get { return _playerControls.PlayerActions.Fire.WasPressedThisFrame(); }
    }

    public bool _AltFireInput
    {
        get { return _altFireInput; }
    }

    public bool _AltFireInputTap
    {
        get { return _playerControls.PlayerActions.AltFire.WasPressedThisFrame(); }
    }

    public bool _ReloadInput
    {
        get { return _reloadInput; }
    }

    public bool _JumpInput
    {
        get { return _jumpInput; }
    }

    public bool _CrouchInput
    {
        get { return _crouchInput; }
    }

    public bool _DropInput
    {
        get { return _dropInput; }
    }

    public bool _InteractInput
    {
        get { return _interactInput; }
    }

    public bool _LeaderBoardInput
    {
        get { return _leaderBoardInput; }
    }

    public bool _PrimaryAbiltyInput
    {
        get { return _primaryAbiliyInput; }
    }

    public bool _SecondaryAbiltyInput
    {
        get { return _secondaryAbiliyInput; }
    }

    public bool _TertiaryAbiltyInput
    {
        get { return _tertiaryAbiliyInput; }
    }

    public bool _UltimateAbiltyInput
    {
        get { return _ultimateAbiliyInput; }
    }

    public bool _PrimaryWeaponSelectInput
    {
        get { return _primaryWeaponSelectInput; }
    }

    public bool _SecondaryWeaponSelectInput
    {
        get { return _secondaryWeaponSelectInput; }
    }

    public bool _TertiaryWeaponSelectInput
    {
        get { return _tertiaryWeaponSelectInput; }
    }

    public bool _OtherWeaponSelectInput
    {
        get { return _otherWeaponSelectInput; }
    }

    public float _ScrollWheelInput
    {
        get { return _scrollWheelInput.y; }
    }

    #endregion

    private void OnEnable()
    {
        Application.targetFrameRate = 60;

        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();

            #region PlayerMovement

            _playerControls.PlayerMovement.Movement.performed += i => _movementInput = i.ReadValue<Vector2>();
            _playerControls.PlayerCamera.Movement.performed += i => _cameraInput = i.ReadValue<Vector2>();

            #endregion

            #region PlayerActions

            _playerControls.PlayerActions.Sprint.performed += i => _sprintInput = true;
            _playerControls.PlayerActions.Sprint.canceled += i => _sprintInput = false;

            _playerControls.PlayerActions.Fire.performed += i => _fireInput = true;
            _playerControls.PlayerActions.Fire.canceled += i => _fireInput = false;

            _playerControls.PlayerActions.AltFire.performed += i => _altFireInput = true;
            _playerControls.PlayerActions.AltFire.canceled += i => _altFireInput = false;

            _playerControls.PlayerActions.Reload.performed += i => _reloadInput = true;
            _playerControls.PlayerActions.Reload.canceled += i => _reloadInput = false;

            _playerControls.PlayerActions.Jump.performed += i => _jumpInput = true;
            _playerControls.PlayerActions.Jump.canceled += i => _jumpInput = false;

            _playerControls.PlayerActions.Crouch.performed += i => _crouchInput = true;
            _playerControls.PlayerActions.Crouch.canceled += i => _crouchInput = false;

            _playerControls.PlayerActions.Drop.performed += i => _dropInput = true;
            _playerControls.PlayerActions.Drop.canceled += i => _dropInput = false;

            _playerControls.PlayerActions.Interact.performed += i => _interactInput = true;
            _playerControls.PlayerActions.Interact.canceled += i => _interactInput = false;

            #endregion

            #region PlayerCamera

            _playerControls.PlayerCamera.View.performed += i => _viewInput = true;
            _playerControls.PlayerCamera.View.canceled += i => _viewInput = false;

            #endregion

            #region PlayerUI

            _playerControls.PlayerUI.LeaderBoard.performed += i => _leaderBoardInput = true;
            _playerControls.PlayerUI.LeaderBoard.canceled += i => _leaderBoardInput = false;

            //playerControls.PlayerUI.Inventory.performed += i => HandleInventory();

            //playerControls.PlayerUI.Map.performed += i => HandleMap();

            //playerControls.PlayerUI.Pause.performed += i => HandlePause();

            #endregion

            #region PlayerAbility

            _playerControls.PlayerAbilities.Primary.performed += i => _primaryAbiliyInput = true;
            _playerControls.PlayerAbilities.Primary.canceled += i => _primaryAbiliyInput = false;

            _playerControls.PlayerAbilities.Secondary.performed += i => _secondaryAbiliyInput = true;
            _playerControls.PlayerAbilities.Secondary.canceled += i => _secondaryAbiliyInput = false;

            _playerControls.PlayerAbilities.Tertiary.performed += i => _tertiaryAbiliyInput = true;
            _playerControls.PlayerAbilities.Tertiary.canceled += i => _tertiaryAbiliyInput = false;

            _playerControls.PlayerAbilities.Ultimate.performed += i => _ultimateAbiliyInput = true;
            _playerControls.PlayerAbilities.Ultimate.canceled += i => _ultimateAbiliyInput = false;

            #endregion

            #region PlayerWeaponSelect

            _playerControls.PlayerWeaponSelect.Primary.performed += i => _primaryWeaponSelectInput = true;
            _playerControls.PlayerWeaponSelect.Primary.canceled += i => _primaryWeaponSelectInput = false;

            _playerControls.PlayerWeaponSelect.Secondary.performed += i => _secondaryWeaponSelectInput = true;
            _playerControls.PlayerWeaponSelect.Secondary.canceled += i => _secondaryWeaponSelectInput = false;

            _playerControls.PlayerWeaponSelect.Tertiary.performed += i => _tertiaryWeaponSelectInput = true;
            _playerControls.PlayerWeaponSelect.Tertiary.canceled += i => _tertiaryWeaponSelectInput = false;

            _playerControls.PlayerWeaponSelect.Other.performed += i => _otherWeaponSelectInput = true;
            _playerControls.PlayerWeaponSelect.Other.canceled += i => _otherWeaponSelectInput = false;

            _playerControls.PlayerWeaponSelect.ScrollWheel.performed += i => _scrollWheelInput = i.ReadValue<Vector2>();

            #endregion
        }
        _playerControls.Enable();
    }



    private void OnApplicationFocus(bool focus)
    {
        if (enabled)
        {
            _playerControls.Enable();
        }
        else
        {
            _playerControls.Disable();
        }
    }

    private void Update()
    {
        HandleAllInputs();
    }

    private void HandleAllInputs()
    {
        HandlePlayerMovementInput();
        HandleCameraMovementInput();
    }

    private void HandlePlayerMovementInput()
    {
        _horizontalInput = _movementInput.x;
        _verticalInput = _movementInput.y;

        _moveAmount = Mathf.Clamp01(Mathf.Abs(_verticalInput) + Mathf.Abs(_horizontalInput));

        if (_moveAmount <= 0.5 && _moveAmount > 0)
        {
            _moveAmount = 0.5f;
        }

        else if (_moveAmount > 0.5 && _moveAmount <= 1)
        {
            _moveAmount = 1;
        }
    }

    private void HandleCameraMovementInput()
    {
        _cameraHorizontalInput = _cameraInput.x;
        _cameraVerticalInput = _cameraInput.y;
    }
}
