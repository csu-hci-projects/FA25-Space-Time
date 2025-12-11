using UnityEngine;
public abstract class Pressable : TravelEffected {
    private InteractionManager iM;
    public int target;
    // public int recievedId;
    public void StartUp() {
        iM = GameObject.Find("Manager").GetComponent<InteractionManager>();
    }
    protected void OnStart() {
        iM.SendStartMessage(id, target);
    }
    protected void OnCancel()
    {
        iM.SendCancelMessage(id, target);
    }
}