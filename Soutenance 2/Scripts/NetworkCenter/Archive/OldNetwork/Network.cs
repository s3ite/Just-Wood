/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public class Network : MonoBehaviour
{
    
    public static Network instance;

    [Header("Player Settings")]
    public GameObject playerPref;
    public Transform spawnPoint;

    [Header("Network Settings")]
    public string ServerIP = "37.187.9.46";
    public int ServerPort = 35565;
    public bool isConnected;

    public static Socket _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    
    public bool shouldHandleData;
    private byte[] data;

    public Dictionary<int, Player> PlayerList = new Dictionary<int, Player>();
    public int MyEntityID = -1;

    private void Awake()
    {
        instance = this;
        
        for (int i = 0; i < 64; i++)
        {
            PlayerList[i] = new Player();
        }
    }

    // Use this for initialization
    void Start()
    {
        ConnectGameServer();
    }

    void ConnectGameServer()
    {
        _client.BeginConnect(ServerIP, ServerPort, new AsyncCallback(ConnectCallback), _client);
    }

    void ConnectCallback(IAsyncResult result)
    {
        _client.EndConnect(result);
        isConnected = true;

        while (isConnected)
        {
            OnReceive();    
        }
    }

    private void Update()
    {
        if (shouldHandleData)
        {
            ClientHandleData.instance.HandleData(data);
            shouldHandleData = false;
        }
    }

    private void OnApplicationQuit()
    {
        isConnected = false;
        _client.Close();
    }

    void OnReceive()
    {
        byte[] _sizeinfo = new byte[4];
        byte[] _receivedbuffer = new byte[1024];

        int totalread = 0, currentread = 0;

        try
        {
            currentread = totalread = _client.Receive(_sizeinfo);
            if (totalread <= 0)
            {
                isConnected = false;
                Debug.Log("You got disconnected from the Server.");
            }
            else
            {
                while (totalread < _sizeinfo.Length && currentread > 0)
                {
                    currentread = _client.Receive(_sizeinfo, totalread, _sizeinfo.Length - totalread, SocketFlags.None);
                    totalread += currentread;
                }

                int messagesize = 0;
                messagesize |= _sizeinfo[0];
                messagesize |= _sizeinfo[1] << 8;
                messagesize |= _sizeinfo[2] << 16;
                messagesize |= _sizeinfo[3] << 24;

                data = new byte[messagesize];
                totalread = 0;
                currentread = totalread = _client.Receive(data, totalread, data.Length - totalread, SocketFlags.None);

                while (totalread < messagesize && currentread > 0)
                {
                    currentread = _client.Receive(data, totalread, data.Length - totalread, SocketFlags.None);
                    totalread += currentread;
                }

                shouldHandleData = true;
            }
            
        }
        catch (Exception e)
        {
            isConnected = false;
            Debug.Log("You got disconnected from the Server.");
        }
    }
   
}*/
