using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TimedSwitch : TimedInteractable
{
    [SerializeField]
    private Sprite on, off;
    private SpriteRenderer sR;
    protected void Start()
    {
        base.Start();
        sR = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if(started)
        {
            sR.sprite = on;
        }
        else
        {
            sR.sprite = off;
        }
    }
}