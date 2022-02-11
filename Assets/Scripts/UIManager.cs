using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(PauseMenuControls))]
public class UIManager : Singleton<UIManager>
{
    
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject stopButton;
    private HumanParent parentSpeed;
    [SerializeField] private GameObject tryAgainPanel;
    [SerializeField] private GameObject resumePanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button pauseButton;
    private PauseMenuControls _pauseMenuControls;
    
    public bool finished = false;

    private void OnEnable()
    {
        GameManager.Failed += Faileds;
        GameManager.Victory += Victory;
    }

    private void OnDisable()
    {
        GameManager.Failed -= Faileds;
        GameManager.Victory -= Victory;
    }
    void Start()
    {
        parentSpeed = FindObjectOfType<HumanParent>();
        Time.timeScale = 1f;
        restartButton.onClick.AddListener(Restart);
        pauseButton.onClick.AddListener(pause);
        _pauseMenuControls = GetComponent<PauseMenuControls>();
    }
    
    public void Victory()
    {
        
        Human.Instance.GetComponent<Animator>().SetBool("victory",true);
        Debug.Log("parentspeed:"+parentSpeed);
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
    public void Restart() {
        Application.LoadLevel(Application.loadedLevel);
        if(tryAgainPanel.activeSelf)
            tryAgainPanel.gameObject.SetActive(false);
        resume();
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
