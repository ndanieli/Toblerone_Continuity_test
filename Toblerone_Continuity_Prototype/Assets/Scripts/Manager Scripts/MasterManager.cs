using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MasterManager : MonoBehaviour {

    public FramesManager framesManager;
    public CameraControl cameraManager;
    public PuzzleManager puzzleManager;
    public ImageManager imageManager;
    public UIManager UIManager;
    public GameObject hero;
    public GameObject virgin;
    public int LevelNumber;

    public void KillHero()
    {
        Debug.Log("Kill Hero");
        imageManager.EnableScreenLoseImage();
        GameOverScreen();
    }

    public void LevelCompleted()
    {
        Debug.Log("Level completed");
        imageManager.EnableScreenWinImage();
    }

    public void GameOverScreen()
    {
        UIManager.ShowGameOverPanel();
    }

    public void RespawnHeroAfterDeath()
    {
        framesManager.PlaceHeroAfterDeath();
        UIManager.HideGameOverPanel();
        imageManager.DisableScreenLoseImage();
    }

    public void StartGame()
    {
        UIManager.HideStartGamePanel();
    }
}
