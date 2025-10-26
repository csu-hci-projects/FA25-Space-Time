using UnityEngine;
using System.Collections.Generic;

public class GhostController : Controller
{
    Color color;
    Vector3 curPosition;
    List<Vector3> positions;
    List<bool> interactions;
    TimeManager tmScript;
    int startFrame;
    int numFrames;
    bool currentlyInteracting = false;

    public void OnStartGhost(Color c, List<Vector3> p, List<bool> i, int s, TimeManager tm, int ID)
    {
        id = ID;
        currentTouching = new SortedList<int, GameObject>();
        positions = p;
        interactions = i;
        color = c;
        startFrame = s;
        tmScript = tm;
        numFrames = p.Count;
        Transform body = transform.Find("body");
        if(body!= null)
        {
            body.gameObject.GetComponent<SpriteRenderer>().color = c;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Get the current time, subtract instantiation time, and 
        int currentFrame = tmScript.GetTime();
        int index = currentFrame - startFrame;
        if (index < numFrames)
        {
            transform.position = positions[index];
            if (currentlyInteracting != interactions[index])
            {
                OnInteract(interactions[index]);
                Debug.Log("Interaction switch: " + interactions[index] + ". Current Touching Size: " + currentTouching.Count);
                currentlyInteracting = interactions[index];
            }
        }
        else
        {
            // Debug.Log("Destroy time: " + tmScript.GetTime());
            GameObject.Destroy(gameObject);
        }
    }
}
