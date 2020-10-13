using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropDown;
    private Resolution[] availableResolutions;

    public Slider musicSlider;
    public Slider sfxSlider;

    public void Start()
    {
        
        audioMixer.GetFloat("musicVolume", out float musicValueForSlider) ;
        musicSlider.value = musicValueForSlider ;
        audioMixer.GetFloat("sfxVolume", out float sfxValueForSlider) ;
        sfxSlider.value = sfxValueForSlider;
        
        // Dans la liste de toutes les résolutions supportées par l'écran (qui contient des duplicatas), on choisit des résolutions distinctes en discriminant sur le couple width;height et on rentre le tout dans une liste.
        availableResolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height= resolution.height }).Distinct().ToArray() ;
        resolutionDropDown.ClearOptions() ;

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0 ; i < availableResolutions.Length ; i++)
        {
            string option = availableResolutions[i].width + "x" + availableResolutions[i].height ;
            options.Add(option) ;

            if (availableResolutions[i].width==Screen.width && availableResolutions[i].height==Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options) ;
        resolutionDropDown.value = currentResolutionIndex ;
        resolutionDropDown.RefreshShownValue() ;

        Screen.fullScreen = true ;
    }
    
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume) ;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume) ;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen=isFullScreen ;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = availableResolutions[resolutionIndex] ;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen) ;
    }

}
