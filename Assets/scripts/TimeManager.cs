using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int time;
    public int GetTime()
    {
        return time;
    }
    void Start()
    {
        StartTime();
    }
    public void SetTime(int t)
    {
        time = t;
    }
    public void StartTime()
    {
        time = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time++;
        // Debug.Log("Time: " + time);
    }
}