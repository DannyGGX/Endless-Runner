using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private bool followPlayer;
    [SerializeField] private float followDistanceToPlayer = 8;

    

    private void Awake()
    {
        
    }


    void Update()
    {
        if (followPlayer)
        {
            Vector3 bossPos = playerTransform.position;
            bossPos.z = playerTransform.position.z + followDistanceToPlayer;
            transform.position = bossPos;
        }


    }

    
}
