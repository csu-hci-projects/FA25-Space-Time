using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public abstract class Interactable : TravelEffected {
    protected int importance;
    protected GameObject player;
    private PlayerController playerScript;
    protected SortedList<int, GameObject> currentTouching;

    public void OnTriggerEnter2D(Collider2D other) {
        Controller controller;
        if(other.TryGetComponent<Controller>(out controller))
        {
            currentTouching.Add(controller.id, controller.gameObject);
            controller.SetTouching(this.gameObject, importance, true);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        Controller controller;
        if(other.TryGetComponent<Controller>(out controller))
        {
            currentTouching.Remove(controller.id);
            controller.SetTouching(this.gameObject, importance, false);
            OnCancelInteract();
        }
    }

    protected void StartUp()
    {
        currentTouching = new SortedList<int, GameObject>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
    }
    public abstract void OnInteract();
    public abstract void OnCancelInteract();
}