using UnityEngine;
public class Door : PressEffected<DoorSetUpArgs> {
    Animator animator;
    BoxCollider2D coll;
    void Start()
    {
        base.GetIM();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
    public override void Reset(int i)
    {
        OnCancel(-1);
    } 
    
    protected override void SetUp(DoorSetUpArgs args)
    {
        id = args.id;
    }
    protected override void OnStart(int senderID) {
        // Debug.Log("Opening!!!");
        animator.SetTrigger("OpenDoor");
        coll.enabled = false;
    }

    protected override void OnCancel(int senderID) {
        // Debug.Log("Closing!!!");
        animator.SetTrigger("CloseDoor");
        coll.enabled = true;
    }
}