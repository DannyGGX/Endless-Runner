using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 position;

    private void Awake()
    {
        position.x = player.position.x;
    }

    void Update()
    {
        position.z = player.position.z;
        position.y = player.position.y;
        transform.position = position;
    }
}
