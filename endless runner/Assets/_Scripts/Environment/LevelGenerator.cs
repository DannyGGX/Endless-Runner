using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
    [SerializeField] private const float SECTION_SPAWN_DISTANCE = 200;


    private Vector3 lastPartEndPos;
    [SerializeField] private Vector3 startLevelPartPos = Vector3.zero;

    [SerializeField] private GameObject[] LevelParts; // All level parts to choose from
    [SerializeField] private int startLevelPartIndex; // Index of level part in the list
    private List<GameObject> ActiveLevelParts = new List<GameObject>(); // List of currently active level parts
    public float zSpawn = 0; // Z position at which to spawn the next ground tile
    public float groundLength = 30; // Length of each ground tile
    [SerializeField] private const int STARTING_lEVEL_PART_AMOUNT = 5;

    void Awake()
    {
        //lastPartEndPos = 
        SpawnLevelPart(startLevelPartIndex);
        
        // Spawn initial ground tiles
        for (int i = 1; i < STARTING_lEVEL_PART_AMOUNT; i++)
        {
            SpawnLevelPart(PickRandomLevelPart()); // Spawn a random ground tile from the groundPrefabs array
        }
    }

    void Update()
    {
        
        if (PlayerDistanceCheck())
        {
            SpawnLevelPart(PickRandomLevelPart()); // Spawn a new random ground tile
            DeleteOldestLevelPart();
        }
    }

    private void SpawnLevelPart(int LevelPartIndex)
    {
        // Instantiate a ground tile at the specified zSpawn position
        GameObject levelPart = LevelParts[LevelPartIndex];

        levelPart = Instantiate(levelPart, transform.forward * zSpawn, transform.rotation);
        ActiveLevelParts.Add(levelPart); // Add the spawned ground tile to the list of active ground tiles
        zSpawn += groundLength; // Update the zSpawn position for the next ground tile
    }

    private Vector3 GetEndPos(GameObject levelPart)
    {
        return levelPart.GetComponentInChildren<Transform>().position;
    }

    private Vector3 GetSpawnPosition(GameObject levelPart)
    {

        throw new System.Exception();
    }

    private void DeleteOldestLevelPart()
    {
        // Destroy the oldest ground tile in the list and remove it from the list
        Destroy(ActiveLevelParts[0]);
        ActiveLevelParts.RemoveAt(0);
    }

    private bool PlayerDistanceCheck()
    {
        return Vector3.Distance(playerTransform.position, lastPartEndPos) < SECTION_SPAWN_DISTANCE;
    }

    private int PickRandomLevelPart()
    {
        return Random.Range(0, LevelParts.Length);
    }
}
