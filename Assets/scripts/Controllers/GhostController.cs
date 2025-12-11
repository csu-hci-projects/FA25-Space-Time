using UnityEngine;
using System;
using System.Collections.Generic;

public class GhostController : Controller
{
    private Color color;
    private Vector3 curPosition;
    private Vector3 nextPosition;
    private List<Vector3> positions;
    private List<bool> interactions;
    private TimeManager tM;
    private int startFrame;
    private int numFrames;
    private int prevFrame;
    private bool currentlyInteracting = false;
    private bool hasNext;
    public override void Die() {
        
    }
    public void OnStartGhost(Color c, List<Vector3> p, List<bool> i, int s, TimeManager tm, int ID)
    {
        id = ID;
        currentTouching = new SortedList<int, GameObject>();
        positions = p;
        interactions = i;
        color = c;
        startFrame = s;
        tM = tm;
        numFrames = p.Count;
        prevFrame = startFrame;
        Transform body = transform.Find("body");
        transform.position = positions[tM.FakeTime - (startFrame-1)];
        if (body != null)
        {
            body.gameObject.GetComponent<SpriteRenderer>().color = c;
        }
        hasNext = true;
    }

    void Update()
    {
        if(hasNext)
        {
            float distance = Vector3.Distance(curPosition, nextPosition);
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, distance/tM.TimeScale*(Time.deltaTime/Time.fixedDeltaTime)); // Moves the ghost a fraction of the distance based on the difference between deltaTime and fixedDeltaTime 
        }
    }

    void FixedUpdate()
    {
        // Get the current time, subtract instantiation time, and 
        int currentFrame = tM.GetTime();
        int index = currentFrame - startFrame;
        hasNext = true;
        if (index < numFrames && index >= 0)
        {
            if (currentFrame != prevFrame)
            {
                prevFrame = currentFrame;
                if (index + 1 < positions.Count)
                {
                    nextPosition = positions[index + 1]; hasNext = true;
                }
                else { hasNext = false; }
                try
                {
                    curPosition = positions[index];
                }
                catch(Exception e)
                {
                    Debug.Log("Error: " + e + "Index: " + index + ", numFrames: " + numFrames + ".");
                }
                if (currentlyInteracting != interactions[index])
                {
                    OnInteract(interactions[index]);
                    currentlyInteracting = interactions[index];
                }
            }
            
        }
        else
        {
            // Debug.Log("Destroying");
            GameObject.Destroy(gameObject);
        }
    }
}
