using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    
    void Start()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildByIndex+1);
    }

    
}
