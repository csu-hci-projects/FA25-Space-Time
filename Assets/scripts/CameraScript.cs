using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Camera cam;
    private GameObject player;
    private float[] bounds;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        player = GameObject.Find("Player");
    }

    public void UpdateBounds(GameObject obj) {
        TileMapScript mapScript = obj.GetComponent<TileMapScript>();
        if(mapScript != null) {
            bounds = mapScript.GetCameraBounds(cam.orthographicSize, cam.aspect);  
        }
    }

    void FixedUpdate()
    {
        float xPos = player.transform.position.x;
        float yPos = player.transform.position.y;
        if(xPos <= bounds[0]){xPos = bounds[0];}
        if(yPos <= bounds[1]){yPos = bounds[1];}
        if(xPos >= bounds[2]){xPos = bounds[2];}
        if(yPos >= bounds[3]){yPos = bounds[3];}
        transform.position = new Vector3(xPos,yPos,player.transform.position.z) + new Vector3(0,0,-5);
    }
}
