using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    [SerializeField] AudioSource tadaSound;
    [SerializeField] AudioSource cheerSound;

    CountManager countManager;
    GameManager gameManager;


    void Awake() {
        countManager = FindObjectOfType<CountManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void BallInHole() {
        countManager.ScoreCounter();
        gameManager.StartCoroutine("DestroyBallInHole");
        tadaSound.Play();
        cheerSound.Play();

    }
}
