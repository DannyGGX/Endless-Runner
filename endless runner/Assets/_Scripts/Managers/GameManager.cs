using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Score { get; set; }

    public bool isPaused { get; set; } = false;
    public bool controlsEnabled { get; private set; } = true;
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

    }

    public void PlayerDie()
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
