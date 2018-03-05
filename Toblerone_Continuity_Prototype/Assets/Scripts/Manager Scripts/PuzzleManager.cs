using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    //marks for each puzzle in the level if it is dependent of the previous puzzles
    //this indication is done in the inspector
    public bool[] puzzleDependency;

    //used to indicate which puzzles where solved already
    private bool[] smallPuzzlesStatus;
    private int numberOfPuzzles;
    private int numberOfSolvedPuzzles;
    private bool isLevelPuzzleSolved;
    public bool IsLevelPuzzleSolved { get { return isLevelPuzzleSolved; } }


    // Use this for initialization
    private void Start()
    {
        isLevelPuzzleSolved = false;
        numberOfPuzzles = puzzleDependency.Length;
        numberOfSolvedPuzzles = 0;

        //in case there are no puzzles in the scene
        if (numberOfPuzzles == 0)
        {
            markLevelPuzzleAsSolved();
        }

        initializeSmallPuzzlesStatusArray();
    }

    public void checkIfAllPuzzlesAreSolved()
    {
        if (numberOfSolvedPuzzles == numberOfPuzzles)
        {
            markLevelPuzzleAsSolved();
        }
    }

    public void markSmallPuzzleAsSolved(int puzzleIndicator)
    {
        if (!smallPuzzlesStatus[puzzleIndicator])
        {
            smallPuzzlesStatus[puzzleIndicator] = true;
            numberOfSolvedPuzzles++;
        }
    }

    public void markSmallPuzzleAsUnsolved(int puzzleIndicator)
    {
        if (smallPuzzlesStatus[puzzleIndicator])
        {
            smallPuzzlesStatus[puzzleIndicator] = false;
            numberOfSolvedPuzzles--;
            isLevelPuzzleSolved = false;
        }

    }

    private void markLevelPuzzleAsSolved()
    {
        isLevelPuzzleSolved = true;
    }

    private void markLevelPuzzleAsUnsolved()
    {
        isLevelPuzzleSolved = false;
    }

    /**
     * returns true if:
     *  the puzzle isn't marked as dependent in the puzzleDependency array
     *      or
     *  the puzzle is marked as dependent in the puzzleDependency array
     *  and all previous puzzles are already solved
     *  
     * return false if:
     *  both of the above are false
     *      or
     *  the small puzzle is already marked as solved
    **/
    public bool smallPuzzleCanBeSolved(int puzzleIndicator)
    {
        bool result = false;

        //checks if the puzzle is not yet solved
        if (!isSmallPuzzleSolved(puzzleIndicator))
        {
            //checks if the puzzle is not dependent on previous puzzles
            //or if the puzzle is the first puzzle in the list (then it can't be dependent)
            if (!smallPuzzleIsMarkedAsDepended(puzzleIndicator)
                || puzzleIndicator == 0)
            {
                result = true;
            }
            else
            {
                //checks if all previous puzzles are solved (for dependency)
                if (arePreviousSmallPuzzlesSolved(puzzleIndicator))
                {
                    result = true;
                }
            }
        }

        return result;
    }

    private void initializeSmallPuzzlesStatusArray()
    {
        smallPuzzlesStatus = new bool[numberOfPuzzles];
        for (int i = 0; i < smallPuzzlesStatus.Length; i++)
        {
            smallPuzzlesStatus[i] = false;
        }
    }

    private bool arePreviousSmallPuzzlesSolved(int puzzleIndicator)
    {
        bool result = true;

        for (int i = 0; i < puzzleIndicator; i++)
        {
            if (smallPuzzlesStatus[i] == false)
            {
                result = false;
            }
        }

        return result;
    }

    private bool isSmallPuzzleSolved(int puzzleIndicator)
    {
        return smallPuzzlesStatus[puzzleIndicator];
    }

    private bool smallPuzzleIsMarkedAsDepended(int puzzleIndicator)
    {
        return puzzleDependency[puzzleIndicator];
    }

    
}
