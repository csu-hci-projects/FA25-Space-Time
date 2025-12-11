using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private TimeManager timeManager;
    private GameObject teleporter;
    private SaveManager saveManager;
    private GhostManager ghostManager;
    private GameObject currentTeleporter;
    private Save save;
    private int startFrame = 0;
    private Vector3 startPos = new Vector3(0, 2.5f, 2.5f);
    public bool canWait = false, canTimeTravel = false, canMakeGhosts = false;
    public GameObject firstTeleporter;
    public Vector3 GetStartPos() { return startPos; }
    public int GetStartFrame() { return startFrame; }
    public SaveManager SaveManage {
        get {return saveManager;} private set {saveManager = value;}
    }
    public void CollectUpgrade(string name) {
        if(name == "CryoUpgrade")
        {
            canWait = true;
        }
        else if(name == "WarpUpgrade")
        {
            canTimeTravel = true;
        }
        else if(name == "GhostUpgrade")
        {
            canMakeGhosts = true;
        }
    }
    
    public void TimeTravel() {
        if (canMakeGhosts)
        {
            ghostManager.TimeTravel();
        }
        foreach(TravelEffected t in FindObjectsByType<TravelEffected>(FindObjectsSortMode.None))
        {
            t.Reset(timeManager.GetTime());
        }
    }
    public void SetTeleporter(GameObject g) {
        saveManager.UpdateData(g.GetComponent<Teleporter>().id, timeManager.GetTime());
        ghostManager.SetTeleporter(g);
        teleporter = g;
        startFrame = timeManager.GetTime();
        startPos = teleporter.transform.position;
    }

    public void ResetObjects(int i) {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject g in enemies) {
            Destroy(g);
        }
    }

    public void QuickSave() {
        saveManager.QuickSave(player.transform.position, timeManager.FakeTime, timeManager.RealTime);
    }

    public void LoadGhosts() {
        GameObject[] ghostsToDestroy = GameObject.FindGameObjectsWithTag("Ghost");
        foreach(GameObject g in ghostsToDestroy) {
            Destroy(g);
        }
        List<Ghost> gL = SaveManage.GetGhosts();
        foreach (Ghost g in gL)
        {
            ghostManager.InstantiateGhost(g);
        }

    }

    public void QuickLoad()
    {
        QuickSave quick = saveManager.QuickLoad();
        if(quick != null)
        {
            timeManager.FakeTime = quick.quickFakeTime;
            timeManager.RealTime = quick.quickRealTime;
            player.transform.position = quick.quickPosition;
        }
        LoadGhosts();
    }

    void Start()
    {
        canWait = false; 
        canTimeTravel = false; 
        canMakeGhosts = false;
        player = GameObject.Find("Player");
        timeManager = GetComponent<TimeManager>();
        saveManager = GetComponent<SaveManager>();
        ghostManager = GetComponent<GhostManager>();
        // saveManager.LoadJson();
        SetTeleporter(firstTeleporter);
    }
}
