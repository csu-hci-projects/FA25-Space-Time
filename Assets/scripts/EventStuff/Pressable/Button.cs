using UnityEngine;
using System.Collections;
public class Button : Pressable {
    private int numTouching;

    public void OnTriggerEnter2D(Collider2D other) {
        numTouching++;
        OnStart();
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if(numTouching > 0) {
            numTouching--;
        }
        Debug.Log(numTouching);
        if(numTouching == 0)
        {
            OnCancel();
        }
    }

    public override void Reset(int i)
    {
        numTouching = 0;
        OnCancel();
    }

    public void Start()
    {
        numTouching = 0;
        StartUp();
    }
}