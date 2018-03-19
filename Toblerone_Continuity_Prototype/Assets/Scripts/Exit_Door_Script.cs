using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Door_Script : MonoBehaviour {

    private bool done;

    private void Start()
    {
        done = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!done)
        {
            done = true;
            GameObject.FindObjectOfType<MasterManager>().LevelCompleted();
        }
    }
}
