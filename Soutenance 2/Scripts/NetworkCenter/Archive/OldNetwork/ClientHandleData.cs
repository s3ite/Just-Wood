/*using System;
using JustHood_SERVER.NetworkCenter;
using UnityEngine;
using System.Collections.Generic;

using UnityEngine.UI;

public class ClientHandleData : MonoBehaviour
{
    public static ClientHandleData instance;
 
    private delegate void Packet(byte[] Data);
    private Dictionary<int, Packet> Packets;

    [Header("PlayerPrefab")] 
    public GameObject playerPrefab;
    
    public void InitMessages()
    {
        Packets = new Dictionary<int, Packet>();
        Packets.Add((int) ServerPackets.SAlertMsg, HandleAlertMsg);
        Packets.Add((int) ServerPackets.SPlayerMovement, HandleMovement);
        Packets.Add((int) ServerPackets.SPlayerDatas, HandlePlayerDatas);
        Packets.Add((int) ServerPackets.SPlayerOwnDatas, HandlePlayerDatas);
    }
    
    private void Awake()
    {
        instance = this;
        InitMessages();
    }

    public void HandleData(byte[] data)
    {
        if (data == null)
            return;
        int packetnum;

        Packet Packet;
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        packetnum = buffer.ReadInteger();

        Debug.Log("Packet recu: "+packetnum);
        
        if (packetnum == 0)
        {
            return;
        }

        if (Packets.TryGetValue(packetnum, out Packet))
        {
            Packet.Invoke(data);
        }
     
    }
    
    void HandleMovement(byte[]data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);

        int packetNum = buffer.ReadInteger();

        //Player Pos
        float x = buffer.ReadFloat();
        float y = buffer.ReadFloat();
        float z = buffer.ReadFloat();
        
        //Player Rotation
        float rotX = buffer.ReadFloat();
        float rotY = buffer.ReadFloat();
        float rotZ = buffer.ReadFloat();
        float rotW = buffer.ReadFloat();
        
        //Player Animation
        float horizontalPress = buffer.ReadFloat();
        float verticalPress = buffer.ReadFloat();
        bool spacePress = buffer.ReadBool();

        //Player Info
        int EntityID = buffer.ReadInteger();
        
        Player p = Player.GetPlayerFromEntityID(EntityID);
        GameObject obj = p.obj;
        
        if (obj != null)
        {
            p.obj.GetComponent<DeplacementElfe>().MoveTo(new Vector3(x,y,z), new Quaternion(rotX, rotY, rotZ, rotW), horizontalPress, verticalPress, spacePress);
        }
    }

    void HandlePlayerDatas(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);

        int packetNum = buffer.ReadInteger(); 
        int ID = buffer.ReadInteger();
        
        Player p = Player.GetPlayerFromEntityID(ID);
        
        p.EntityID = ID;
        
        p.posX = buffer.ReadFloat();
        p.posY = buffer.ReadFloat();
        p.posZ = buffer.ReadFloat();

        if (p.obj != null)
            Destroy(p.obj);
        
        p.obj = Instantiate(playerPrefab, new Vector3(p.posX, p.posY, p.posZ), Quaternion.identity);
        p.obj.GetComponent<DeplacementElfe>().EntityID= ID;
        p.obj.name = "Player: " + ID;

        if (packetNum == (int) ServerPackets.SPlayerOwnDatas)
        { 
            Network.instance.MyEntityID = ID;
        }
        else
        {
            p.obj.GetComponentInChildren<Camera>().enabled = false;
            p.obj.GetComponentInChildren<AudioListener>().enabled = false;
            p.obj.GetComponentInChildren<CharacterController>().enabled = false;
            p.obj.GetComponentInChildren<Minimap_script>().enabled = false;
        }
    }

    void HandleAlertMsg(byte[]data)
    {
        int packetnum;
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        packetnum = buffer.ReadInteger();

        string AlertMsg = buffer.ReadString();

        Debug.Log(AlertMsg);
    }
    

}*/
