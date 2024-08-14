using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountManager : MonoBehaviour
{

    enum Levels {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9
    }

    [SerializeField] Levels nextLevelSelector;

    [SerializeField] Text TimeText;
    [SerializeField] Text TimeTextBack;
    [Space]
    [SerializeField] Text HitCountText;
    [SerializeField] Text HitCountTextBack;
    [Space]
    [SerializeField] Text ScoreCountText;
    [SerializeField] Text ScoreCountTextBack;
    [Space]
    [SerializeField] Text UpdateScoreCountText;
    [SerializeField] Text UpdateScoreCountTextBack;

    [Header("Values")]
    public float timer;
    public float timeSpeed;
    public float hitCount;
    public int scoreCount;
    public int winLevelScore;
    private string levelPrefs;

    [Space]
    [SerializeField] float bestResultHit;
    [SerializeField] float normalResultHit;
    

    [Header("Score/Values")]
    [SerializeField] int maxHitScore;
    [SerializeField] int mediumHitScore;
    [SerializeField] int lowHitScore;

    [Header("GameFinishPanel")]
    [SerializeField] GameObject GameFinishPanel;
    [SerializeField] GameObject LevelCompleteFX;
    private bool isLevelComplete;

    public event EventHandler OnHitActiveCount;
    public event EventHandler OnScoreActiveCount;
    public event EventHandler OnMaxScoreFX;
    public event EventHandler OnMediumScoreFX;
    public event EventHandler OnLowScoreFX;


    void Start()
    {
        isLevelComplete = false;
        scoreCount = PlayerPrefs.GetInt("Score");
        ResetHitCount();

        levelPrefs = nextLevelSelector.ToString();

        StartTextsUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        GameTimer();
    }

    public void GameTimer() {
        timer -= Time.deltaTime * timeSpeed;
        float timeMinutes = ((float)(timer / 60)) % 60;

        if(timer <= 10) {
            TimeText.color = Color.red;
        }
        if(timer <= 0) {
            timer = 0;
            Debug.Log("GameOver");
            GameFinishPanel.SetActive(true);
        }

        TimeText.text = timeMinutes.ToString("0.00");
        TimeTextBack.text = TimeText.text;
    }

    public void HitCount() {
        hitCount++;
        HitCountText.text = hitCount.ToString();
        HitCountTextBack.text = HitCountText.text;
        OnHitActiveCount?.Invoke(this, EventArgs.Empty);
    }

    public void ResetHitCount() {
        hitCount = 0;
        HitCountText.text = hitCount.ToString();
        HitCountTextBack.text = HitCountText.text;
    }

    public void ScoreCounter() {


        if(hitCount <= bestResultHit) {
            scoreCount += maxHitScore;
            OnMaxScoreFX?.Invoke(this, EventArgs.Empty);
        }
        if (hitCount >bestResultHit && hitCount <= normalResultHit) {
            scoreCount += mediumHitScore;
            OnMediumScoreFX?.Invoke(this, EventArgs.Empty);
        }
        if (hitCount > normalResultHit) {
            scoreCount += lowHitScore;
            OnLowScoreFX?.Invoke(this, EventArgs.Empty);
        }

        ScoreCountText.text = scoreCount.ToString() + "/" + winLevelScore.ToString();
        ScoreCountTextBack.text = ScoreCountText.text;
        OnScoreActiveCount?.Invoke(this, EventArgs.Empty);

        UpdateScoreCountText.text = ScoreCountText.text;
        UpdateScoreCountTextBack.text = ScoreCountText.text;

        if(scoreCount >= winLevelScore) {
            PlayerPrefs.SetInt(levelPrefs, 1);
            if (!isLevelComplete) {
                Instantiate(LevelCompleteFX, transform.position, Quaternion.identity);
                isLevelComplete = true;
            }
        }

        BestPlayerResultSet();
    }

    public void StartTextsUpdate() {
        ScoreCountText.text = scoreCount.ToString() + "/" + winLevelScore.ToString();
        ScoreCountTextBack.text = ScoreCountText.text;
        UpdateScoreCountText.text = ScoreCountText.text;
        UpdateScoreCountTextBack.text = ScoreCountText.text;
    }

    public void BestPlayerResultSet() {
        int bestResult = PlayerPrefs.GetInt("BestResult");
        if (scoreCount > bestResult)
            PlayerPrefs.SetInt("BestResult", scoreCount);


    }
}
