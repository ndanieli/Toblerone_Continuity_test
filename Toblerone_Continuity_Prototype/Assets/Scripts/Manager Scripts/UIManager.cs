using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject gameOverPanel;
    public GameObject startGamePanel;

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void HideStartGamePanel()
    {
        startGamePanel.SetActive(false);
    }
}
