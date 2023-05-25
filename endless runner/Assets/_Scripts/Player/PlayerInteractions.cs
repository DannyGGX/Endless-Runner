using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    [SerializeField] private Transform face;
    [SerializeField] private Transform lowerBodyPos;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float rayCastLength = 0.15f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.PauseToggle();
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.gameObject.CompareTag("Deadly") || CheckForFacePlant())
        {
            //GameManager.Instance.PlayerDie();
        }
    }

    private bool CheckForFacePlant()
    {
        if (Physics.Raycast(lowerBodyPos.position, Vector3.down, rayCastLength, whatIsGround))
            return true;
        if (Physics.Raycast(face.position, Vector3.down, rayCastLength, whatIsGround))
            return true;
        return false;
    }

}
