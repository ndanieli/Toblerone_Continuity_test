using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPuzzleScript : MonoBehaviour
{

    public PuzzleManager puzzleManager;
    public int SmallPuzzleIndicator;
    private bool isSmallPuzzleSolved;

    private void Start()
    {
        isSmallPuzzleSolved = false;
    }

    public void OnSmallPuzzleTriggerToSolve()
    {
        if (!isSmallPuzzleSolved)
        {
            //Debug.Log("updated solved");
            if (puzzleManager.smallPuzzleCanBeSolved(SmallPuzzleIndicator))
            {
                solveSmallPuzzle();
            }
        }
    }

    public void OnSmallPuzzleTriggerToUnsolve()
    {
        if (isSmallPuzzleSolved)
        {
            //Debug.Log("updated unsolved");
            unsolveSmallPuzzle();
        }
    }

    private void unsolveSmallPuzzle()
    {
        isSmallPuzzleSolved = false;
        puzzleManager.markSmallPuzzleAsUnsolved(SmallPuzzleIndicator);
    }

    private void solveSmallPuzzle()
    {
        isSmallPuzzleSolved = true;
        puzzleManager.markSmallPuzzleAsSolved(SmallPuzzleIndicator);
        puzzleManager.checkIfAllPuzzlesAreSolved();
    }
}
