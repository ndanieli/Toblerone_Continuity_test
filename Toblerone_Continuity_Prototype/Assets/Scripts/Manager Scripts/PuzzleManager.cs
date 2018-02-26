using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

    private bool isPuzzleSolved;

    public bool IsPuzzleSolved { get { return isPuzzleSolved; } }

    // Use this for initialization
    private void Start () {
        isPuzzleSolved = false;
	}
	
    public void MarkPuzzleAsSolved()
    {
        isPuzzleSolved = true;
    }

}
