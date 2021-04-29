


using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Script.NetworkCenter
{
    public class NetworkPhoton : MonoBehaviourPunCallbacks
    {
        public static NetworkPhoton Instance;

        [Header("Charcter Selection Settings")]
        [SerializeField] private GameObject SpawnPoint;
        [SerializeField] public GameObject[] chacracters;
        private int selectedCharacter = 0;

            [Header("Playername Input Field")]
        [SerializeField] private TMP_InputField playernameText;
        
        [Header("ErrorText Field")]
        [SerializeField] private TMP_Text errorText;
        
        [Header("Room Fields")]
        [SerializeField] private TMP_Text roomCellText;
        [SerializeField] private Transform playerListContent;
        [SerializeField] private GameObject playerCellPrefab;
        [SerializeField] private GameObject ReadyButton;
        [SerializeField] private GameObject NotReadyButton;

        [Header("Find Room Fields")]
        [SerializeField] private GameObject roomCellPrefab;
        [SerializeField] private Transform gameList;

        public int PlayersReadyCounter = 0;
        public int MaxPlayers = 14;
        private TypedLobby randomRoomTyped = new TypedLobby("random", LobbyType.Default);
        private TypedLobby privateRoomTyped = new TypedLobby("private_room", LobbyType.Default);
        private bool ModifProfil = true;
        
        public void CharacterSelectionNext()
        {
            chacracters[selectedCharacter].SetActive(false);
            selectedCharacter = (selectedCharacter + 1) % chacracters.Length;
            chacracters[selectedCharacter].SetActive(true);
        }
        
        public void CharacterSelectionPrevious()
        {
            chacracters[selectedCharacter].SetActive(false);
            if (--selectedCharacter < 0)
                selectedCharacter = chacracters.Length-1;
            chacracters[selectedCharacter].SetActive(true);
            
        }

        public void Start()
        {
            for (int i = 0; i < chacracters.Length; i++)
            {
                GameObject obj = chacracters[i];
                obj = Instantiate(obj, SpawnPoint.transform.position, new Quaternion(0, 180f, 0, 0), MenuManager.Instance.menus[5].transform);
                obj.SetActive(i == 0);
                chacracters[i] = obj;
            }
        }

        public void Awake()
        {
            Instance = this;
        }

        public void OnApplicationQuit()
        {
            DisconnectNetwork();
        }

        public void Update()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                
            }
        }

        public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);
        }

        public void OnClickOnBeReady()
        {
            if (PhotonNetwork.IsMasterClient)
                AddBeReady();
            else
                RoomManager.Instance.photonView.RPC("AddBeReady", RpcTarget.MasterClient);
            
            ReadyButton.SetActive(false);
            NotReadyButton.SetActive(true);
            
            PhotonNetwork.LocalPlayer.CustomProperties.Add("isReady", true);
        }

        public void OnClickOnBeNotReady()
        {
            if (PhotonNetwork.IsMasterClient)
                AddBeNotReady();
            else
                RoomManager.Instance.photonView.RPC("AddBeNotReady", RpcTarget.MasterClient);
            
            NotReadyButton.SetActive(false);
            ReadyButton.SetActive(true);
            PhotonNetwork.LocalPlayer.CustomProperties.Add("isReady", false);
        }

        public void AddBeReady()
        {
            PlayersReadyCounter += 1;
            
            if (PlayersReadyCounter >= PhotonNetwork.PlayerList.Length-1 && PhotonNetwork.IsMasterClient)
                StartGame();
        }
        
        public void AddBeNotReady()
        {
            PlayersReadyCounter -= 1;
        }
        
        public bool DisconnectNetwork()
        {
            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.InRoom)
                    PhotonNetwork.LeaveRoom();
                if (PhotonNetwork.InLobby)
                    PhotonNetwork.LeaveLobby();
                
                PhotonNetwork.Disconnect();
                return true;
            }
            return false;
        }
        
        public void ConnectToLobby()
        {
            MenuManager.Instance.OpenMenu(MenuEnum.LOADING_SCREEN);
            Debug.Log("Connecting to Network...");
            PhotonNetwork.ConnectUsingSettings();
            ModifProfil = true;
        }
        
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Network !");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnJoinedLobby()
        {
            if (ModifProfil)
            {
                MenuManager.Instance.OpenMenu(MenuEnum.MULTIPLAYER_TITLE_SCREEN);  
            }
            else
            {
                MenuManager.Instance.OpenMenu(MenuEnum.MULTIPLAYER_HOME_SCREEN);
            }
            
            Debug.Log("Joined Lobby !");
        }

        public void JoinMultiplayerHome()
        {
            if (playernameText.text == null || playernameText.text.Equals(""))
                return;
            
            ModifProfil = false;
            PhotonNetwork.NickName = playernameText.text;
            
            Hashtable hash = new Hashtable();
            hash.Add("selectedCharacter", selectedCharacter);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

            MenuManager.Instance.OpenMenu(MenuEnum.MULTIPLAYER_HOME_SCREEN);
        }
        
        public void JoinRandom()
        {
            Debug.Log("Connecting to a Random Room...");
            MenuManager.Instance.OpenMenu(MenuEnum.LOADING_SCREEN);
            PhotonNetwork.JoinRandomRoom(null, (byte) MaxPlayers, MatchmakingMode.RandomMatching, randomRoomTyped, null);
        }
        
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
 
            if (message.Equals("No match found"))
            {
                RoomOptions roomOptions = new RoomOptions{MaxPlayers = (byte) MaxPlayers, IsOpen = true, IsVisible = true};
                PhotonNetwork.CreateRoom(GenerateRoomName(), roomOptions, randomRoomTyped);   
            }
            else
            {
                errorText.text = "#01 Room Creation Failed: "+message;
                MenuManager.Instance.OpenMenu(MenuEnum.ERROR_SCREEN);
            }

        }

        private string GenerateRoomName()
        {
            Random rand = new Random();
            return rand.Next(90000).ToString("00000");
        }
        
        public void CreateRoom()
        {
            MenuManager.Instance.OpenMenu(MenuEnum.LOADING_SCREEN);
            RoomOptions roomOptions = new RoomOptions{MaxPlayers = (byte) MaxPlayers, IsOpen = true, IsVisible = true};
            PhotonNetwork.CreateRoom(GenerateRoomName(), roomOptions, TypedLobby.Default);   
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("update rooms: "+ roomList.Count);
            foreach (Transform trans in gameList)
            {
                Destroy(trans.gameObject);
            }

            foreach (var r in roomList)
            {
                if (r.RemovedFromList)
                    continue;
                Instantiate(roomCellPrefab, gameList).GetComponent<RoomListItem>().SetUp(r);
            }
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Room Joined : "+ PhotonNetwork.CurrentRoom.Name);
            MenuManager.Instance.OpenMenu(MenuEnum.MULTIPLAYER_ROOM_SCREEN);
            roomCellText.text = "Room #"+PhotonNetwork.CurrentRoom.Name;

            PhotonNetwork.LocalPlayer.CustomProperties.Add("isReady", true);
            
            foreach (Transform t in playerListContent)
                Destroy(t.gameObject);

            foreach (var p in PhotonNetwork.PlayerList)
                Instantiate(playerCellPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(p);

            if (PhotonNetwork.PlayerList.Length == 99)
                StartGame();
            
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            Instantiate(playerCellPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        }

        public void BackToMain()
        {
            MenuManager.Instance.OpenMenu(MenuEnum.LOADING_SCREEN);
            Debug.Log("Disconnect from the Network");
            
            if (!DisconnectNetwork())
                MenuManager.Instance.OpenMenu(MenuEnum.HOME_TITLE_SCREEN);

        }
        
        public void BackToMainMultiplayer()
        {
            MenuManager.Instance.OpenMenu(MenuEnum.LOADING_SCREEN);
            ModifProfil = true;
            OnJoinedLobby();
        }

        public void StartGame()
        {
            PhotonNetwork.LoadLevel(1);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            MenuManager.Instance.OpenMenu(MenuEnum.HOME_TITLE_SCREEN);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            errorText.text = "#01 Room Creation Failed: "+message;
            MenuManager.Instance.OpenMenu(MenuEnum.ERROR_SCREEN);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            MenuManager.Instance.OpenMenu(MenuEnum.LOADING_SCREEN);
        }

        public override void OnLeftRoom()
        {
            if ((bool) PhotonNetwork.LocalPlayer.CustomProperties["isReady"])
            {
                RoomManager.Instance.photonView.RPC("AddBeNotReady", RpcTarget.MasterClient);
            }
            
            PhotonNetwork.LocalPlayer.CustomProperties.Remove("isReady");
            Debug.Log("Room left !");
   //         MenuManager.Instance.OpenMenu(MenuEnum.MULTIPLAYER_HOME_SCREEN);
        }

        public void JoinRoom(RoomInfo info)
        {
            PhotonNetwork.JoinRoom(info.Name);
            MenuManager.Instance.OpenMenu(MenuEnum.LOADING_SCREEN);
        }
        
    }

}