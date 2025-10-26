using UnityEngine;
public class Button : Pressable {
    public void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Button Press");
        OnStart();
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        OnCancel();
    }
    public override void Reset(int i)
    {
        OnCancel();
    } 
}