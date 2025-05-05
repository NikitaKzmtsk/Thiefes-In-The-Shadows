using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

public class Menuscript : MonoBehaviour
{
    public GameObject pausePanel; 
    private bool isPaused = false; 
    public MonoBehaviour playerControl;
    public MonoBehaviour player2;
    public bool isFullScreen = true;
    public AudioMixer am;
    void Start()
    {
        HideCursor(); 
    }

    private bool escapePressedLastFrame = false;
    void Update()
    {
        bool escapePressedThisFrame = Input.GetKeyDown(KeyCode.Escape);

        if (escapePressedThisFrame && !escapePressedLastFrame)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        escapePressedLastFrame = escapePressedThisFrame;
    }

    public void Pause()
    {
        pausePanel.SetActive(true); 
        
        isPaused = true; 
        ShowCursor(); 
        DisablePlayerControl(); 
    }

    public void Resume()
    {
        var panels = GameObject.FindGameObjectsWithTag("MenuPanel");
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
        //pausePanel.SetActive(false); 
        
        isPaused = false; 
        HideCursor(); 
        EnablePlayerControl(); 

    }

    public void QuitGame()
    {
        
        Application.Quit();
        
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 
    }

    private void DisablePlayerControl()
    {
        if (playerControl != null || player2 != null)
        {
            playerControl.enabled = false;
            player2.enabled = false;
        }
    }

    private void EnablePlayerControl()
    {
        if (playerControl != null || player2 != null)
        {
            playerControl.enabled = true; 
            player2.enabled = true;
        }
    }

    public void FromPauseToMenu()
    {
        SceneManager.LoadScene("menu");
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
