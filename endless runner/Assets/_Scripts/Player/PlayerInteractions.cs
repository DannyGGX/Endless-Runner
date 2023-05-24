using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.gameObject.CompareTag("Deadly"))
        {
            // game manager restart
        }
        else
        {
            // wait to see if collider still is hitting to die
        }
    }



}
