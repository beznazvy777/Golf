using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventUI : MonoBehaviour
{
    CountManager countManager;

    [SerializeField] Animator hitTextAnimator;
    [SerializeField] Animator scoreTextAnimator;

    [Header("UI/FX Prefabs")]
    [SerializeField] GameObject SuperScoreFx;
    [SerializeField] GameObject MediumScoreFx;
    [SerializeField] GameObject LowScoreFx;

    void Start()
    {
        countManager = GetComponent<CountManager>();
        countManager.OnHitActiveCount += CountManager_OnHitActiveCount;
        countManager.OnScoreActiveCount += CountManager_OnScoreActiveCount;

        countManager.OnMaxScoreFX += CountManager_OnMaxScoreFX;
        countManager.OnMediumScoreFX += CountManager_OnMediumScoreFX;
        countManager.OnLowScoreFX += CountManager_OnLowScoreFX;
    }

    private void CountManager_OnLowScoreFX(object sender, System.EventArgs e)
    {
        Instantiate(LowScoreFx, Vector3.zero, Quaternion.identity);
    }

    private void CountManager_OnMediumScoreFX(object sender, System.EventArgs e)
    {
        Instantiate(MediumScoreFx, Vector3.zero, Quaternion.identity);
    }

    private void CountManager_OnMaxScoreFX(object sender, System.EventArgs e)
    {
        Instantiate(SuperScoreFx, Vector3.zero, Quaternion.identity);
    }

    private void CountManager_OnScoreActiveCount(object sender, System.EventArgs e) {
        scoreTextAnimator.SetTrigger("IsHitActive");
    }

    private void CountManager_OnHitActiveCount(object sender, System.EventArgs e) {
        hitTextAnimator.SetTrigger("IsHitActive");
    }

    
}
