using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    [Space]
    [SerializeField] private float initialForwardSpeed;
    [SerializeField] private float initialStrafeSpeed;
    [SerializeField] private float initialAirStrafeSpeed;
    [SerializeField] private float initialGravity;
    [Space]
    [SerializeField] private float increaseForwardSpeed;
    [SerializeField] private float increaseStrafeSpeed;
    [SerializeField] private float increaseGravityAmount;
    [SerializeField] private float maxForwardSpeed = 15;
    private float forwardSpeed;
    private float strafeSpeed;
    private float airStrafeSpeed;
    private float forwardMovement;
    private float strafeMovement;
    private float verticalMovement; // velocity
    private float gravity;
    private Vector3 move;
    [Space]
    [SerializeField] private float jumpForce;
    [SerializeField] private float lethalGravityValue = -25;
    //private Vector3 playerPosition;
    private bool isGrounded = false;
    [Space]
    [SerializeField] private Transform groundCheckPositionForward;
    [SerializeField] private Transform groundCheckPositionBackward;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    [Space]
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;

    public delegate void OnPlayerFallTooFast();
    public static OnPlayerFallTooFast onPlayerFallTooFast;

    private void Awake()
    {
        forwardSpeed = initialForwardSpeed;
        strafeSpeed = initialStrafeSpeed;
        airStrafeSpeed = initialAirStrafeSpeed;
        gravity = initialGravity;

        LevelGenerator1.onLevelPartSpawned += IncreaseSpeed;
        
    }
    private void OnDestroy()
    {
        LevelGenerator1.onLevelPartSpawned -= IncreaseSpeed;
    }

    void Update()
    {
        if (GameManager.Instance.controlsEnabled == false)
            return;

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
        else if (verticalMovement < lethalGravityValue)
        {
            onPlayerFallTooFast?.Invoke(); // die event to PlayerInteractions
        }

        if(Input.GetButton("Jump") && isGrounded)
        {
            verticalMovement = jumpForce;
            animator.SetTrigger("Jump");
        }

        move = new Vector3(strafeMovement, verticalMovement, forwardMovement);
        characterController.Move(move * Time.deltaTime);
        //transform.position = Mathf.Clamp()
    }

    //private void FixedUpdate()
    //{
    //    SetAnimatorState();
    //}

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

    private struct SaveSpeedBeforeSlowDown
    {
        public float forwardSpeed, strafeSpeed, airStrafeSpeed, gravity;
    }
    SaveSpeedBeforeSlowDown savedSpeeds;
    private void SlowDownSpeed()
    {
        savedSpeeds = new SaveSpeedBeforeSlowDown();
        savedSpeeds.forwardSpeed = forwardSpeed;
        savedSpeeds.strafeSpeed = strafeSpeed;
        savedSpeeds.airStrafeSpeed = airStrafeSpeed;
        savedSpeeds.gravity = gravity;

        // TODO: Change speeds
    }

    private void BackToNormalSpeed()
    {
        forwardSpeed = savedSpeeds.forwardSpeed;
        strafeSpeed = savedSpeeds.strafeSpeed;
        airStrafeSpeed = savedSpeeds.airStrafeSpeed;
        gravity = savedSpeeds.gravity;
    }

    private void IncreaseSpeed()
    {
        if (GameManager.Instance.SlowDownOn)
            return;

        float previousForwardSpeed = forwardSpeed;
        forwardSpeed += increaseForwardSpeed;
        if(forwardSpeed > maxForwardSpeed)
        {
            forwardSpeed = previousForwardSpeed;
            return;
        }
        strafeSpeed += increaseStrafeSpeed;
        airStrafeSpeed += increaseStrafeSpeed;
        gravity += increaseGravityAmount;
        lethalGravityValue += increaseGravityAmount;
    }

    private void SetAnimatorState()
    {
        if(isGrounded)
        {

        }
        else
        {
            if(verticalMovement > 0)
            {
                
            }
            else
            {

            }
        }
    }
    private bool BoundaryCheck()
    {
        return transform.position.x <= rightBoundary.position.x && transform.position.x >= leftBoundary.position.x;
        
    }
    private bool LeftBoundaryCheck()
    {
        return transform.position.x == rightBoundary.position.x;
    }
}
