using UnityEngine;
public class Door : PressEffected<DoorSetUpArgs> {
    private Animator animator;
    private BoxCollider2D coll;
    private int senderID;

    public override void Reset(int i) {
        OnCancel(-1);
    } 
    
    protected override void SetUp(DoorSetUpArgs args) {
        id = args.id;
    }
    protected override void OnStart(int senderID) {
        Debug.Log("OnStart");
        animator.ResetTrigger("CloseDoor");
        animator.SetTrigger("OpenDoor");
        coll.enabled = false;
    }

    protected override void OnCancel(int senderID)
    {
        Debug.Log("OnCancel");
        animator.ResetTrigger("OpenDoor");
        animator.SetTrigger("CloseDoor");
        coll.enabled = true;
    }
    
    private void Start() {
        base.GetIM();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
}