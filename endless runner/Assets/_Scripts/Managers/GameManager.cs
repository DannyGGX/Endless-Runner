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
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

    }

    public void PlayerDie()
    {

    }

    public void RestartGame()
    {

    }

    public void SwitchLevel()
    {

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
