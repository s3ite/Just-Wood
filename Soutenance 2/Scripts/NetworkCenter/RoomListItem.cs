using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Script.NetworkCenter;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private RoomInfo Info;
    
    public void SetUp(RoomInfo info)
    {
        Info = info;
        text.text = "#"+info.Name+" | "+info.PlayerCount+"/"+info.MaxPlayers;
    }

    public void onClick()
    {
        NetworkPhoton.Instance.JoinRoom(Info);
    }

}
