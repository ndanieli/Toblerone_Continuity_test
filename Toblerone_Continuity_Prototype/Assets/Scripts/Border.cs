using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

    public int borderSide;
    public Frame frame;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            frame.OnBorderTrigger(borderSide);
        }
    }

}