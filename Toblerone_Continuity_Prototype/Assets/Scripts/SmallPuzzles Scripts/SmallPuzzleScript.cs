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

    public void OnSmallPuzzleTrigger()
    {
        if (!isSmallPuzzleSolved)
        {
            if (puzzleManager.smallPuzzleCanBeSolved(SmallPuzzleIndicator))
            {
                solveSmallPuzzle();
            }
        }
    }

    private void solveSmallPuzzle()
    {
        isSmallPuzzleSolved = true;
        puzzleManager.markSmallPuzzleAsSolved(SmallPuzzleIndicator);
        puzzleManager.checkIfAllPuzzlesAreSolved();
    }
}
