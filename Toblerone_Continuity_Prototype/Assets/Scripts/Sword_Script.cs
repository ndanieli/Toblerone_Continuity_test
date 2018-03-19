using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Script : MonoBehaviour {

    private MasterManager masterManager;
    public int initialRow;
    public int initialCol;

    private void Start()
    {
        masterManager = FindObjectOfType<MasterManager>();
        initialRow++;
        initialCol++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (areBothPrincessAndKnightInTheSameRoom())
        {
            FindObjectOfType<knight_images>().ChangeKnightSpriteToKnightWithSword();
            FindObjectOfType<FramesManager>().SetKnightGotSword();
            gameObject.SetActive(false);
        }
    }

    private bool areBothPrincessAndKnightInTheSameRoom()
    {
        bool result = false;
        if (masterManager.framesManager.ActiveFrame.row == initialRow && masterManager.framesManager.ActiveFrame.col == initialCol)
        {
            if (masterManager.framesManager.VirginFrame.row == initialRow && masterManager.framesManager.VirginFrame.col == initialCol)
            {
                result = true;
            }
        }
        return result;
    }
}
