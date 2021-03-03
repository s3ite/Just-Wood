using System;
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
        buffer = null;
        if (packetnum == 0)
            return;
        
        if (Packets.TryGetValue(packetnum, out Packet))
        {
            Packet.Invoke(data);
        }
        else
        {
            Debug.Log("packet inconnu "+packetnum);
        }
    }
    
    void HandleMovement(byte[]data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);

        int packetNum = buffer.ReadInteger(); 
        
        //Player Info
        int index = buffer.ReadInteger();

        //Player Pos
        float horizontalPress = buffer.ReadFloat();
        float verticalPress = buffer.ReadFloat();
        float spacePress = buffer.ReadFloat();

        //Player Rotation
        /*
        float rotX = buffer.ReadFloat();
        float rotY = buffer.ReadFloat();
        float rotZ = buffer.ReadFloat();
        float rotW = buffer.ReadFloat();*/
        
        Player p = Player.GetPlayerFromIndex(index);
        
        GameObject obj = p.obj;
        
        if (obj != null)
        {
            p.obj.GetComponent<Deplacement>().MoveTo(horizontalPress, verticalPress, spacePress > 0.5f);
        }
        
    }

    void HandlePlayerDatas(byte[] data)
    {
        
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);

        int packetNum = buffer.ReadInteger(); 
        int i = buffer.ReadInteger();

        if (Datas.instance.MyIndex < 1)
        {
            Datas.instance.MyIndex = i;
        }

        Debug.Log("player init "+i);
        Player p = Player.GetPlayerFromIndex(i);
        
        p.Index = i;
        
        p.posX = buffer.ReadFloat();
        p.posY = buffer.ReadFloat();
        p.posZ = buffer.ReadFloat();

        if (p.obj != null)
            Destroy(p.obj);
        
        p.obj = Instantiate(playerPrefab, new Vector3(p.posX, p.posY, p.posZ), Quaternion.identity);
        p.obj.GetComponent<Deplacement>().Index = i;
        p.obj.name = "Player: " + i;

        if (i != Datas.instance.MyIndex)
        {
            p.obj.GetComponentInChildren<Camera>().enabled = false;
            p.obj.GetComponentInChildren<AudioListener>().enabled = false;
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
    

}
