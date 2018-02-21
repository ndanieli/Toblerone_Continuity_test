using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject zoomOutCamera;
    private bool zoomIn;

	// Use this for initialization
	void Start () {
        zoomIn = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            if (zoomIn)
            {
                zoomOutCamera.SetActive(true);
                zoomIn = false;
                Debug.Log("Zoom out");
            } else
            {
                zoomOutCamera.SetActive(false);
                zoomIn = true;
                Debug.Log("Zoom in");
            }
        }


    }
}
