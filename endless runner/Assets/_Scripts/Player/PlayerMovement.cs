using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [Space]
    [SerializeField] private float initialForwardSpeed;
    [SerializeField] private float increaseForwardSpeed;
    private float forwardSpeed;
    private float forwardMovement;
    [SerializeField] private float initialStrafeSpeed;
    [SerializeField] private float increaseStrafeSpeed;
    private float strafeSpeed;
    private float strafeMovement;
    [Space]
    [SerializeField] private float jumpForce;

    [SerializeField] private float initialGravity;
    [SerializeField] private float increaseGravityAmount;
    private float gravity;
    private float verticalMovement; // velocity
    private Vector3 move;
    private bool isGrounded = false;
    [Space]
    [SerializeField] private Transform groundCheckPositionForward;
    [SerializeField] private Transform groundCheckPositionBackward;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    [Space]
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;


    private void Awake()
    {
        forwardSpeed = initialForwardSpeed;
        strafeSpeed = initialStrafeSpeed;
        gravity = initialGravity;
        
    }

    void Update()
    {
        strafeMovement = Input.GetAxis("Horizontal") * strafeSpeed;
        forwardMovement = forwardSpeed;
        verticalMovement += gravity * Time.deltaTime;

        isGrounded = GroundCheck();
        if(isGrounded && verticalMovement < 0)
        {
            verticalMovement = 0;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalMovement = jumpForce;
        }

        move = new Vector3(strafeMovement, verticalMovement, forwardMovement);
        characterController.Move(move * Time.deltaTime);
    }

    private bool GroundCheck()
    {
        if (Physics.Raycast(groundCheckPositionForward.position, Vector3.down, groundCheckDistance, whatIsGround))
        {
            Debug.DrawLine(groundCheckPositionForward.position, new Vector3(0, -groundCheckDistance), Color.yellow, 2);
            return true;
        }
        if (Physics.Raycast(groundCheckPositionBackward.position, Vector3.down, groundCheckDistance, whatIsGround))
        {
            Debug.DrawLine(groundCheckPositionBackward.position, new Vector3(0, -groundCheckDistance), Color.red, 2);
            return true;
        }
        return false;
    }

    private void IncreaseSpeed()
    {
        strafeSpeed += increaseStrafeSpeed;
        forwardSpeed += increaseForwardSpeed;
        gravity += increaseGravityAmount;
    }
}
