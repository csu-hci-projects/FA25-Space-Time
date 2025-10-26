using UnityEngine;
public class Pressable : TravelEffected {
    private InteractionManager iM;
    public int id;
    public int target;
    void Start() {
        iM = GameObject.Find("Manager").GetComponent<InteractionManager>();
    }
    protected void OnStart() {
        iM.SendStartMessage(id, target);
    }
    protected void OnCancel()
    {
        iM.SendCancelMessage(id, target);
    }
    public override void Reset(int i){ OnCancel(); } 
}