using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Score { get; set; }
    public bool DoublePointsOn { get; set; } = false;
    public bool isPaused { get; set; } = false;
    public bool controlsEnabled { get; private set; } = true;

    [SerializeField] private int level1Index = 0;
    [SerializeField] private int level2Index = 1;
    private int currentScene = 0;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

    }

    public void PlayerDie()
    {
        controlsEnabled = false; // stops player movement
        
        StartGame();
    }

    public void RestartGame()
    {

    }
    public void StartGame()
    {
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
