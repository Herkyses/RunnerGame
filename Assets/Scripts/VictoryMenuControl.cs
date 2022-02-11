using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryMenuControl : Singleton<VictoryMenuControl>
{
    public Button restartButton;

    public GameObject victoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(RestartFromVictory);
    }

    public void RestartFromVictory()
    {
        Application.LoadLevel(Application.loadedLevel);
        victoryPanel.SetActive(false);
    }
    
}
