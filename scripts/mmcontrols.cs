using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Unity.VisualScripting;


public class mmcontrols : MonoBehaviour
{
    public bool isFullScreen = true;
    public AudioMixer am;
    public void PlayPressed()
    {
        SceneManager.LoadScene("Lobby");
    }
    
    public void ExitPressed()
    {
        Application.Quit();
        
    }
    public void FullScreenToggle()
    {
        
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
    }
}

