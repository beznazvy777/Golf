using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] List<Image> WhiteLevelSelector = new List<Image>();
    [SerializeField] GameObject LoaderCanvas;
    [SerializeField] Slider slider;
    public int loadLevel;

    [Header("Level UI Tools")]
    [Header("Level_1")]
    [SerializeField] GameObject Level_1_Active;
    [Space]
    [Header("Level_2")]
    [SerializeField] GameObject Level_2_Active;
    [SerializeField] GameObject Level_2_Deactive;
    [Space]
    [Header("Level_3")]
    [SerializeField] GameObject Level_3_Active;
    [SerializeField] GameObject Level_3_Deactive;
    [Space]
    [Header("Level_4")]
    [SerializeField] GameObject Level_4_Active;
    [SerializeField] GameObject Level_4_Deactive;
    [Space]
    [Header("Level_5")]
    [SerializeField] GameObject Level_5_Active;
    [SerializeField] GameObject Level_5_Deactive;
    [Space]
    [Header("Level_6")]
    [SerializeField] GameObject Level_6_Active;
    [SerializeField] GameObject Level_6_Deactive;
    [Space]
    [Header("Level_7")]
    [SerializeField] GameObject Level_7_Active;
    [SerializeField] GameObject Level_7_Deactive;
    [Space]
    [Header("Level_8")]
    [SerializeField] GameObject Level_8_Active;
    [SerializeField] GameObject Level_8_Deactive;
    [Space]
    [Header("Level_9")]
    [SerializeField] GameObject Level_9_Active;
    [SerializeField] GameObject Level_9_Deactive;
    
    
    

    void Awake()
    {
        LoaderCanvas.SetActive(false);
        StartLevelUIConfig();
	
    }
    public void LoadSelectLevel()
    {
        LoaderCanvas.SetActive(true);
        StartCoroutine("LoadProgressAsync");
    }

    public void SetLevel(int level)
    {   

        StartCoroutine("WhiteSelectorDisable");
        int value = PlayerPrefs.GetInt("FirstEnter");
        if (level == 4 && value == 0)
        {
            loadLevel = 3;
        }
        else
        {
            loadLevel = level;
        }
        
        
    }

    public void LoadLevelOnNumber(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    IEnumerator WhiteSelectorDisable()
    {

        //Disabled all "WhiteUI selectors"
        for (int i = 0; i < WhiteLevelSelector.Count; i++)
        {
            WhiteLevelSelector[i].enabled = false;
            
        }
        yield return null;
    }

    void StartLevelUIConfig() {
        
        Level_1_Active.SetActive(true);

        int l2 = PlayerPrefs.GetInt("Level2");
        int l3 = PlayerPrefs.GetInt("Level3");
        int l4 = PlayerPrefs.GetInt("Level4");
        int l5 = PlayerPrefs.GetInt("Level5");
        int l6 = PlayerPrefs.GetInt("Level6");
        int l7 = PlayerPrefs.GetInt("Level7");
        int l8 = PlayerPrefs.GetInt("Level8");
        int l9 = PlayerPrefs.GetInt("Level9");
        int l10 = PlayerPrefs.GetInt("Level10");


        //Level 2
        if (l2 == 1) {
            Level_2_Active.SetActive(true);
            Level_2_Deactive.SetActive(false);
        }
        else {
            Level_2_Active.SetActive(false);
            Level_2_Deactive.SetActive(true);
        }

        //Level 3
        if (l3 == 1) {
            Level_3_Active.SetActive(true);
            Level_3_Deactive.SetActive(false);
        }
        else {
            Level_3_Active.SetActive(false);
            Level_3_Deactive.SetActive(true);
        }

        //Level 4
        if (l4 == 1) {
            Level_4_Active.SetActive(true);
            Level_4_Deactive.SetActive(false);
        }
        else {
            Level_4_Active.SetActive(false);
            Level_4_Deactive.SetActive(true);
        }

        //Level 5
        if (l5 == 1) {
            Level_5_Active.SetActive(true);
            Level_5_Deactive.SetActive(false);
        }
        else {
            Level_5_Active.SetActive(false);
            Level_5_Deactive.SetActive(true);
        }

        //Level 6
        if (l6 == 1) {
            Level_6_Active.SetActive(true);
            Level_6_Deactive.SetActive(false);
        }
        else {
            Level_6_Active.SetActive(false);
            Level_6_Deactive.SetActive(true);
        }

        //Level 7
        if (l7 == 1) {
            Level_7_Active.SetActive(true);
            Level_7_Deactive.SetActive(false);
        }
        else {
            Level_7_Active.SetActive(false);
            Level_7_Deactive.SetActive(true);
        }

        //Level 8
        if (l8 == 1) {
            Level_8_Active.SetActive(true);
            Level_8_Deactive.SetActive(false);
        }
        else {
            Level_8_Active.SetActive(false);
            Level_8_Deactive.SetActive(true);
        }

        //Level 9
        if (l9 == 1) {
            Level_9_Active.SetActive(true);
            Level_9_Deactive.SetActive(false);
        }
        else {
            Level_9_Active.SetActive(false);
            Level_9_Deactive.SetActive(true);
        }

     
    }

    public IEnumerator LoadProgressAsync() {
        
        AsyncOperation loadingProgress = SceneManager.LoadSceneAsync(loadLevel);

        while (!loadingProgress.isDone) {
            float progress = Mathf.Clamp01(loadingProgress.progress / 0.9f);
            if (progress > slider.value)
                slider.value = progress;

            yield return null;
        }
    }

    
}
