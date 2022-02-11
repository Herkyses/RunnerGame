using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuControls : MonoBehaviour
{
    public GameObject pauseButtonGO;
    public GameObject resumePanel;
    public Button resumeButton;
    public Button restartButton;
    
    void Start()
    {
        Time.timeScale = 1f;

        resumeButton.onClick.AddListener(Resume);
        restartButton.onClick.AddListener(Restart);
    }

    void Restart()
    {
        ToggleMenu();
        UIManager.Instance.Restart();
    }

    private void Awake()
    {
        resumePanel.transform.localScale = Vector3.zero;
    }

    public void Resume()
    {
        UIManager.Instance.resume();
        ToggleMenu();
    }

    public void ToggleMenu()
    {
        pauseButtonGO.transform.localScale = new Vector3(2f, 2f, 2f);
        resumePanel.transform.localScale = new Vector3(0, 0, 0);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        UIManager.Instance.pause();
        pauseButtonGO.transform.localScale = new Vector3(0, 0, 0);
        resumePanel.transform.localScale = new Vector3(1f, 1f, 1f);
        Time.timeScale = 0f;
    }
}