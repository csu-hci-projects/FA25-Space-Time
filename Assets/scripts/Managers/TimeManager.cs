using UnityEngine;
using System;
public class TimeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private int _time = 0;
    [SerializeField]
    private int _actualTime = 0;
    [SerializeField]
    private float _timeScale;
    [SerializeField]
    private bool _isPaused;

    public int FakeTime { get { return _time; } set { _time = value; } }
    public int RealTime { get { return _actualTime; } set { _actualTime = value; } }
    public float TimeScale { get { return _timeScale; } set { Time.timeScale = value; _timeScale = value; } }
    public bool IsPaused { get { return _isPaused; } set { Time.timeScale = value ? 0 : TimeScale; _isPaused = value; } }
    public int GetTime() {
        return FakeTime;
    }
    void Start() {
        StartTime();
    }
    public void SetTime(int t) {
        FakeTime = t;
    }
    public void StartTime() {
        FakeTime = 0;
        RealTime = 0;
        TimeScale = 1.0f;
        IsPaused = false;
    }
    public void Pause() {
        TimeScale = 0;

    }
    private void FixedUpdate() {
        RealTime++;
        FakeTime++;
    }
    public string GetTimeString() {
        double fpmsScale = Math.Floor(1f/Time.fixedDeltaTime)/100f;
        int convertedTime = (int)Math.Floor(FakeTime * (1/fpmsScale));
        int ms = (int)(convertedTime % 100);
        int seconds = (int)Math.Floor((double)(convertedTime % 6000 / 100));
        int minutes = (int)Math.Floor((double)(convertedTime % 360000 / 6000));
        int hours = (int)Math.Floor((double)(convertedTime % 2160000 / 360000));
        return hours + ":" + minutes + ":" + seconds + ":" + ms;
    }
}