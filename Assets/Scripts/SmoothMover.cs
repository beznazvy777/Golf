using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMover : MonoBehaviour
{
    // Points A and B between which the object will move
    public Vector3 pointA;
    public Vector3 pointB;

    // Speed at which the object moves
    public float speed = 2.0f;

    // Axis along which the object will move (X or Y)
    public enum Axis { Z, Y }
    public Axis moveAxis = Axis.Z;

    // Reference to the Rigidbody component
    private Rigidbody rb;

    private Vector3 targetPoint;
    private bool movingToB = true;

    void Start()
    {
        // Initialize the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Initialize the starting position and target point
        transform.position = pointA;
        targetPoint = pointB;
    }

    void FixedUpdate()
    {
        // Calculate the direction to the target point
        Vector3 direction = (targetPoint - transform.position).normalized;

        // Move the object smoothly towards the target point
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);

        // Check if the object has reached the target point
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            // Swap the target points
            if (movingToB)
            {
                targetPoint = pointA;
            }
            else
            {
                targetPoint = pointB;
            }
            movingToB = !movingToB;
        }

        // Clamp the movement to the specified axis
        if (moveAxis == Axis.Z)
        {
            rb.MovePosition(new Vector3(transform.position.x, pointA.y, transform.position.z));
        }
        else if (moveAxis == Axis.Y)
        {
            rb.MovePosition(new Vector3(transform.position.x, transform.position.y, pointA.z));
        }
    }
}
