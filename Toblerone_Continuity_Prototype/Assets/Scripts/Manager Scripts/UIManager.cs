﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject gameOverPanel;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

}
