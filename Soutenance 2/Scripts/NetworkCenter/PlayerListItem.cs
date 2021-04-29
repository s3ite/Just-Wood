using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text text;
    private Photon.Realtime.Player player;
    
    public void SetUp(Photon.Realtime.Player player)
    {
        this.player = player;
        text.text = player.NickName;
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player player1)
    {
        if (player1 == player)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
