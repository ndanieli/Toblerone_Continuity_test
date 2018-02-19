using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {

    public int borderSide;
    public Frame frame;

    private void OnTriggerEnter2D(Collider2D other)
    {
      //  Debug.Log("Enter");
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        if (other.tag.Equals("Player"))
        {
            frame.OnBorderTrigger(borderSide);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
//        Debug.Log("Exit");
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
