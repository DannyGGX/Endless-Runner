using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator1 : MonoBehaviour {

    [SerializeField] private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 70;
    [SerializeField] private Transform playerTransform;
    [Space]
    [SerializeField] private const int START_LEVEL_PART_INDEX = 0;
    [SerializeField] private const int STARTING_LEVEL_PART_AMOUNT = 8;
    [Space]
    [SerializeField] private List<GameObject> availableLevelParts;
    private List<GameObject> activeLevelParts;

    private const string END_POSITION_OBJECT_NAME = "EndPos";
    private Vector3 lastEndPosition;

    [Space]
    private bool isBossActive = false;
    [SerializeField] private int bossLevelPartStartIndex;

    public delegate void OnLevelPartSpawned();
    public static OnLevelPartSpawned onLevelPartSpawned;

    private void Awake()
    {
        activeLevelParts = new List<GameObject>();
        SpawnLevelPart(START_LEVEL_PART_INDEX);

        for (int i = activeLevelParts.Count; i < STARTING_LEVEL_PART_AMOUNT; i++)
        {
            SpawnLevelPart(GetRandomLevelPartIndex());
        }
        GameManager.onBossSpawn += BossSpawn;
    }

    private void OnDisable()
    {
        GameManager.onBossSpawn -= BossSpawn;
    }

    private void Update()
    {
        if (CheckPlayerDistanceToEnd())
        {
            if (isBossActive)
            {

            }
            else
            {
                SpawnLevelPart(GetRandomLevelPartIndex());
                DeleteOldestLevelPart();
                onLevelPartSpawned?.Invoke(); // to PlayerMovement to IncreaseSpeed()
            }
        }
    }

    private bool CheckPlayerDistanceToEnd()
    {
        return Vector3.Distance(playerTransform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART;
    }

    private void SpawnLevelPart(int levelPartIndex)
    {
        GameObject chosenLevelPart = availableLevelParts[levelPartIndex];
        chosenLevelPart = Instantiate(chosenLevelPart, lastEndPosition, Quaternion.identity);
        activeLevelParts.Add(chosenLevelPart);
        lastEndPosition = chosenLevelPart.transform.Find(END_POSITION_OBJECT_NAME).position;
    }

    private int GetRandomLevelPartIndex()
    {
        return Random.Range(0, availableLevelParts.Count);
    }

    private void DeleteOldestLevelPart()
    {
        Destroy(activeLevelParts[0]);
        activeLevelParts.RemoveAt(0);
    }

    private void BossSpawn()
    {
        //isBossActive = true;


    }

    private void ReplaceLevelParts()
    {
        if (isBossActive)
        {

        }
        else
        {

        }
    }
}
