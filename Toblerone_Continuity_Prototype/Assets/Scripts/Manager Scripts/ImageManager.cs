using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour {

    public GameObject EndScreenWin;
    public GameObject EndScreenLose;

    // Use this for initialization
    void Start () {
        EndScreenWin.GetComponent<Image>().enabled = false;
        EndScreenLose.GetComponent<Image>().enabled = false;
    }
	
	public void EnableScreenWinImage()
    {
        EndScreenWin.GetComponent<Image>().enabled = true;
    }

    public void EnableScreenLoseImage()
    {
        EndScreenLose.GetComponent<Image>().enabled = true;
    }
}
