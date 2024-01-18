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
    [SerializeField] private Vector2 _cameraInput;
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

    #region Properties

    public Vector2 MovementInput
    {
        get { return _movementInput; }
    }

    public Vector2 CameraInput
    {
        get { return _cameraInput; }
    }

    public float MoveAmout
    {
        get { return _moveAmount; }
    }

    public bool SprintInput
    {
        get { return _sprintInput; }
    }

    public bool FireInput
    {
        get { return _fireInput; }
    }

    public bool FireInputTap
    {
        get { return _playerControls.PlayerActions.Fire.WasPressedThisFrame(); }
    }

    public bool AltFireInput
    {
        get { return _altFireInput; }
    }

    public bool AltFireInputTap
    {
        get { return _playerControls.PlayerActions.AltFire.WasPressedThisFrame(); }
    }

    public bool ReloadInput
    {
        get { return _reloadInput; }
    }

    public bool JumpInput
    {
        get { return _jumpInput; }
    }

    public bool CrouchInput
    {
        get { return _crouchInput; }
    }

    public bool DropInput
    {
        get { return _dropInput; }
    }

    public bool InteractInput
    {
        get { return _interactInput; }
    }

    #endregion

    private void OnEnable()
    {
        Application.targetFrameRate = 60;

        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();

            _playerControls.PlayerMovement.Movement.performed += i => _movementInput = i.ReadValue<Vector2>();
            _playerControls.PlayerCamera.Movement.performed += i => _cameraInput = i.ReadValue<Vector2>().normalized;

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

            _playerControls.PlayerCamera.View.performed += i => _viewInput = true;
            _playerControls.PlayerCamera.View.canceled += i => _viewInput = false;

            //playerControls.PlayerUI.Inventory.performed += i => HandleInventory();

            //playerControls.PlayerUI.Map.performed += i => HandleMap();

            //playerControls.PlayerUI.Pause.performed += i => HandlePause();
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
