using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private TextMeshProUGUI scoreText;


    private void Awake()
    {
        GameManager.onShowDeathScreenCalled += ShowDeathScreen;
        deathScreen.SetActive(false);
    }
    private void OnDisable()
    {
        GameManager.onShowDeathScreenCalled -= ShowDeathScreen;
    }

    private void ShowDeathScreen()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
        deathScreen.SetActive(true);
    }
    
    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
