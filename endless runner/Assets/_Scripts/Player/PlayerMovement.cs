using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    //[SerializeField] private Animator animator;
    [Space]
    [SerializeField] private float initialForwardSpeed;
    [SerializeField] private float increaseForwardSpeed;
    private float forwardSpeed;
    private float forwardMovement;
    [SerializeField] private float initialStrafeSpeed;
    [SerializeField] private float initialAirStrafeSpeed;
    [SerializeField] private float increaseStrafeSpeed;
    private float strafeSpeed;
    private float airStrafeSpeed;
    private float strafeMovement;
    [Space]
    [SerializeField] private float jumpForce;

    [SerializeField] private float initialGravity;
    [SerializeField] private float increaseGravityAmount;
    private float gravity;
    private float verticalMovement; // velocity
    private Vector3 move;
    private Vector3 playerPosition;
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
        airStrafeSpeed = initialAirStrafeSpeed;
        gravity = initialGravity;
        
    }

    void Update()
    {
        isGrounded = GroundCheck();
        
        forwardMovement = forwardSpeed;

        if (isGrounded)
            strafeMovement = Input.GetAxis("Horizontal") * strafeSpeed;
        else
            strafeMovement = Input.GetAxis("Horizontal") * airStrafeSpeed;

        verticalMovement += gravity * Time.deltaTime;

        if(isGrounded && verticalMovement < 0)
        {
            verticalMovement = 0;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalMovement = jumpForce;
        }

        move = new Vector3(strafeMovement, verticalMovement, forwardMovement);
        //move.x = Mathf.Clamp(move.x, -3, 3);
        characterController.Move(move * Time.deltaTime);
        //transform.position = Mathf.Clamp()
    }

    private bool GroundCheck()
    {
        if (Physics.Raycast(groundCheckPositionForward.position, Vector3.down, groundCheckDistance, whatIsGround))
        {
            //Debug.DrawLine(groundCheckPositionForward.position, new Vector3(0, -groundCheckDistance), Color.yellow, 2);
            return true;
        }
        if (Physics.Raycast(groundCheckPositionBackward.position, Vector3.down, groundCheckDistance, whatIsGround))
        {
            //Debug.DrawLine(groundCheckPositionBackward.position, new Vector3(0, -groundCheckDistance), Color.red, 2);
            return true;
        }
        return false;
    }

    private bool BoundaryCheck()
    {
        return transform.position.x <= rightBoundary.position.x && transform.position.x >= leftBoundary.position.x;
        
    }
    private bool LeftBoundaryCheck()
    {
        return transform.position.x == rightBoundary.position.x;
    }

    private void IncreaseSpeed()
    {
        strafeSpeed += increaseStrafeSpeed;
        airStrafeSpeed += increaseStrafeSpeed;
        forwardSpeed += increaseForwardSpeed;
        gravity += increaseGravityAmount;
    }
}
