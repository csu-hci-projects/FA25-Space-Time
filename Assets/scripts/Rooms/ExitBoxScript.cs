using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBoxScript : MonoBehaviour
{
    public int exitNum;
    private RoomManager rManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") {
            rManager.ChangeRoom(exitNum);
        }
    }

    void Awake()
    {
        rManager = GameObject.Find("Manager").GetComponent<RoomManager>();
    }
}
