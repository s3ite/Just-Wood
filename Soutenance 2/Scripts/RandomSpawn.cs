using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
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
    	//player = (GameObject)PrefabS("Elf", typeof(GamoObject));
    	//respawnLocation = player.transform.position; 

    	//SpawnPlayer(); 
    } 

    void Update() 
    {

    }

    private void SpawnPlayer()
    {
    	//int spawn = Random.Range(0, spawnlocations.length);
    	//GamoObject.Instantiate(player, spawnlocation[spawn].transform.position, Quaternion.identity);
    }
}
