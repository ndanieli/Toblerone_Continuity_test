using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

    public int borderSide;
    public Frame frame;

    void Start()
    {
        TextMesh label = GetComponentInChildren<TextMesh>();
        if (label != null)
        {
            label.text = "" + frame.borderLabels[borderSide];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            frame.frameManager.SwitchHeroFrame(borderSide);
        }
    }

}