using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerStats : MonoBehaviour
{
    public bool useOldInput_ = false;
    public GameObject cube_;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _sprintSpeed = 14f;

    private FPSPlayerHealth _playerHealth;
    private FPSPlayerMovement _playerMovement;
    private FPSPlayerInputNew _playerInputNew;

    private bool _isLocalPlayer;

    private Ray _rayOfPreviousBullet = new Ray();

    private Camera _camera;

    #region Properties
    public int _MaxHealth
    {
        get { return _maxHealth; }
    }

    public float _WalkSpeed
    {
        get { return _walkSpeed; }
    }

    public float _SprintSpeed
    {
        get { return _sprintSpeed; }
    }

    public Camera _Camera
    {
        get { return _camera; }
    }

    public FPSPlayerInputNew _PlayerInputNew
    {
        get { return _playerInputNew; }
    }

    public bool _IsLocalPlayer
    {
        get { return _isLocalPlayer; }
    }

    #endregion

    private void Awake()
    {
        _playerHealth = GetComponent<FPSPlayerHealth>();
        _playerMovement = GetComponent<FPSPlayerMovement>();
        _playerInputNew = GetComponent<FPSPlayerInputNew>();

        _camera = GetComponentInChildren<Camera>();

        _isLocalPlayer = false;

        _playerMovement.enabled = false;
        _camera.gameObject.SetActive(false);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void IsLocalPlayer()
    {
        _camera.gameObject.SetActive(true);
        _playerMovement.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        cube_.SetActive(false);

        _isLocalPlayer = true;
    }

    [PunRPC]
    public void GotHit(int damage, float posX, float posY, float posZ, float dirX, float dirY, float dirZ) 
    {
        Vector3 positionOfShooter = new Vector3(posX, posY, posZ);
        Vector3 directionOfShooter = new Vector3(dirX, dirY, dirZ).normalized;

        _rayOfPreviousBullet = new Ray(positionOfShooter, directionOfShooter);

        _playerHealth.TakeDamage(damage, _rayOfPreviousBullet);
    }
}
