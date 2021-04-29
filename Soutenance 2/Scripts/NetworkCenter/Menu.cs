using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuEnum
{
    LOADING_SCREEN = 0,
    ERROR_SCREEN = 1,

    HOME_TITLE_SCREEN = 2,
    HOME_OPTIONS_SCREEN = 3,
    HOME_PLAY_SCREEN = 4,
    
    MULTIPLAYER_TITLE_SCREEN = 5,
    MULTIPLAYER_HOME_SCREEN = 6,
    MULTIPLAYER_ROOM_SCREEN = 7,
    
    MULTIPLAYER_PRIV_TITLE_SCREEN = 8,
    MULTIPLAYER_PRIV_FIND_SCREEN = 9,
    MULTIPLAYER_PRIV_CREATE_SCREEN = 10,
    
    INGAME_PLAY_SCREEN = 11,
    INGAME_OPTIONS_SCREEN = 12,
    
    NONE
}

public class Menu : MonoBehaviour
{
    public MenuEnum MenuID;

    public bool isOpened()
    {
        return gameObject.activeSelf;
    }
    
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
