using UnityEngine;
using System.Collections.Generic;

public class GhostManager : MonoBehaviour
{
    public GameObject ghostPrefab;
    private GameObject player, currentTeleporter;
    private int startFrame;
    private GhostData ghosts;
    private TimeManager tM;
    private List<Vector3> positions;
    private List<bool> interactions;
    private int currentGhostID = -1;
    private List<Color> colors = new List<Color>(){
        Color.blue,
        Color.green,
        Color.red,
        Color.cyan,
        Color.orange,
        Color.deepPink,
        Color.grey
    };
    int lastColor;
    public void SetStartFrame(int i) { startFrame = i; }


    /* 
     * Will need to consider ghosts that are instantiated earlier but run later. 
     * We (Yes, I just mean me at this point) Can do this by cutting their data off at startframe and making a new ghost 
       object with the new start frame and position points following.
     * I will also need to consider how to allow players to reset to earlier checkpoints. 
     * - I need to put a start frame associated with each time segment (GhostDictionary)
     * - I will also need to give a teleporterID associated with the aformentioned segment
     * - I need to create a UI that will be opened when the player interacts with an active teleporter to allow the player 
         to choose which point in time they would like to reset, and whether they would like to clear their ghosts.
     * - - If they choose not to destroy their ghosts I will need to move the subsequent ghosts to the current segment
     * - - If they choose to destroy their ghosts I simply need to pop repeatedly.
     * - Regardless of if the player clears their ghosts or does not, permanant progress will be saved by time reached.
     * - When reaching a permanent progress point: 
     * - - The player will be forced to warp back in time to the starting zone
     * - - All of their ghosts will be cleared
     * - - Start time will be set to zero
     * Once a player chooses to reset and clear ghosts, there will be NO undo.
    */
    public void SetTeleporter(GameObject g)
    {
        int time = tM.GetTime();
        Debug.Log("Setting" + time);
        ghosts.SetCheckPoint(time);
        startFrame = time;

    }

    void RecordPosition()
    {
        positions.Add(player.transform.position);
        interactions.Add(player.GetComponent<PlayerController>().IsInteracting);
    }

    public void ResetGhosts()
    {
        ghosts.Clear();
    } 

    

    private void DestroyAllGhosts()
    {
        GameObject[] ghostArr = GameObject.FindGameObjectsWithTag("Ghost"); 
        foreach(GameObject g in ghostArr)
        {
            Destroy(g);
        }
    }
    
    public void TimeTravel()
    {
        DestroyAllGhosts();
        lastColor++;
        Color c = colors[lastColor % 7];
        CreateGhost(c);
        positions.Clear();
        interactions.Clear();
        List<Ghost> gL = ghosts.getGhosts();
        foreach (Ghost g in gL)
        {
            InstantiateGhost(g);
        }
    }

    private void CreateGhost(Color c)
    {
        Ghost g = new Ghost(c, positions, interactions, startFrame, currentGhostID);
        ghosts.Add(g);
        currentGhostID--;
    }
    
    void InstantiateGhost(Ghost ghost)
    {
        GameObject ghostObject = Instantiate(ghostPrefab);
        GhostController gC = ghostObject.GetComponent<GhostController>();
        ghost.StartGhost(gC, tM);
    }


    void FixedUpdate()
    {
        RecordPosition();
        int time = tM.GetTime();
        // List<Ghost> gL = new List<Ghost>();
        // if (ghosts.TryGetValue(time, out gL))
        // {
        //     Debug.Log(gL.Count);
        //     foreach (Ghost g in gL)
        //     {
        //         InstantiateGhost(g);
        //         Debug.Log("Instantiated Ghost");
        //     }
        // }
    }

    void Start()
    {
        ghosts = new GhostData();
        ghosts.SetCheckPoint(0);
        tM = GameObject.Find("Manager").GetComponent<TimeManager>();
        player = GameObject.Find("Player");
        positions = new List<Vector3>();
        interactions = new List<bool>();
    }
}
