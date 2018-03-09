using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour {

    public FramesManager framesManager;
    public CameraControl cameraManager;
    public PuzzleManager puzzleManager;
    public ImageManager imageManager;
    public GameObject hero;
    public GameObject virgin;

    public void KillHero()
    {
        Debug.Log("Kill Hero");
        imageManager.EnableScreenLoseImage();
    }

    public void LevelCompleted()
    {
        Debug.Log("Level completed");
        imageManager.EnableScreenWinImage();
    }
}
