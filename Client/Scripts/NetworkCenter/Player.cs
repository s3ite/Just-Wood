using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player
{
    public static Player GetPlayerFromIndex(int index)
    {
        return Network.PlayerList[index];
    }

    //General
    public int Index;
    
    //Position
    public float posX;
    public float posY;
    public float posZ;
    
    //Rotation
    public float rotX;
    public float rotY;
    public float rotZ;
    public float rotW;

    public GameObject obj;


}
