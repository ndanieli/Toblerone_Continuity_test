using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

    public int borderSide;
    public Frame frame;
    private bool isTriggered;
    private MasterManager masterManager;

    void Start()
    {
        TextMesh label = GetComponentInChildren<TextMesh>();
        if (label != null)
        {
            label.text = "" + frame.borderLabels[borderSide];
        }

        isTriggered = false;
        masterManager = FindObjectOfType<MasterManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Triggered");
        if (other.tag.Equals("Player") && !isTriggered)
        {
            isTriggered = true;
            moveHeroToNextFrame();
        }
    }

    private void moveHeroToNextFrame()
    {
        frame.frameManager.SwitchHeroFrame(borderSide);
        isTriggered = false;
    }
}