using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour {

    [Tooltip("Time in seconds")]
    public int initTime;
    public Text time;
    public UnityEvent onTimerZero;

    private float timeLeft;

    private void Start()
    {
        timeLeft = initTime;
    }

    private void Update () {
        if (timeLeft < 0)
        {
            time.text = "00m:00s";
            onTimerZero.Invoke();
            this.enabled = false;
            return;
        }
        timeLeft -= Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(timeLeft);

        time.text = string.Format("{0:D2}m:{1:D2}s",t.Minutes,t.Seconds);
    }
}
