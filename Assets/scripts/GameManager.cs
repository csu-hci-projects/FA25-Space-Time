using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TimeManager timeManager;
    private GameObject teleporter;
    private SaveManager saveManager;
    private GhostManager ghostManager;
    private GameObject currentTeleporter;
    private Save save;
    private int startFrame = 0;
    private Vector3 startPos = new Vector3(0,2.5f,2.5f);
    public Vector3 GetStartPos() { return startPos; }
    public int GetStartFrame() { return startFrame; }

    public void TimeTravel()
    {
        ghostManager.TimeTravel();
        foreach(TravelEffected t in FindObjectsByType<TravelEffected>(FindObjectsSortMode.None))
        {
            t.Reset(timeManager.GetTime());
        }
    }
    public void SetTeleporter(GameObject g)
    {
        // Should set the teleporter, set the start time, and reset ghosts if the current teleporter is the same as the previous teleporter. Maybe spill to disk?
        // if (teleporter == g)
        // {
        //     // ghostManager.ResetGhosts();
        // }
        ghostManager.SetTeleporter(g);
        teleporter = g;
        startFrame = timeManager.GetTime();
        startPos = teleporter.transform.position;
        // ghostManager.SetStartFrame(startFrame);
    }

    public void ResetObjects(int i) {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject g in enemies) {
            Destroy(g);
        }
    }

    public SaveManager GetSaveManager() {
        return saveManager;
    }


    public void EndGame() {

    }
    void Start()
    {
        timeManager = GetComponent<TimeManager>();
        saveManager = GetComponent<SaveManager>();
        ghostManager = GetComponent<GhostManager>();
        saveManager.LoadJson();
    }

    // protected override void OnStart(object sender, PressEventArgs e)
    // {
    //     if(sender.tag == "Teleporter")
    //     {
            
    //     }
    // }
    
    // protected override void OnCancel(object sender, PressEventArgs e)
    // {
        
    // }
}
