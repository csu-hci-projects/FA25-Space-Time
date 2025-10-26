using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject player, ghost, button, elevator, door, lever, crate, teleporter;
    public GameObject[] prefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prefabs = new GameObject[] { player, ghost, button, elevator, door, crate, teleporter };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
