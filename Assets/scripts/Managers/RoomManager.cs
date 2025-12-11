using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoomManager : MonoBehaviour
{
    public GameObject exitBoxPrefab;
    // private MapUpgrades mapUpg;
    private CameraScript camScript;
    private GameManager gMScript;
    // private SpawnManager sMScript;
    public GameObject[] tMArray;
    private int curTMIndex = 0;
    private RoomGraph roomGraph;
    private Room curRoom;
    private GameObject player;


    private void SetActiveTileMap(int i)
    {
        tMArray[curTMIndex].SetActive(false);
        tMArray[i].SetActive(true);
        curTMIndex = i;
        camScript.UpdateBounds(tMArray[i]);
        roomGraph.SetCurRoom(i);
        curRoom = roomGraph.GetCurRoom();
        Debug.Log("Current Room: " + i);
    }

    void Start()
    {
        gMScript = GetComponent<GameManager>();
        // sMScript = GetComponent<SpawnManager>();
        player = GameObject.Find("Player");
        camScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraScript>();
        SetAllRooms(true);
        roomGraph = new RoomGraph();
        RoomGraphInitializer.RoomGraphInit(roomGraph, tMArray);
        SetActiveTileMap(0);
        // SetLightValues();
    }

    public void SetAllRooms(bool b)
    {
        if (b)
        {
            foreach (GameObject g in tMArray)
            {
                g.transform.position = new Vector3(0, 0, 0);
                g.SetActive(false);
            }
        }
    }

    public void ChangeRoom(int exitNum)
    { // NOT DONE
        Room prevRoom = curRoom;
        int nextRoomInd = prevRoom.GetNextRoom(exitNum);
        Room newRoom = roomGraph.GetRoom(nextRoomInd);
        player.transform.position = newRoom.GetEnterCoords(prevRoom.GetNextEntrance(exitNum));
        SetActiveTileMap(nextRoomInd);
        // ChangeEnemies(nextRoomInd);
        // SetLightValues();
    }

    // public void ChangeEnemies(int nextRoom)
    // {
    //     ClearEnemies();
    //     sMScript.SpawnEnemies(nextRoom);
    // }

    // public void ClearEnemies()
    // {
    //     GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //     foreach (GameObject e in enemies)
    //     {
    //         Destroy(e);
    //     }
    // }
    // public void SetLightValues()
    // {
    //     TileMapScript tMS = tMArray[curTMIndex].GetComponent<TileMapScript>();
    //     if (tMS != null)
    //     {
    //         foreach (Light2D l in tMS.UpgradableLights)
    //         {
    //             l.intensity = mapUpg.lightUp.lightIntensity;
    //             l.shadowIntensity = mapUpg.lightUp.shadowIntensity;
    //             l.pointLightOuterRadius = mapUpg.lightUp.lightRadius;
    //         }
    //     }
    //     else
    //     {
    //         Debug.Log("Null TileMapScript");
    //     }
    // }
}
