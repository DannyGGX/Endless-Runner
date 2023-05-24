using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    private const float SECTION_SPAWN_DISTANCE = 200;


    private Vector3 lastSectionEndPos;

    public GameObject[] groundPrefabs; // Array of ground prefabs to spawn
    public float zSpawn = 0; // Z position at which to spawn the next ground tile
    public float groundLength = 30; // Length of each ground tile
    public int numberOfGroundTiles = 5; // Number of initial ground tiles to spawn
    private List<GameObject> activeGroundTiles = new List<GameObject>(); // List of currently active ground tiles
    public Transform playerTransform; // Reference to the player's transform

    // Start is called before the first frame update
    void Start()
    {
        // Spawn initial ground tiles
        for (int i = 0; i < numberOfGroundTiles; i++)
        {
            if (i == 0)
                SpawnTile(0); // Spawn the first ground tile from the groundPrefabs array
            else
                SpawnTile(Random.Range(0, groundPrefabs.Length)); // Spawn a random ground tile from the groundPrefabs array
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Check if the player has moved ahead of the currently spawned ground tiles
        if (playerTransform.position.z - 35 > zSpawn - (numberOfGroundTiles * groundLength))
        {
            SpawnTile(Random.Range(0, groundPrefabs.Length)); // Spawn a new random ground tile
            DeleteGroundTile(); // Delete the oldest ground tile
        }
    }

    public void SpawnTile(int groundIndex)
    {
        // Instantiate a ground tile at the specified zSpawn position
        GameObject go = Instantiate(groundPrefabs[groundIndex], transform.forward * zSpawn, transform.rotation);
        activeGroundTiles.Add(go); // Add the spawned ground tile to the list of active ground tiles
        zSpawn += groundLength; // Update the zSpawn position for the next ground tile
    }

    private void DeleteGroundTile()
    {
        // Destroy the oldest ground tile in the list and remove it from the list
        Destroy(activeGroundTiles[0]);
        activeGroundTiles.RemoveAt(0);
    }
}
