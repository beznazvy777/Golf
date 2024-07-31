using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    CountManager countManager;
    GameManager gameManager;

    void Awake() {
        countManager = FindObjectOfType<CountManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void BallInHole() {
        countManager.ScoreCounter();
        gameManager.StartCoroutine("DestroyBallInHole");
    }
}
