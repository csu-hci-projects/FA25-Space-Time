using UnityEngine;

public class RandomizedPlatform : MonoBehaviour
{
    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private int _number;
    [SerializeField]
    private Sprite enabledSprite;
    [SerializeField]
    private Sprite disabledSprite;
    private int totalNum;
    private int thisNum;
    public int Number {
        get {
            return _number;
        }
        set {
            _number = value;
            if(value % totalNum == thisNum)
            {
                collider.enabled = true;
                spriteRenderer.sprite = enabledSprite;
            }
            else
            {
                collider.enabled = false;
                spriteRenderer.sprite = disabledSprite;
            }
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer.sprite = disabledSprite;
        collider.enabled = false;
    }

    public void AssignData(int value) {
        Number = value;
    }

    public void SetStartCond(int particular, int total)
    {
        totalNum = total;
        thisNum = particular;
    }
}