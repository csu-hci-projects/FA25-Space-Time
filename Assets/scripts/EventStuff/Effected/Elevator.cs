using UnityEngine;

public class Elevator : PressEffected<ElevatorSetUpArgs>
{
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


    void Start()
    {
        base.GetIM();
        id = ID;
        startPos = transform.position;
        goingUp = false;
        ePlatform = transform.Find("Elevator Platform");
        eBar = transform.Find("Elevator Bar");
        eBase = transform.Find("Elevator Base");
    }
    
    public override void Reset(int i)
    {
        float distanceTraveled = ePlatform.localPosition.y;
        atTop = false;
        atBottom = true;
        Travel(-1.0f * distanceTraveled);
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
    protected override void OnStart(int senderID)
    {
        Debug.Log("OnStart");
        goingUp = true;
    }
    
    protected override void OnCancel(int senderID)
    {
        goingUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (!atBottomgoingUp) Debug.Log("Going up");
        if (goingUp && !atTop)
        {
            if (ePlatform.localPosition.y >= height)
            {
                atTop = true;
            }
            float distance = Time.fixedDeltaTime * speed;
            Travel(distance);
        }
        else if(!goingUp && !atBottom)
        {
            if(ePlatform.localPosition.y <= 0)
            {
                atBottom = true;
            }
            float distance = Time.fixedDeltaTime * speed * -1;
            Travel(distance);
        }
    }
    public void Travel(float distance)
    {
        Debug.Log("Traveling: " + distance);
        Vector3 posChange = new Vector3(0.0f, distance, 0.0f);
        ePlatform.position = ePlatform.position + posChange;

        eBar.position = eBar.position + posChange/2;
        float distanceTraveled = ePlatform.localPosition.y;
        eBar.localScale = new Vector3(1.0f, distanceTraveled+1, 1.0f);
    }
}
