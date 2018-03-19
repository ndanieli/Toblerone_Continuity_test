using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knight_images : MonoBehaviour {

    private MasterManager masterManager;

    private void Awake()
    {
        masterManager = GetComponent<VirginMovement>().masterManager;
        gameObject.GetComponent<SpriteRenderer>().sprite = masterManager.imageManager.knight_without_sword;
    }

    public void ChangeKnightSpriteToKnightWithSword()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = masterManager.imageManager.knight_with_sword;
    }
}
