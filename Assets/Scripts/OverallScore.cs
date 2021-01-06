using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverallScore : MonoBehaviour
{
    public bool battleStarted = false;
    public float timeStarted = 0;
    public float timeAlive = 0;
    public Text timerText;
    public string finalResult;

    void Awake()
    {
        battleStarted = true;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (battleStarted)
        {
            timeAlive += Time.deltaTime;
            int seconds = (int)Mathf.Round(timeAlive);
            int min = (int)seconds / 60;
            seconds -= 60 * min;


            timerText.text = min.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}
