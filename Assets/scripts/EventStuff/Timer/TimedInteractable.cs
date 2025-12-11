using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TimedInteractable : Interactable, Savable
{
    private TimeManager tM;
    private InteractionManager iM;
    [SerializeField]
    private ObjectData objData;
    [SerializeField]
    private int target;
    private int startTime;
    private int defStart;
    [SerializeField]
    protected int startUpTime, runTime;
    protected bool started, phase1, phase2;
    private IEnumerator runRoutine;
    private IEnumerator delayRoutine;

    protected void Start() {
        iM = GameObject.Find("Manager").GetComponent<InteractionManager>();
        tM = GameObject.Find("Manager").GetComponent<TimeManager>();
        started = false;
        phase1 = false;
        phase2 = false;
        startTime = -1;
        StartUp();
    }

    public override void Reset(int time) {
        AssignData(objData);
        startTime = defStart;
        if(defStart < time && startTime != -1 && !((startUpTime + runTime) < (time - startTime)))
        {
            StartTimer(time);
        }
    }

    public void AssignData(ObjectData data) {
        defStart = data.int1;
        startTime = data.int1;
        started = data.bool1;
        phase1 = data.bool2;
        phase2 = data.bool3;
    }

    public void SetObjectData(ObjectData data)
    {
        objData = data;
    }

    public ObjectData GetData() {
        // Debug.Log("Saved.");
        defStart = startTime;
        objData = new ObjectData(id, startTime, started, phase1, phase2, new Vector3());
        return objData;
    }

    public override void OnInteract(int sender) {
        startTime = tM.FakeTime;
        StartTimer(tM.FakeTime);
    }

    public override void OnCancelInteract(int sender) {
        // StartTimer(tM.FakeTime);
    }

    private IEnumerator StartInstantTimer(int t) {
        phase2 = true;
        Debug.Log("Starting instant timer");
        yield return new WaitForSeconds(t * Time.fixedDeltaTime);
        iM.SendCancelMessage(id, target);
        // Debug.Log("Cancelled");
        started = false;
        phase1 = false;
        phase2 = false;
    }

    private IEnumerator StartDelayedTimer(int t) {
        phase1 = true;
        // Debug.Log("Starting delayed timer");
        yield return new WaitForSeconds(t * Time.fixedDeltaTime);
        iM.SendStartMessage(id, target);
        StartCoroutine(runRoutine);
        phase1 = false;
    }

    private void StartTimer(int time)
    {
        int stateTime = time - startTime;
        Debug.Log("State Time: " + stateTime + ". Def Time: " + defStart + ". Current Time: " + time);
        if(delayRoutine != null){StopCoroutine(delayRoutine);}
        if(runRoutine != null){StopCoroutine(runRoutine);}
        if(stateTime >= startUpTime + runTime || stateTime == 0) { // No recent timer or just pressed
            delayRoutine = StartDelayedTimer(startUpTime);
            runRoutine = StartInstantTimer(runTime);
            Debug.Log("Path 1");
        }
        else if(stateTime > startUpTime) // Saved during runTime for a warp. Time remaining is difference between runTime + delayTime and stateTime. Only run the runTime part
        {
            Debug.Log("Path 2");
            delayRoutine = StartInstantTimer(runTime - (stateTime - startUpTime));
        } 
        else if(stateTime != 0) // Saved during delayTime
        {
            Debug.Log("Path 3");
            delayRoutine = StartDelayedTimer(startUpTime - stateTime);
            runRoutine = StartInstantTimer(runTime);
        }
        started = true;
        // Debug.Log("Starting");
        StartCoroutine(delayRoutine);
    }
}