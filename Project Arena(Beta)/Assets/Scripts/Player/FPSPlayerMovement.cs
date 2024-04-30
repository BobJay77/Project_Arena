using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerMovement : MonoBehaviour
{
    public float maxVelocityChange_ = 10f;

    public float jumpHeight_ = 3f;

    public float airControl_ = 0.5f;

    private bool sprinting => _playerStats.useOldInput_ ? Input.GetKey(_playerInputOld._SprintingKey) : _playerInputNew._SprintInput;
    private bool jumping => _playerStats.useOldInput_ ? Input.GetKey(_playerInputOld._JumpingKey) : _playerInputNew._JumpInput;

    private bool grounded = false;

    private Rigidbody _rb;
    private FPSPlayerStats _playerStats;
    private FPSPlayerInputOld _playerInputOld;
    private FPSPlayerInputNew _playerInputNew;


    private void Start()
    {
        _playerStats = GetComponent<FPSPlayerStats>();
        _playerInputOld = GetComponent<FPSPlayerInputOld>();
        _playerInputNew = GetComponent<FPSPlayerInputNew>();    
        _rb = GetComponent<Rigidbody>();
    }

    //Do general cleanup for this code
    private void FixedUpdate()
    {
        if(grounded)
        {
            if (jumping)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, jumpHeight_, _rb.velocity.z);
            }

            else if (_playerInputNew._MovementInput.magnitude > 0.5f)
            {
                _rb.AddForce(CalculateMovement(sprinting ? _playerStats._SprintSpeed : _playerStats._WalkSpeed), ForceMode.VelocityChange);
            }

            else
            {
                Vector3 newVelocity = _rb.velocity;
                newVelocity = new Vector3(newVelocity.x * 0.2f * Time.deltaTime,
                                          newVelocity.y,
                                          newVelocity.x * 0.2f * Time.deltaTime);

                _rb.velocity = newVelocity;
            }
        }

        else
        {
            if (_playerInputNew._MovementInput.magnitude > 0.5f)
            {
                _rb.AddForce(CalculateMovement(sprinting ? _playerStats._SprintSpeed * airControl_ : _playerStats._WalkSpeed * airControl_), ForceMode.VelocityChange);
            }

            else
            {
                Vector3 newVelocity = _rb.velocity;
                newVelocity = new Vector3(newVelocity.x * 0.2f * Time.deltaTime,
                                          newVelocity.y,
                                          newVelocity.x * 0.2f * Time.deltaTime);

                _rb.velocity = newVelocity;
            }
        }

        grounded = false;
    }

    private Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new Vector3(_playerInputNew._MovementInput.x, 0, _playerInputNew._MovementInput.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= _speed;

        Vector3 velocity = _rb.velocity;

        if (_playerInputNew._MovementInput.magnitude > 0.5)
        {
            Vector3 velocityChange = targetVelocity - velocity;
            
            velocityChange.x = Mathf.Clamp(velocityChange.x, - maxVelocityChange_, maxVelocityChange_);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange_, maxVelocityChange_);

            velocityChange.y = 0;

            return velocityChange;
        }

        else
        {
            return Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        grounded = true;
    }
}
