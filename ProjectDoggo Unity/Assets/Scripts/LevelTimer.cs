using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TimerUI timerUI = null;
    private float startTime;
    private float currentTimer;

    private void Start()
    {
        if(timerUI == null)
            Debug.LogWarning("Missing TimerUI object in inspector of LevelTimer.cs");
        startTime = Time.time;
    }

    private void Update()
    {
        currentTimer = Time.time - startTime;
        timerUI.UpdateTimer(currentTimer);
    }

    public void Reset()
    {
        startTime = Time.time;
    }
}
