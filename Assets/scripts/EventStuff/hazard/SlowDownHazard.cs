using UnityEngine;

public class SlowDownHazard : Hazard
{
    [SerializeField]
    private float slowSpeed;
    private TimeManager tMScript; 

    protected void Start()
    {
        StartUp();
        tMScript = GameObject.Find("Manager").GetComponent<TimeManager>();
    }
    // Slow the player down and then gradually speed up timescale to make them and the world move faster.
    protected override void WhenPlayerEnters(GameObject other)
    {
        other.GetComponent<PlayerController>().ScaleMovementSpeed(slowSpeed);
    }
    protected override void WhenPlayerExits(GameObject other)
    {
        other.GetComponent<PlayerController>().ScaleMovementSpeed(1.0f);
    }

    protected override void WhenGhostEnters(GameObject other)
    {
        
    }
    protected override void WhenGhostExits(GameObject other)
    {
        
    }
}