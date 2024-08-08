using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindExposureManager : MonoBehaviour
{
    public static WindExposureManager instance { get; private set; }

    public Vector3 windMoveDirection;
    public float windSpeed;

    [SerializeField] RectTransform windArrowUI;
    [SerializeField] Vector2 speedBeetwenToValues;
    [SerializeField] Vector2 timerBeetwenToValues;

    [Header("WindTextUpdate")]
    [SerializeField] Text windSpeedText;
    [SerializeField] Text windSpeedTextBack;


    float timer;
    
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

        windSpeed = speedBeetwenToValues.y;
    }

    void Start()
    {
        timer = 0;
        ShowWindUIDirection();
    } 

    void Update()
    {
        WindRandomizer();
    }
    void ShowWindUIDirection()
    {
        if(windMoveDirection.x > 0)
        {
            windArrowUI.eulerAngles = new Vector3(0f,0f,-105f);
        }

        if (windMoveDirection.x < 0)
        {
            windArrowUI.eulerAngles = new Vector3(0f, 0f, 90f);
        }

        if (windMoveDirection.z >= 0)
        {
            windArrowUI.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (windMoveDirection.z < 0)
        {
            windArrowUI.eulerAngles = new Vector3(0f, 0f, -180f);
        }
    }

    void WindRandomizer()
    {
        if(timer <= 0)
        {
            var randomValue = Random.Range(speedBeetwenToValues.x, speedBeetwenToValues.y);
            var randomTime = Random.Range(timerBeetwenToValues.x, timerBeetwenToValues.y);
            timer = randomTime;
            windSpeed = randomValue;
            windSpeedText.text = "WIND " + windSpeed.ToString("0.00") + " (M/S)";
            windSpeedTextBack.text = windSpeedText.text;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;

        }

        
    }
}
