using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float launchForce;
    [SerializeField] private float bounceForce;
    [SerializeField] private float idleVelocityThreshold = .05f;
    [SerializeField] private float speedUpJumpPower;
    [SerializeField] private LineRenderer aimLineRenderer;

    [Space]
    [SerializeField] AudioSource hitSound;
    [SerializeField] AudioSource jumpSound;

    private bool isStationary;
    private bool isPreparingToShoot;
    private Rigidbody ballRigidbody;

    private Vector3 windDirection;
    private float windForce;
    

    
    // Other existing variables and methods

    private Vector3 startPosition;
    private CountManager countManager;
    private Vector3 launchDir;
    private void Start()
    {
        countManager = FindObjectOfType<CountManager>();
        countManager.ResetHitCount();
        ballRigidbody = GetComponent<Rigidbody>();
        isPreparingToShoot = false;
        aimLineRenderer.enabled = false;

        windDirection = WindExposureManager.instance.windMoveDirection;
        windForce = WindExposureManager.instance.windSpeed;
    }

    private void FixedUpdate()
    {
        if (ballRigidbody.velocity.magnitude < idleVelocityThreshold)
        {
            BecomeIdle();
        }

        HandleAiming();
    }

    private void OnMouseDown()
    {
        if (isStationary)
        {
            isPreparingToShoot = true;
        }
    }

    private void HandleAiming()
    {
        if (!isPreparingToShoot || !isStationary)
        {
            return;
        }

        Vector3? targetPoint = GetMouseWorldPosition();

        if (!targetPoint.HasValue)
        {
            return;
        }

        RenderAimLine(targetPoint.Value);

        if (Input.GetMouseButtonUp(0))
        {
            LaunchBall(targetPoint.Value);
            countManager.HitCount();
            hitSound.Play();
        }
    }

    private void LaunchBall(Vector3 targetPoint)
    {
        isPreparingToShoot = false;
        aimLineRenderer.enabled = false;

        // Record the start position of the ball before launching
        startPosition = transform.position;

        Vector3 horizontalTargetPoint = new Vector3(targetPoint.x, transform.position.y, targetPoint.z);
        Vector3 launchDirection = (horizontalTargetPoint - transform.position).normalized;
        float power = Vector3.Distance(transform.position, horizontalTargetPoint);
        
        launchDir = launchDirection;
        // Apply horizontal launch force
        ballRigidbody.AddForce(launchDirection * power * launchForce);
        
        // Apply vertical bounce force
        ballRigidbody.AddForce(Vector3.up * bounceForce * power/10, ForceMode.Impulse);

        WindDirection(windDirection, windForce);
        isStationary = false;
    }

    private void RenderAimLine(Vector3 targetPoint)
    {
        Vector3[] linePositions = {
            transform.position,
            targetPoint
        };
        aimLineRenderer.SetPositions(linePositions);
        aimLineRenderer.enabled = true;
    }

    private Vector3? GetMouseWorldPosition()
    {
        Vector3 mousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldPosFar = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 worldPosNear = Camera.main.ScreenToWorldPoint(mousePosNear);
        RaycastHit hit;
        if (Physics.Raycast(worldPosNear, worldPosFar - worldPosNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }

    private void BecomeIdle()
    {
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        isStationary = true;
    }

    public bool IsStationary
    {
        get { return isStationary; }
    }

    public bool IsPreparingToShoot
    {
        get { return isPreparingToShoot; }
    }

    // Method to get the position of the ball at the start of the kick
    public Vector3 GetStartPosition()
    {
        return startPosition;
    }

    public void OnTriggerEnter(Collider other) {

        if (other.GetComponent<HoleManager>()) {
            other.GetComponent<HoleManager>().BallInHole();
        }

        if(other.gameObject.tag == "Sand")
        {
            var lforce = launchForce;
            var bforce = bounceForce;

            launchForce = lforce / 2;
            bounceForce = bforce / 2;
        }

        if(other.gameObject.tag == "Water") {

            var ballWeight = 0.5f;
            var ballForce = 0f;
            Debug.Log("Ball in water");

        }

        if(other.gameObject.tag == "SpeedUpBlock") {

            ballRigidbody.AddForce(launchDir * speedUpJumpPower * launchForce);
            ballRigidbody.AddForce(Vector3.up * bounceForce * speedUpJumpPower / 10, ForceMode.Impulse);
            jumpSound.Play();
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sand")
        {
            var lforce = launchForce;
            var bforce = bounceForce;

            launchForce = lforce * 2;
            bounceForce = bforce * 2;
        }
    }

    public void WindDirection(Vector3 direction, float windForce)
    {
        ballRigidbody.AddForce(direction * windForce, ForceMode.Impulse);

    }
}
