using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider doublePointsBar;
    [SerializeField] private Slider slowDownBar;

    void Awake()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        scoreText.text = GameManager.Instance.Score.ToString();

        if(GameManager.Instance.DoublePointsOn)
        {
            
        }
    }
}
