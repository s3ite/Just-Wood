using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTroll : MonoBehaviour
{
    public GameObject troll;
    public GameObject SpawnZone;
    
    private int i;
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float x = SpawnZone.transform.position.x;
        float z = SpawnZone.transform.position.z;



        bool PressA = Input.GetKeyDown("d");
        
        if (PressA)
        {
            SpawnMyPlayer(x, z);
        }
    }

    private void SpawnMyPlayer(float x, float z)
    {
        GameObject trollClone = Instantiate(troll, new Vector3(x, 0, z), troll.transform.rotation );
        
      /*  trollClone.transform.parent = trollContainer.transform;
        trollClone.name = "Troll numero " + i;
        i++;
        */
    }
}
