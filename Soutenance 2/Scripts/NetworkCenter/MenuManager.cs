using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance;
    
    //private AudioManager audioManager;
    
    public void Awake()
    {
        Instance = this;
        
    // son
        
 

    //audioManager = FindObjectOfType<AudioManager>();
    //audioManager.Play("MenuPlay");
    }

    [SerializeField] public Menu[] menus;

    public void OpenMenu(MenuEnum ID)
    {
        foreach (var m in menus)
        {
            if (m.MenuID.Equals(ID))
            {
                m.Open();
            }
            else
            {
                m.Close();
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        menu.Open();

        foreach (var m in menus)
        {
            if (!m.MenuID.Equals(menu.MenuID))
            {
                m.Close();
            }
        }
    }
    
    
    
    public void PlayAudioMenuMenu()
    {
        if (AudioManager.instance) AudioManager.instance.Play("Menu");
        AudioManager.instance.Pause("MenuOption");
        AudioManager.instance.Pause("MenuPlay");
        AudioManager.instance.Pause("MenuPlay");

    }
    
    
    public void PlayAudioMenuPlay()
    {
        if (AudioManager.instance) AudioManager.instance.Play("MenuPlay");
        AudioManager.instance.Pause("MenuOption");
    }
    
    public void PlayAudioMenuOption()
    {
        AudioManager.instance.Pause("MenuPlay");
        AudioManager.instance.Pause("Menu");
        if (AudioManager.instance) AudioManager.instance.Play("MenuOption");
    }
    
    public void PlayAudioTouchEnter()
    {
        if (AudioManager.instance) AudioManager.instance.Play("EnterTouch");
    }
    public void PlayAudioTouchBack()
    {
        if (AudioManager.instance) AudioManager.instance.Play("BackTouch");
    }
    
    
}
