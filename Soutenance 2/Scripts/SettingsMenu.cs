﻿ using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
 using UnityEngine.UI;
 using System.Linq;

 public class SettingsMenu : MonoBehaviour
{
   public AudioMixer audiomixer;
   public bool CanEditResolution = true;
   public Dropdown resolutionDropdown;
   
   Resolution[] resolutions;

   public void Start()
   {
       if (!CanEditResolution) 
           return;
       
       resolutions = Screen.resolutions.Select(resolution=>new Resolution{width = resolution.width,height = resolution.height}).Distinct().ToArray();;
       resolutionDropdown.ClearOptions();

       List<string> options = new List<string>();
       int currentResolutionIndex = 0;
       for (int i = 0; i < resolutions.Length; i++)
       {
           string option = resolutions[i].width + "x" + resolutions[i].height;
           options.Add(option);
           if (resolutions[i].width==Screen.width && resolutions[i].height==Screen.height)
           {
               currentResolutionIndex = i;
           }
       }
       resolutionDropdown.AddOptions(options);
       resolutionDropdown.value = currentResolutionIndex;
       resolutionDropdown.RefreshShownValue();

       // son
       AudioManager.instance.Play("Menu");
   }

   public void SetVolume(float volume)
   {
       //audiomixer.SetFloat("volume",volume);
       
       if (AudioManager.instance)
           AudioManager.instance.mixerGroup.audioMixer.SetFloat("volume",volume);
   }

   public void SetFullScreen(bool isFullscreen)
   {
       Screen.fullScreen = isFullscreen;
   }

   public void SetResolution(int resolutionIndex)
   {
       Resolution resolution = resolutions[resolutionIndex];
       Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
   }
}
