using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private CameraController cameraController;

    private GameObject currentBall;
    private Vector3 lastKickStartPosition;

    public bool playerWin;
    public bool playerLoseBall;

    private void Start()
    {
        // Initially spawn the ball at the origin
        SpawnNewBall(Vector3.zero);
    }

    private void Update()
    {
        // Check if the current ball has fallen off the ground
        if (currentBall != null && currentBall.transform.position.y < -10)
        {
            
            Destroy(currentBall);
            currentBall = null;
        }

        // If there is no current ball, spawn a new one at the last kick start position
        if (currentBall == null)
        {
            SpawnNewBall(lastKickStartPosition);

        }

        // Update the last kick start position when the ball starts moving
        if (currentBall != null)
        {
            BallController ballController = currentBall.GetComponent<BallController>();
            if (!ballController.IsStationary && lastKickStartPosition == Vector3.zero)
            {
                lastKickStartPosition = currentBall.transform.position;
            }
        }
    }

    // Method to spawn a new ball at a given position
    private void SpawnNewBall(Vector3 position)
    {
        currentBall = Instantiate(ballPrefab, position, Quaternion.identity);
        cameraController.SetNewBall(currentBall.transform);

        // Reset the last kick start position when a new ball is spawned
        lastKickStartPosition = Vector3.zero;
    }

    public IEnumerator DestroyBallInHole() {
        yield return new WaitForSeconds(2);
        Destroy(currentBall);
        currentBall = null;
    }
}
