using UnityEngine;

public class Teleporter : Interactable
{
    private GameManager gM;
    public override void OnInteract()
    {
        // Debug.Log("Setting Teleporter");
        gM.SetTeleporter(this.gameObject);
    }
    public override void OnCancelInteract() { }
    public override void Reset(int i){}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartUp();
        gM = GameObject.Find("Manager").GetComponent<GameManager>();
        importance = 0;
    }
}
