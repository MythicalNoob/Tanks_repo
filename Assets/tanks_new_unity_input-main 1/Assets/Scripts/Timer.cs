using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float time;
    public bool isCountdown;
    public List<TextMeshProUGUI> text = new List<TextMeshProUGUI>();
    public UnityAction finishCountDown;

    private void OnDestroy()
    {
        instance = null;
    }

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountdown && time >= 0)
        {
            time -= Time.deltaTime;

            DisplayTime(time);

            if (time < 0)
            {
                EndTime();
                finishCountDown();
            }
        }
        else
        {
            time += Time.deltaTime;
        }

        Mathf.Floor(100 / 60);

        //Debug.Log($"{Mathf.Floor(time / 60):00}:{time % 60:00}");
    }

    void DisplayTime(float time)
    {
       

        foreach(TextMeshProUGUI timerText in text)
        {

            timerText.SetText(($"{Mathf.Floor(time / 60):00}:{time % 60:00}"));
        }
    }
    void EndTime()
    {
        foreach(TextMeshProUGUI timerText in text)
        {
            timerText.SetText("00:00");
        }
    }
}
