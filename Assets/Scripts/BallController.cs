using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float launchForce;
    [SerializeField] private float bounceForce;
    [SerializeField] private float idleVelocityThreshold = .05f;
    [SerializeField] private LineRenderer aimLineRenderer;

    private bool isStationary;
    private bool isPreparingToShoot;
    private Rigidbody ballRigidbody;

    // Other existing variables and methods

    private Vector3 startPosition;

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        isPreparingToShoot = false;
        aimLineRenderer.enabled = false;
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

        // Apply horizontal launch force
        ballRigidbody.AddForce(launchDirection * power * launchForce);

        // Apply vertical bounce force
        ballRigidbody.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

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

    // Method to get the position of the ball at the start of the kick
    public Vector3 GetStartPosition()
    {
        return startPosition;
    }
}
