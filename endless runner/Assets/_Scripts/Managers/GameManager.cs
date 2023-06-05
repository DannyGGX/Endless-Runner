using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Score { get; set; }
    private bool isPaused { get; set; } = false;
    public bool controlsEnabled { get; private set; } = true;

    [SerializeField] private int level1Index = 0;
    [SerializeField] private int level2Index = 1;
    private int currentScene = 0;
    [field: Space]
    [field: Header("Power Ups")]
    [field: SerializeField] public float DoublePointsTime { get; set; } = 6;
    public bool DoublePointsOn { get; set; } = false;

    [field: SerializeField] public float SlowDownTime { get; set; } = 8;
    public bool SlowDownOn { get; set; } = false;
    
    public float gameTime { get; private set; } = 0f;
    [field: Space]
    [field: SerializeField] public float BossSpawnTime { get; private set; } = 30f;
    public bool isBossActive = false;

    public delegate void OnBossSpawn();
    public static OnBossSpawn onBossSpawn;

    public delegate void OnShowDeathScreenCalled();
    public static OnShowDeathScreenCalled onShowDeathScreenCalled;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        controlsEnabled = true;
    }

    private void FixedUpdate()
    {
        gameTime += Time.fixedDeltaTime;
        if (isBossActive == false && gameTime > BossSpawnTime)
        {
            onBossSpawn?.Invoke();
            isBossActive = true;
        }
    }

    public void PlayerDie()
    {
        controlsEnabled = false; // stops player movement
        
    }
    public void ShowDeathScreen()
    {
        onShowDeathScreenCalled?.Invoke();
        //StartGame();
    }

    public void RestartGame()
    {
        StartGame();
    }
    public void StartGame()
    {
        gameTime = 0f;
        Score = 0;
        StartLevel1();
    }

    public void SwitchLevel()
    {
        if (currentScene == level1Index)
        {
            StartLevel2();

        }
        else if (currentScene == level2Index)
        {
            StartLevel1();
        }
    }

    private void StartLevel()
    {
        controlsEnabled = true;
        SceneManager.LoadScene(currentScene);
    }
    private void StartLevel1()
    {
        currentScene = level1Index;
        StartLevel();
    }
    private void StartLevel2()
    {
        currentScene = level2Index;
        StartLevel();
    }

    public void PauseToggle()
    {
        if (isPaused)
            Unpause();
        else
            Pause();
    }
    private void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
        controlsEnabled = false;
    }
    private void Unpause()
    {
        Time.timeScale = 1;
        isPaused = false;
        controlsEnabled = true;
    }
}
