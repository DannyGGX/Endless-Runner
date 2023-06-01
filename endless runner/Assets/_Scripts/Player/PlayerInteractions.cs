using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    private float verticalVelocity;
    private float previousVerticalVelocity;
    [SerializeField] Animator animator;
    [Space]
    [SerializeField] private Transform face;
    [SerializeField] private Transform lowerBodyPos;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float rayCastLength = 0.15f;

    private void OnEnable()
    {
        PlayerMovement.onPlayerFallTooFast += KillPlayer;
    }

    private void OnDisable()
    {
        PlayerMovement.onPlayerFallTooFast -= KillPlayer;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.PauseToggle();
        }

    }

    private float CalcVerticalVelocity()
    {
        return 0;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.gameObject.CompareTag("Deadly") || CheckForFacePlant())
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        GameManager.Instance.PlayerDie();
        animator.SetTrigger("Die");
    }

    private bool CheckForFacePlant()
    {
        if (Physics.Raycast(lowerBodyPos.position, Vector3.forward, rayCastLength, whatIsGround))
            return true;
        if (Physics.Raycast(face.position, Vector3.forward, rayCastLength, whatIsGround))
            return true;
        return false;
    }

}
