using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = Unity.Mathematics.Random;


public class ZoneRouge : MonoBehaviour
{
    private List<(float, float)> pointSpawn;
    
    //Timer
    private float currenttime = 0f;
    private int delaySpawnHemi = 5; // 120


    private float startSpawnZone = 10; //540
    private int delaySpawnZone = 10; // 60


    private (int, int) selectionHemi;
    private int selectionZone;
    
    private int ind = 0;

    protected List<int> listHemi = new List<int>{};
    protected List<int> listZone = new List<int>{30, 31, 32, 33};
    private int countH, countZ;

    
    private bool alreadyDoHemi = false;
    private bool alreadyDoZone = false;

    private Transform ZoneActive;
    
    

    void Start()
    {
        for (int i = 0; i < 30; i++)
            listHemi.Add(i);
        
        //if (nb.Count > 28) Debug.Log("Liste initialisé");

    }

    void Update()
    {
        currenttime += 1 * Time.deltaTime;
        countH = listHemi.Count;
        countZ = listZone.Count;
        
        // Activation des zones
        if (countH > 0 && (int) currenttime % delaySpawnHemi == 0 && !alreadyDoHemi)
        {
            selectionHemi = HemiActivation(listHemi);
            ind = selectionHemi.Item1;

            ZoneActive = gameObject.transform.GetChild(ind);
            ZoneActive.GetComponent<DestroyElfeZoneRouge>().isActive = true;
            gameObject.transform.GetChild(ind).gameObject.SetActive(true);
            
            alreadyDoHemi = true;
        }
        
        if (countZ > 0 && currenttime >= startSpawnZone && (int) currenttime % delaySpawnZone == 0 && !alreadyDoZone)
        {
            selectionZone = ZoneActivation(listZone);
            Debug.Log("On active le " + selectionZone);
            gameObject.transform.GetChild(selectionZone).gameObject.SetActive(true);
            alreadyDoZone = true;
        }
            
        if ((int) currenttime % delaySpawnHemi != 0 && alreadyDoHemi)
            alreadyDoHemi = false;

        if (((int) currenttime % delaySpawnZone != 0 && alreadyDoZone))
            alreadyDoZone = false;
        
    }

    public (int, int) HemiActivation(List<int> list)
    {
        int tmp = UnityEngine.Random.Range(0, list.Count-2); // -2

        int index = list[tmp];
        list.RemoveAt(tmp);
        
        tmp = UnityEngine.Random.Range(25, 40);
        
        return (index, tmp);
    }

    public int ZoneActivation(List<int> list)
    {
        int  tmp = UnityEngine.Random.Range(0, list.Count-2);

        int index = list[tmp];
        list.RemoveAt(tmp);
        return index;
    }
}