using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

    public int borderSide;
    public Frame frame;
    private bool isTriggered;

    void Start()
    {
        TextMesh label = GetComponentInChildren<TextMesh>();
        if (label != null)
        {
            label.text = "" + frame.borderLabels[borderSide];
        }

        isTriggered = false;
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
        if(!frame.frameManager.DidKnightGetSword)
        {
            frame.frameManager.SwitchHeroFrame(borderSide);
        }
        else
        {
            frame.frameManager.MovePrincessAndKnight(borderSide);
        }
        isTriggered = false;
    }
}