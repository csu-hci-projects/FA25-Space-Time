using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OneTimeCollectible : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private string name;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collected Item");
        if(other.gameObject.name == "Player")
        {
            gameManager.CollectUpgrade(name);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
    }
}