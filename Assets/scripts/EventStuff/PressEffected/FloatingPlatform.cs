using UnityEngine;
public class FloatingPlatform : PressEffected<DoorSetUpArgs> {
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite enabledSprite;
    [SerializeField]
    private Sprite disabledSprite;
    private BoxCollider2D coll;
    private int senderID;

    public override void Reset(int i) {
        OnCancel(-1);
    } 
    
    protected override void SetUp(DoorSetUpArgs args) {
        id = args.id;
    }
    protected override void OnStart(int senderID) {
        spriteRenderer.sprite = enabledSprite;
        coll.enabled = true;
    }

    protected override void OnCancel(int senderID)
    {
        spriteRenderer.sprite = disabledSprite;
        coll.enabled = false;
    }
    
    private void Start() {
        base.GetIM();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        OnCancel(0);
    }
}