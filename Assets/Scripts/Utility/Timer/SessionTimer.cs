using System;
using UnityEngine;
using System.Collections;

public class SessionTimer : MonoBehaviour 
{
    [SerializeField] private int StartMinutes;
    [SerializeField] private int StartSeconds;

    private DateTime startDateTime;
    private float totalTimer;

    private enum State
    {
        Ready, 
        Ticking, 
        Finished
    }
    private State timerState;

    public void StartTimer()
    {
        timerState = State.Ticking;
    }

    public void ResetTimer()
    {
        timerState = State.Ready;
        totalTimer = StartMinutes * 60 + StartSeconds;
    }

    public void AddTime(float time)
    {
        totalTimer += time;
    }

    public bool HasFinished()
    {
        return timerState.Equals(State.Finished);
    }

    public string GetFormattedString()
    {
        float minutes = Mathf.Floor(totalTimer / 60);
        float seconds = totalTimer % 60;
        return string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));
    }

	private void Awake () 
    {
        ResetTimer();
	}

	private void Update () 
    {
        if(timerState.Equals(State.Ticking))
        {
            totalTimer -= Time.deltaTime;

            if (totalTimer < 0f) 
            {
                totalTimer = 0f;
                timerState = State.Finished;
            }
        }
	}
}
