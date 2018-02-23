using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject zoomOutCamera;
    public bool zoomIn;
    public GameObject hero;

    private Vector2 linearBackup;
    private float angularBackup;
    // Use this for initialization
    void Start () {
        zoomIn = false;
        
        // stores the hero's velocity and angular velocity for un-freezing
        linearBackup = new Vector2();
        angularBackup = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            zoomInOut();
        }
    }

    void zoomInOut()
    {
        if (zoomIn)
        {
            zoomOutCamera.SetActive(true);
            zoomIn = false;
            linearBackup = hero.GetComponent<Rigidbody2D>().velocity;
            angularBackup = hero.GetComponent<Rigidbody2D>().angularVelocity;
            hero.GetComponent<HeroMovement>().FreezeHero();
            //Debug.Log("Zoom out");
        }
        else
        {
            zoomOutCamera.SetActive(false);
            zoomIn = true;
            hero.GetComponent<HeroMovement>().UnFreezeHero(linearBackup, angularBackup);
            //Debug.Log("Zoom in");
        }
    }
}
