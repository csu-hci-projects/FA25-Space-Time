using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TileMapScript : MonoBehaviour
{
    public int sizeX, sizeY;
    public GameObject[] objects;
    private const float ratio = 1.777778f;
    private Light2D[] _upgradableLights;

    public Light2D[] UpgradableLights {
        get { return _upgradableLights; }
        private set { _upgradableLights = value; }
    }

    // Return sthe camera bounds in minX,minY,maxX,maxY format
    public float[] GetCameraBounds(float cameraSize, float aspectRatio) {
        Debug.Log(cameraSize + " " + aspectRatio);
        Vector2 pos = transform.position;
        float[] returnArr = new float[4];
        returnArr[0] = cameraSize * aspectRatio;
        returnArr[1] = cameraSize;
        returnArr[2] = sizeX - (cameraSize * aspectRatio);
        returnArr[3] = sizeY - cameraSize;
        return returnArr;
    }
    public void Start()
    {
        SetUpRoom();
    }
    public void SetUpRoom()
    {
        
    }

}
