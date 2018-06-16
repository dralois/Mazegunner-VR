using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour {
    public enum DisplayType { MINUTES_SECONDS, SECONDS};

    [Tooltip("Time in seconds")]
    public int initTime;
    public Text time;
    public UnityEvent onTimerZero;
    public DisplayType type;

    private string formatString;
    private float timeLeft;

    private void Start()
    {
        timeLeft = initTime;
        formatString = time.text;
    }

    private void Update () {
        if (timeLeft < 0)
        {
            onTimerZero.Invoke();
            this.enabled = false;
            return;
        }
        timeLeft -= Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(timeLeft);

        if (type == DisplayType.MINUTES_SECONDS)
            time.text = string.Format(formatString, t.Minutes, t.Seconds);
        else
            time.text = string.Format(formatString, t.TotalSeconds);
    }
}
