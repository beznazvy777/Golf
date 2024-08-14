using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform ballTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float rotationSensitivity = 100f;
    [SerializeField] private float stopFollowPointY;

    private BallController ballController;
    private bool shouldFollowBall = true;

    public bool ballInMovePlatform;

    [SerializeField] Animator cameraAnimator;
    private void Start()
    {
        ballController = ballTransform.GetComponent<BallController>();
        cameraAnimator = GetComponent<Animator>();
        //FollowBall();
    }

    private void LateUpdate()
    {


        if (ballTransform) {
            if (ballTransform.position.y < stopFollowPointY) {
                shouldFollowBall = false;
                return;
            }
        }
       

        if (shouldFollowBall)
        {
            if (!ballController.IsStationary)
            {
                FollowBall();
            }
            
            if (!ballController.IsPreparingToShoot && ballController.IsStationary && !ballInMovePlatform)
            {
                RotateAroundBall();
            }

            if (ballController.IsStationary && ballInMovePlatform) {
                FollowBallInMovePlatform();
            }

            if (!ballController.IsPreparingToShoot && ballController.IsStationary && ballInMovePlatform) {
                
            }

        }
    }

   

    private void FollowBall()
    {
        if (ballTransform) {
            Vector3 targetPosition = ballTransform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);


            transform.LookAt(ballTransform);
        }
        

    }

    private void FollowBallInMovePlatform() {
        if (ballTransform) {
            

            if (Input.GetMouseButton(0)) {
                
                if (ballInMovePlatform) {
                    if (!ballController.IsPreparingToShoot) {
                        float horizontalInput = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
                        float verticalInput = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;

                        transform.RotateAround(ballTransform.position, Vector3.up, horizontalInput);
                        transform.RotateAround(ballTransform.position, transform.right, -verticalInput);

                        transform.position = ballTransform.position + (transform.position - ballTransform.position).normalized * offset.magnitude;
                        transform.LookAt(ballTransform);
                    }
                    if (ballController.IsPreparingToShoot) {
                        float horizontalInput = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;

                        transform.RotateAround(ballTransform.position, Vector3.up, horizontalInput);

                        transform.position = ballTransform.position + (transform.position - ballTransform.position).normalized * offset.magnitude;
                        transform.LookAt(ballTransform);
                    }
                    
                    
                }
            }
            else {
                Vector3 targetPosition = transform.position = ballTransform.position + (transform.position - ballTransform.position).normalized * offset.magnitude; ;
                transform.position = targetPosition;
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

                transform.LookAt(ballTransform);
            }
            
        }
        

    }

    private void RotateAroundBall()
    {
        if (Input.GetMouseButtonDown(0) && cameraAnimator.enabled) {
             cameraAnimator.enabled = false; 
        }

        if (Input.GetMouseButton(0))
        {

            if (!ballInMovePlatform) {
                float horizontalInput = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
                float verticalInput = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;

                transform.RotateAround(ballTransform.position, Vector3.up, horizontalInput);
                transform.RotateAround(ballTransform.position, transform.right, -verticalInput);

                transform.position = ballTransform.position + (transform.position - ballTransform.position).normalized * offset.magnitude;
                transform.LookAt(ballTransform);
            }

            //if (ballInMovePlatform) {
            //    float horizontalInput = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
            //    float verticalInput = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;

            //    transform.RotateAround(ballTransform.position, Vector3.up, horizontalInput);
            //    transform.RotateAround(ballTransform.position, transform.right, -verticalInput);

            //    transform.position = ballTransform.position + (transform.position - ballTransform.position).normalized * offset.magnitude;
            //    transform.LookAt(ballTransform);
            //}
            
        }
    }

    public void SetNewBall(Transform newBallTransform)
    {
        ballTransform = newBallTransform;
        ballController = newBallTransform.GetComponent<BallController>();
        shouldFollowBall = true;
    }

    
}
