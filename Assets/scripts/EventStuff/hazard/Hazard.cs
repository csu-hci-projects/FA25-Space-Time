using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    private GameObject player;
    
    protected abstract void WhenPlayerEnters(GameObject other);
    protected abstract void WhenPlayerExits(GameObject other);
    protected abstract void WhenGhostEnters(GameObject other);
    protected abstract void WhenGhostExits(GameObject other);

    protected void StartUp()
    {
        player = GameObject.Find("Player");
    }

    public void OnTriggerEnter2D(Collider2D other) {
        GameObject obj = other.gameObject;
        if(obj.name == "Player") {
            Debug.Log("Player entered");
            WhenPlayerEnters(obj);
        } else if(obj.tag == "Ghost") {
            WhenGhostEnters(obj);
        }
    }
    public void OnTriggerExit2D(Collider2D other) {
        GameObject obj = other.gameObject;
        if(obj.name == "Player") {
            WhenPlayerExits(obj);
        } else if(obj.tag == "Ghost") {
            WhenGhostExits(obj);
        }
    }
}
