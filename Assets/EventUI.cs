using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventUI : MonoBehaviour
{
    CountManager countManager;

    [SerializeField] Animator hitTextAnimator;
    [SerializeField] Animator scoreTextAnimator;
    void Start()
    {
        countManager = GetComponent<CountManager>();
        countManager.OnHitActiveCount += CountManager_OnHitActiveCount;
        countManager.OnScoreActiveCount += CountManager_OnScoreActiveCount;
    }

    private void CountManager_OnScoreActiveCount(object sender, System.EventArgs e) {
        scoreTextAnimator.SetTrigger("IsHitActive");
    }

    private void CountManager_OnHitActiveCount(object sender, System.EventArgs e) {
        hitTextAnimator.SetTrigger("IsHitActive");
    }

    
}
