using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountManager : MonoBehaviour
{
    [SerializeField] Text TimeText;
    [SerializeField] Text TimeTextBack;
    [Space]
    [SerializeField] Text HitCountText;
    [SerializeField] Text HitCountTextBack;
    [Space]
    [SerializeField] Text ScoreCountText;
    [SerializeField] Text ScoreCountTextBack;
    
    [Header("Values")]
    public float timer;
    public float timeSpeed;
    public float hitCount;
    public int scoreCount;

    [Header("Score/Values")]
    [SerializeField] int maxHitScore;
    [SerializeField] int mediumHitScore;
    [SerializeField] int lowHitScore;

    public event EventHandler OnHitActiveCount;
    public event EventHandler OnScoreActiveCount;
    public event EventHandler OnMaxScoreFX;
    public event EventHandler OnMediumScoreFX;
    public event EventHandler OnLowScoreFX;


    void Start()
    {
        scoreCount = PlayerPrefs.GetInt("Score");
        ResetHitCount();
    }

    // Update is called once per frame
    void Update()
    {
        GameTimer();
    }

    public void GameTimer() {
        timer -= Time.deltaTime * timeSpeed;
        float timeMinutes = ((float)(timer / 60)) % 60;
        if(timer <= 0) {
            timer = 0;
            Debug.Log("GameOver");
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


        if(hitCount <= 1) {
            scoreCount += maxHitScore;
            OnMaxScoreFX?.Invoke(this, EventArgs.Empty);
        }
        if (hitCount >1 && hitCount <= 3) {
            scoreCount += mediumHitScore;
            OnMediumScoreFX?.Invoke(this, EventArgs.Empty);
        }
        if (hitCount > 3) {
            scoreCount += lowHitScore;
            OnLowScoreFX?.Invoke(this, EventArgs.Empty);
        }

        ScoreCountText.text = scoreCount.ToString();
        ScoreCountTextBack.text = ScoreCountText.text;
        OnScoreActiveCount?.Invoke(this, EventArgs.Empty);
    }
}
