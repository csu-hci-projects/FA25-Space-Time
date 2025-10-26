using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrateScript : Interactable
{
    GameObject mainObject;
    private bool isAttached;
    private GameObject attachObject;
    private Vector3 prevPosition;
    private Vector3 startPos;
    private void GetAttached()
    {
        attachObject = currentTouching.Values.Last();
    }
    public override void OnInteract()
    {
        GetAttached();
        isAttached = true;
        prevPosition = attachObject.transform.position;
    }
    public override void OnCancelInteract()
    {
        isAttached = false;
    }

    public override void Reset(int i)
    {
        transform.parent.position = startPos;
        Debug.Log("Reseting Crate");
    } 

    void Start()
    {
        mainObject = transform.parent.gameObject;
        StartUp();
        isAttached = false;
        attachObject = player;
        startPos = transform.position;
    }
    

    void Update()
    {
        if (isAttached)
        {
            mainObject.transform.position += attachObject.transform.position - prevPosition;
            prevPosition = attachObject.transform.position;
        }
    }
}
