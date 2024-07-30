using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField] private GameObject StartSceneLayer;
    [SerializeField] private GameObject SecondSceneLayerOptions;
    [SerializeField] private GameObject QuickChallengeMenu;
    [SerializeField] private GameObject TrophyRoomMenu;
    [SerializeField] private GameObject InstructionsMenu;
    [SerializeField] private GameObject AboutMenu;
    [SerializeField] private GameObject BackButton;

    [Header("LoadingAction")]

    [SerializeField] private Image loadBackground;
    [SerializeField] private Slider slider;
    [SerializeField] private float timeIncrease;

    [SerializeField] bool isMainMenuScene;
    [SerializeField] bool isStartLoadScene;


    void Start()
    {
        if (isMainMenuScene)
        {
            StartSceneLayer.SetActive(true);
            StartCoroutine("StartBackgroundImageFade");
        }

        if (isStartLoadScene)
        {
            StartCoroutine("StartLoaderAsync");
        }
        
    }
    public void OpenQuickChallengeMenu()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

     IEnumerator StartLoaderAsync()
    {
        yield return new WaitForSeconds(2);
        for (float t = 0; t < 3; t += Time.deltaTime)
        {


            slider.value = Mathf.Lerp(0, 0.35f, t);

            yield return null;
        }

        

        yield return new WaitForSeconds(0.5f);
        AsyncOperation loadingProgress = SceneManager.LoadSceneAsync(1);

        while (!loadingProgress.isDone)
        {
            float progress = Mathf.Clamp01(loadingProgress.progress / 0.9f);
            if (progress > slider.value)
                slider.value = progress;
            
            yield return null;
        } 
        
    }

    IEnumerator StartBackgroundImageFade()
    {
        float time = 0f;

        while (time < timeIncrease)
        {
            time += Time.deltaTime;

            float newAlphaValue = Mathf.Lerp(1, 0, time / timeIncrease);
            loadBackground.color = new Color(loadBackground.color.r, loadBackground.color.g, loadBackground.color.b, newAlphaValue);

            yield return null;
        }

        loadBackground.enabled = false;
    }

}
