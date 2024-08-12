using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoverUpDown : MonoBehaviour
{
    public enum Direct
    {
        Up,
        Down,
    }
    public Transform ASide;
    public Transform BSide;

    private Rigidbody rb;

    public Direct CurrentDirect;
    [SerializeField] private float Speed;
    [SerializeField] private float StopTime;
    [SerializeField] private bool _isStopped;

    CameraController cameraController;


    private void Start()
    {
        ASide.parent = null;
        BSide.parent = null;

        rb = GetComponent<Rigidbody>();
        cameraController = FindObjectOfType<CameraController>();
    }
    void FixedUpdate()
    {


        if (_isStopped == true)
        {
            return;
        }
        if (CurrentDirect == Direct.Up)
        {
            transform.position -= new Vector3(0, Time.deltaTime * Speed, 0);
            if (transform.position.y < ASide.position.y)
            {
                CurrentDirect = Direct.Down;
                _isStopped = true;
                Invoke("ContinueWalk", StopTime);


            }

        }
        else
        {
            transform.position += new Vector3(0, Time.deltaTime * Speed,0);
            if (transform.position.y > BSide.position.y)
            {
                CurrentDirect = Direct.Up;
                _isStopped = true;
                Invoke("ContinueWalk", StopTime);

            }
        }
    }

    public void ContinueWalk()
    {
        _isStopped = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            cameraController.ballInMovePlatform = true;
            cameraController.gameObject.transform.SetParent(transform);
            other.transform.SetParent(transform);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            cameraController.ballInMovePlatform = false;
            cameraController.gameObject.transform.SetParent(null);
            other.transform.SetParent(null);
        }
    }
}
