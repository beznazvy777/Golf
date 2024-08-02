 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    public float timeToDestroy;

    private void Start()
    {
        Invoke("Destroy", timeToDestroy);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
