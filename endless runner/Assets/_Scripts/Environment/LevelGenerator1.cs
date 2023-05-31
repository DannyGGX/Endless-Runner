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
    [SerializeField] private List<GameObject> activeLevelParts;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        SpawnLevelPart(START_LEVEL_PART_INDEX);

        for (int i = 1; i < STARTING_LEVEL_PART_AMOUNT; i++)
        {
            SpawnLevelPart(GetRandomLevelPartIndex());
        }
    }

    private void Update()
    {
        if (CheckPlayerDistance())
        {
            SpawnLevelPart(GetRandomLevelPartIndex());
            DeleteOldestLevelPart();
        }
    }

    private bool CheckPlayerDistance()
    {
        return Vector3.Distance(playerTransform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART;
    }

    private void SpawnLevelPart(int levelPartIndex) {
        GameObject chosenLevelPart = availableLevelParts[levelPartIndex];
        chosenLevelPart = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        //chosenLevelPart = Instantiate(chosenLevelPart, lastEndPosition, Quaternion.identity);
        activeLevelParts.Add(chosenLevelPart);
        lastEndPosition = chosenLevelPart.transform.Find("EndPos").position;
    }

    private GameObject SpawnLevelPart(GameObject levelPart, Vector3 spawnPosition) 
    {
        return Instantiate(levelPart, spawnPosition, Quaternion.identity);
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
}
