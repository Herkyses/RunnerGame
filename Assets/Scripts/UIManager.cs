using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    
    public GameObject finishPanel;
    public GameObject stopButton;
    private HumanParent parentSpeed;
    public GameObject tryAgainPanel;
    public GameObject resumePanel;
    public bool finished = false;
    void Start()
    {
        parentSpeed = FindObjectOfType<HumanParent>();
        Time.timeScale = 1f;
    }
    
    public void Victory()
    {
        
        Human.Instance.GetComponent<Animator>().SetBool("victory",true);
        parentSpeed.speed = 0f;
        finishPanel.SetActive(true);
        stopButton.SetActive(false);
        
    }

    public void Faileds()
    {
        stopButton.transform.localScale = new Vector3(0, 0, 0);
        tryAgainPanel.SetActive(true);
        Human.Instance.finishControl = true;
        parentSpeed.speed = 0f;
        
        
        
    }
    public void tryAgain() {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void resume() {
        stopButton.transform.localScale = new Vector3(2f, 2f, 2f);
        resumePanel.transform.localScale = new Vector3(0, 0, 0);
        Time.timeScale = 1f;
    }
    public void pause() {
        stopButton.transform.localScale = new Vector3(0, 0, 0);
        resumePanel.transform.localScale = new Vector3(1f, 1f, 1f);
        Time.timeScale = 0f;
    }
    
    
}
