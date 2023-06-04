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
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPositionForward;
    [SerializeField] private Transform groundCheckPositionBackward;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    [Space]
    [Header("Slow Down Power Up")]
    [SerializeField] private float slowDownSpeedChange = -1f;
    [SerializeField] private float slowDownGravityChange = 2f;


    public delegate void OnPlayerFallTooFast();
    public static OnPlayerFallTooFast onPlayerFallTooFast;

    private void Awake()
    {
        forwardSpeed = initialForwardSpeed;
        strafeSpeed = initialStrafeSpeed;
        airStrafeSpeed = initialAirStrafeSpeed;
        gravity = initialGravity;

        LevelGenerator1.onLevelPartSpawned += IncreaseSpeed;
        PlayerInteractions.onSlowDownPickUp += SlowDownSpeed;
        
    }
    private void OnDestroy()
    {
        LevelGenerator1.onLevelPartSpawned -= IncreaseSpeed;
        PlayerInteractions.onSlowDownPickUp -= SlowDownSpeed;
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
        public float forwardSpeed, strafeSpeed, airStrafeSpeed, gravity, lethalGravityValue;
    }
    private SaveSpeedBeforeSlowDown savedSpeeds;
    private void SlowDownSpeed()
    {
        if (GameManager.Instance.SlowDownOn)
            return;

        StopSlowDownTransition();

        savedSpeeds = new SaveSpeedBeforeSlowDown
        {
            forwardSpeed = forwardSpeed,
            strafeSpeed = strafeSpeed,
            airStrafeSpeed = airStrafeSpeed,
            gravity = gravity,
            lethalGravityValue = lethalGravityValue
        };

        forwardSpeed += slowDownSpeedChange;
        strafeSpeed += slowDownSpeedChange;
        airStrafeSpeed += slowDownSpeedChange;
        gravity += slowDownGravityChange;
        lethalGravityValue += slowDownGravityChange;
    }
    private void StopSlowDownTransition()
    {
        StopCoroutine(nameof(TransitionSpeedDown));
        forwardSpeed = savedSpeeds.forwardSpeed;
        strafeSpeed = savedSpeeds.strafeSpeed;
        airStrafeSpeed = savedSpeeds.airStrafeSpeed;
        gravity = savedSpeeds.gravity;
        lethalGravityValue = savedSpeeds.lethalGravityValue;
    }

    private void BackToNormalSpeed()
    {
        StartCoroutine(TransitionSpeedDown(1));
        //forwardSpeed = savedSpeeds.forwardSpeed;
        //strafeSpeed = savedSpeeds.strafeSpeed;
        //airStrafeSpeed = savedSpeeds.airStrafeSpeed;
        //gravity = savedSpeeds.gravity;
        //lethalGravityValue = savedSpeeds.lethalGravityValue;
    }
    IEnumerator TransitionSpeedDown(float duration)
    {
        float timeElapsed = 0;
        float t;
        float[] targetSpeeds = { savedSpeeds.forwardSpeed, savedSpeeds.strafeSpeed, savedSpeeds.airStrafeSpeed, savedSpeeds.gravity, savedSpeeds.lethalGravityValue};
        float[] oldSpeeds = { forwardSpeed, strafeSpeed, airStrafeSpeed, gravity, lethalGravityValue};
        while (timeElapsed < duration)
        {
            
            t = timeElapsed / duration;

            forwardSpeed = Mathf.Lerp(oldSpeeds[0], targetSpeeds[0], t); // might give error if oldSpeed > targetSpeed
            strafeSpeed = Mathf.Lerp(oldSpeeds[1], targetSpeeds[1], t);
            airStrafeSpeed = Mathf.Lerp (oldSpeeds[2], targetSpeeds[2], t);
            gravity = Mathf.Lerp(oldSpeeds[3], targetSpeeds[3], t);
            lethalGravityValue = Mathf.Lerp(oldSpeeds[4], targetSpeeds[4], t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        forwardSpeed = targetSpeeds[0];
        strafeSpeed = targetSpeeds[1];
        airStrafeSpeed = targetSpeeds[2];
        gravity = targetSpeeds[3];
        lethalGravityValue = targetSpeeds[4];
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
            Debug.Log("Max speed reached");
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
}
