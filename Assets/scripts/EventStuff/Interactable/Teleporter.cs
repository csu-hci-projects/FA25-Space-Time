using UnityEngine;

public class Teleporter : Interactable
{
    private GameManager gM;
    public override void OnInteract(int senderId)
    {
        // Debug.Log("Setting Teleporter");
        if(senderId == 0)
        {
            gM.SetTeleporter(this.gameObject);
        }
    }
    public override void OnCancelInteract(int senderId) {/* gM.ResetBlock()*/}
    public override void Reset(int i){}

    void Start()
    {
        StartUp();
        gM = GameObject.Find("Manager").GetComponent<GameManager>();
        importance = 0;
    }
}
