using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Script.NetworkCenter;

public class Player
{
    public static Player GetPlayerFromEntityID(int EntityID)
    {
        if (NetworkManager.instance.PlayerList.ContainsKey(EntityID))
            return NetworkManager.instance.PlayerList[EntityID];
     
        Player p = new Player();
        NetworkManager.instance.PlayerList.Add(EntityID, p);

        return p;
        
    }

    //General
    public int EntityID;
    
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
