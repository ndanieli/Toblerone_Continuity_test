using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAgainButton : MonoBehaviour {

    public MasterManager masterManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartGame()
    {
        masterManager.RespawnHeroAfterDeath();
    }
}
