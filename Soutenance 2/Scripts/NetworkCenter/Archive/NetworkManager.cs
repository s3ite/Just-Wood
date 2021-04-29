using System;
using UnityEngine;
using System.Collections.Generic;

namespace Script.NetworkCenter
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager instance;

        [Header("Player Settings")]
        public GameObject playerPrefab;
        public Transform spawnPoint;

        [Header("Network Settings")]
        public int MyEntityID = -1;
        
        public Dictionary<int, Player> PlayerList = new Dictionary<int, Player>();
        

        public void Awake()
        {
            instance = this;
        }
        
        public void Start()
        { 
            DontDestroyOnLoad(this);
            
            Network.InitNetwork();
            Network.ConnectToServer();
        }

        public void OnApplicationQuit()
        {
            Network.DisconnectFromServer();
        }

        void Update()
        {
           
        }
        
    }
}