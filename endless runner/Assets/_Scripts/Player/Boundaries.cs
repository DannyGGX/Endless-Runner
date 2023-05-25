using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 position;

    private void Awake()
    {
        transform.position = player.position;
    }

    void Update()
    {
        position.z = player.position.z;
        position.y = player.position.y;
        transform.position = position;
    }
}
