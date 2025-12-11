using UnityEngine;
using System;
using System.Collections;

public class SeededRandomizer : TravelEffected, Savable
{
    public TimeManager tMScript;
    public ObjectData objData;
    public int seed = 0;
    public int numPulled = 0;
    public int currentNum;
    [SerializeField]
    private Randomizable randomizable;
    [SerializeField]
    private float waitTime;
    private int recentStart;
    private IEnumerator waitFunction;
    private IEnumerator startFunction;
    private System.Random random;
    
    void Start()
    {
        tMScript = GameObject.Find("Manager").GetComponent<TimeManager>();
        if(!TryGetComponent<Randomizable>(out randomizable)) {
            Destroy(this);
        }
        numPulled = 0;
        seed = 43;
        random = new System.Random(seed);
        objData = new ObjectData(id, seed, numPulled, new Vector3(waitTime,0f,0f));
        AssignData(objData);
        StartCoroutine(waitFunction);
    }
    public override void Reset(int time) {
        StopCoroutine(waitFunction);
        AssignData(objData);
        StartCoroutine(startFunction);
    }

    protected IEnumerator Run(float time) {
        while(true) {
            yield return new WaitForSeconds(time);
            randomizable.AssignData(random.Next(9999));
            numPulled++;
            recentStart = tMScript.FakeTime;
        }
    }
    protected IEnumerator FirstRun(float time) {
            yield return new WaitForSeconds(time);
            randomizable.AssignData(random.Next(9999));
            recentStart = tMScript.FakeTime;
            numPulled++;
            StartCoroutine(waitFunction);
    }

    public ObjectData GetData() {
        float timeSince = (tMScript.FakeTime - recentStart)/Time.fixedDeltaTime;
        float timeLeft = waitTime - timeSince;
        objData = new ObjectData(id, seed, numPulled, new Vector3(timeLeft,0f,0f));
        return objData;
    }

    public void AssignData(ObjectData data) {
        seed = data.int1;
        numPulled = data.int2;
        random = new System.Random(seed);
        for(int i = 0; i < numPulled; i++) {
            currentNum = random.Next(9999);
        }
        startFunction = FirstRun(data.position1.x); 
        waitFunction = Run(waitTime);
    }

    public void SetObjectData(ObjectData data) {
        objData = data;
    }
}
