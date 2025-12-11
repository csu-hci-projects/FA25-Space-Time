using UnityEngine;

public class Elevator : PressEffected<ElevatorSetUpArgs>, Savable
{
    [SerializeField]
    private ObjectData objData;
    private Transform ePlatform, eBar, eBase;
    [SerializeField]
    int ID;
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private Vector3 startPos;
    [SerializeField]
    private float height;
    [SerializeField]
    private bool goingUp, atTop, atBottom;

    // Start Savable
    public ObjectData GetData()
    {
        objData = new ObjectData(id, -1, goingUp, atTop, atBottom, startPos + new Vector3(0f, ePlatform.localPosition.y, 0f));
        return objData;
    }
    public void AssignData(ObjectData data)
    {
        float distanceTraveled = data.position1.y - startPos.y;
        goingUp = data.bool1;
        atTop = data.bool2;
        atBottom = data.bool3;
        Travel(distanceTraveled - ePlatform.localPosition.y);
    }

    // Start PressEffected
    protected override void OnStart(int senderID)
    {
        // Debug.Log("On Elevator Start");
        goingUp = true;
    }

    protected override void OnCancel(int senderID)
    {
        // Debug.Log("Cancelling elevator");
        goingUp = false;
    }
    protected override void SetUp(ElevatorSetUpArgs args)
    {
        startPos = args.startPos;
        height = args.height;
        id = args.id;
        speed = args.speed;
        atTop = false;
        atBottom = true;
    }
    public void SetObjectData(ObjectData data)
    {
        objData = data;
    }

    // Start TravelEffected
    public override void Reset(int i)
    {
        float distanceTraveled = ePlatform.localPosition.y;
        atTop = false;
        atBottom = true;
        // Travel(-1.0f * distanceTraveled);
        AssignData(objData);
    }

    public void Travel(float distance)
    {
        // Debug.Log("Traveling: " + distance);
        Vector3 posChange = new Vector3(0.0f, distance, 0.0f);
        ePlatform.position = ePlatform.position + posChange;

        eBar.position = eBar.position + posChange/2;
        float distanceTraveled = ePlatform.localPosition.y;
        eBar.localScale = new Vector3(1.0f, distanceTraveled+1, 1.0f);
    }

    private void Update()
    {
        if (goingUp && !atTop)
        {
            if(ePlatform.localPosition.y > 0){atBottom = false;}
            if (ePlatform.localPosition.y >= height)
            {
                atTop = true;
            }
            float distance = Time.fixedDeltaTime * speed;
            Travel(distance);
        }
        else if (!goingUp && !atBottom)
        {
            if(ePlatform.localPosition.y < height){atTop = false;}
            if (ePlatform.localPosition.y <= 0)
            {
                atBottom = true;
            }
            float distance = Time.fixedDeltaTime * speed * -1;
            Travel(distance);
        }
    }
    
    private void Start()
    {
        base.GetIM();
        id = ID;
        startPos = transform.position;
        goingUp = false;
        ePlatform = transform.Find("Elevator Platform");
        eBar = transform.Find("Elevator Bar");
        eBase = transform.Find("Elevator Base");
    }

}
