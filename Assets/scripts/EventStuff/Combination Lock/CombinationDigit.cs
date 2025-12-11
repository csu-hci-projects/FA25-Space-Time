using UnityEngine;

public class CombinationDigit : Interactable
{
    private SpriteRenderer spriteRenderer;
    private int currentNumber;
    [SerializeField]
    private Sprite[] sprites;

    public override void OnInteract(int sender) {
        currentNumber++;
        currentNumber = currentNumber % 10;
        spriteRenderer.sprite = sprites[currentNumber];
    }

    public override void Reset(int time) {}
    public int GetNum() {return currentNumber;}
    public void SetNum(int n) {currentNumber = n;}
    public void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentNumber = -1;
        StartUp();
    }

    public void AssignData(int value) {
        spriteRenderer.sprite = sprites[currentNumber];
    }
}