using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bossTimer;
    [Space]
    [Header("Power Ups")]
    [SerializeField] private GameObject doublePointsUI;
    [SerializeField] private Slider doublePointsBar;
    private float doublePointsBarFill = 0;
    [Space]
    [SerializeField] private GameObject slowDownUI;
    [SerializeField] private Slider slowDownBar;
    private float slowDownBarFill = 0;

    public delegate void OnSlowDownTimerFinished();
    public static OnSlowDownTimerFinished onSlowDownTimerFinished;

    private void Awake()
    {
        GameManager.onBossSpawn += HideBossCountDown;
        PlayerInteractions.onDoublePointsPickUp += DoublePointsActivation;
        PlayerInteractions.onSlowDownPickUp += SlowDownActivation;

        doublePointsUI.SetActive(false);
        slowDownUI.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.onBossSpawn -= HideBossCountDown;
        PlayerInteractions.onDoublePointsPickUp -= DoublePointsActivation;
        PlayerInteractions.onSlowDownPickUp -= SlowDownActivation;
    }

    private void FixedUpdate()
    {
        scoreText.text = GameManager.Instance.Score.ToString();

        bossTimer.enabled = !GameManager.Instance.isBossActive;
        bossTimer.text = GetBossCountDownTime().ToString();

        doublePointsBar.value = doublePointsBarFill;
        slowDownBar.value = slowDownBarFill;
    }

    private void DoublePointsActivation()
    {
        StopCoroutine(nameof(DepleteDoublePointsBar));
        GameManager.Instance.DoublePointsOn = true;
        doublePointsUI.SetActive(true);
        doublePointsBar.maxValue = GameManager.Instance.DoublePointsTime;
        doublePointsBar.minValue = 0;
        doublePointsBar.value = GameManager.Instance.DoublePointsTime;
        StartCoroutine(nameof(DepleteDoublePointsBar));
    }
    IEnumerator DepleteDoublePointsBar()
    {
        float minValue = 0;
        float maxValue = GameManager.Instance.DoublePointsTime;
        float duration = GameManager.Instance.DoublePointsTime;
        float timeElapsed = duration;

        while(timeElapsed > 0)
        {
            float t = timeElapsed / duration;
            doublePointsBarFill = Mathf.Lerp(minValue, maxValue, t);
            timeElapsed -= Time.deltaTime;
            yield return null;
        }
        doublePointsBarFill = minValue;
        GameManager.Instance.DoublePointsOn = false;
        doublePointsUI.SetActive(false);
    }

    private void SlowDownActivation()
    {
        StopCoroutine(nameof(DepleteSlowDownBar));
        //GameManager.Instance.SlowDownOn = true; // this has to be done in slow down method in PlayerMovement
        slowDownUI.SetActive(true);
        slowDownBar.maxValue = GameManager.Instance.SlowDownTime;
        slowDownBar.minValue = 0;
        slowDownBar.value = GameManager.Instance.SlowDownTime;
        StartCoroutine(nameof(DepleteSlowDownBar));
    }
    IEnumerator DepleteSlowDownBar()
    {
        float minValue = 0;
        float maxValue = GameManager.Instance.SlowDownTime;
        float duration = maxValue;
        float timeElapsed = duration;
        while (timeElapsed > 0)
        {
            float t = timeElapsed / duration;
            slowDownBarFill = Mathf.Lerp(minValue, maxValue, t);
            timeElapsed -= Time.deltaTime;
            yield return null;
        }
        slowDownBarFill = minValue;
        GameManager.Instance.SlowDownOn = false;
        slowDownUI.SetActive(false);
        onSlowDownTimerFinished?.Invoke();
    }


    private int GetBossCountDownTime()
    {
        float time = GameManager.Instance.gameTime - GameManager.Instance.BossSpawnTime;
        time = Mathf.Abs(time);
        return Mathf.RoundToInt(time);
    }

    private void HideBossCountDown()
    {
        //bossTimer.enabled = false;
    }
}
