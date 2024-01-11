using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float sprintSpeed = 14f;
    public float maxVelocityChange = 10f;

    public float jumpHeight = 3f;

    public float airControl = 0.5f;

    private Vector2 input;
    private Rigidbody rb;

    private bool sprinting;
    private bool jumping;

    private bool grounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Do general cleanup for this code
    private void FixedUpdate()
    {
        if(grounded)
        {
            if (jumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            }

            else if (input.magnitude > 0.5f)
            {
                rb.AddForce(CalculateMovement(sprinting ? sprintSpeed : walkSpeed), ForceMode.VelocityChange);
            }

            else
            {
                var newVelocity = rb.velocity;
                newVelocity = new Vector3(newVelocity.x * 0.2f * Time.deltaTime,
                                          newVelocity.y,
                                          newVelocity.x * 0.2f * Time.deltaTime);

                rb.velocity = newVelocity;
            }
        }

        else
        {
            if (input.magnitude > 0.5f)
            {
                rb.AddForce(CalculateMovement(sprinting ? sprintSpeed * airControl : walkSpeed * airControl), ForceMode.VelocityChange);
            }

            else
            {
                var newVelocity = rb.velocity;
                newVelocity = new Vector3(newVelocity.x * 0.2f * Time.deltaTime,
                                          newVelocity.y,
                                          newVelocity.x * 0.2f * Time.deltaTime);

                rb.velocity = newVelocity;
            }
        }

        grounded = false;
    }

    private void Update()
    {
        //Change this to new input system in the future
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();

        sprinting = Input.GetKey(KeyCode.LeftShift);
        jumping = Input.GetKey(KeyCode.Space);
    }


    private Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= _speed;

        Vector3 velocity = rb.velocity;

        if (input.magnitude > 0.5)
        {
            Vector3 velocityChange = targetVelocity - velocity;
            
            velocityChange.x = Mathf.Clamp(velocityChange.x, - maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

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
