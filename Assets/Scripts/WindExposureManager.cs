using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindExposureManager : MonoBehaviour
{
    public static WindExposureManager instance { get; private set; }

    public Vector3 windMoveDirection;
    public float windSpeed;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
