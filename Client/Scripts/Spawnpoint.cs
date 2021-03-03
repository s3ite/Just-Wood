using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public GameObject[] spawnlocations; 
    public GameObject player; 

    private Vector3 respawnLocation; 

    void Awake()
    {
    	spawnlocations = GameObject.FindGameObjectsWithTag("Spawnpoint");
    }

    void Start()
    {
    	player = (GameObject)Resources.Load("Elf", typeof(GameObject));

    	respawnLocation = player.transform.position; 

    	SpawnPlayer();
    }

    void update()
    {

    }

    private void SpawnPlayer()
    {
        int spawn = Random.Range(0, spawnlocations.Length); 
        GameObject.Instantiate(player, spawnlocations[spawn].transform.position, Quaternion.identity); 
    }
}
