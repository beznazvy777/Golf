using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public enum Direct {
        Left,
        Right,
    }
    public Transform ASide;
    public Transform BSide;

    private Rigidbody rb;

    public Direct CurrentDirect;
    public float Speed;
    public float StopTime;
    public bool _isStopped;

    CameraController cameraController;
    

    private void Start() {
        ASide.parent = null;
        BSide.parent = null;

        rb = GetComponent<Rigidbody>();
        cameraController = FindObjectOfType<CameraController>();
    }
    void FixedUpdate() {


        if (_isStopped == true) {
            return;
        }
        if (CurrentDirect == Direct.Left) {
            transform.position -= new Vector3(0, 0, Time.deltaTime * Speed);
            if (transform.position.z < ASide.position.z) {
                CurrentDirect = Direct.Right;
                _isStopped = true;
                Invoke("ContinueWalk", StopTime);


            }

        }
        else {
            transform.position += new Vector3(0, 0, Time.deltaTime * Speed);
            if (transform.position.z > BSide.position.z) {
                CurrentDirect = Direct.Left;
                _isStopped = true;
                Invoke("ContinueWalk", StopTime);

            }
        }
    }

    public void ContinueWalk() {
        _isStopped = false;
    }


    private void OnTriggerEnter(Collider other) {
        cameraController.ballInMovePlatform = true;
        cameraController.gameObject.transform.SetParent(transform);
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other) {
        cameraController.ballInMovePlatform = false;
        cameraController.gameObject.transform.SetParent(null);
        other.transform.SetParent(null);
    }


}
