using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.SetInt("FirstEnter", 1);
    }
    void Start()
    {

        SceneManager.LoadScene(3);

    }

    
}
