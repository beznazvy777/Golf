using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform ballTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float rotationSensitivity = 100f;

    private BallController ballController;
    private bool shouldFollowBall = true;

    private void Start()
    {
        ballController = ballTransform.GetComponent<BallController>();
    }

    private void LateUpdate()
    {
        if (ballTransform.position.y < -1)
        {
            shouldFollowBall = false;
            return;
        }

        if (shouldFollowBall)
        {
            if (!ballController.IsStationary)
            {
                FollowBall();
            }
            else
            {
                RotateAroundBall();
            }
        }
    }

    private void FollowBall()
    {
        Vector3 targetPosition = ballTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    private void RotateAroundBall()
    {
        if (Input.GetMouseButton(0))
        {
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
            float verticalInput = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;

            transform.RotateAround(ballTransform.position, Vector3.up, horizontalInput);
            transform.RotateAround(ballTransform.position, transform.right, -verticalInput);

            transform.position = ballTransform.position + (transform.position - ballTransform.position).normalized * offset.magnitude;
            transform.LookAt(ballTransform);
        }
    }

    public void SetNewBall(Transform newBallTransform)
    {
        ballTransform = newBallTransform;
        ballController = newBallTransform.GetComponent<BallController>();
        shouldFollowBall = true;
    }
}
