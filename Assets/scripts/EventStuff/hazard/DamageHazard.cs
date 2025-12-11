using UnityEngine;

public class AcidHazard : Hazard
{
    [SerializeField]
    private float acidDamage;
    
    protected override void WhenPlayerEnters(GameObject other)
    {
        other.GetComponent<Controller>().Die();
    }

    protected override void WhenGhostEnters(GameObject other)
    {
        other.GetComponent<Controller>().Die();
    }

    protected override void WhenPlayerExits(GameObject other)
    {
        other.GetComponent<Controller>().Die();
    }

    protected override void WhenGhostExits(GameObject other)
    {
        other.GetComponent<Controller>().Die();
    }
}