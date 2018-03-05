using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayableLevelSmallPuzzle0 : MonoBehaviour {

    public SmallPuzzleScript smallPuzzleScript;
    public FramesManager frameManager;
    public int currentRow;
    public int currentCol;

    private void Start()
    {
        currentRow++;
        currentCol++;
    }

    public void TriggerSmallPuzzleIfNeeded(int emptyFrameNewRow, int emptyFrameNewCol, int emptyFramePreviousRow, int emptyFramePreviousCol)
    {
        if (didSmallPuzzleMove(emptyFrameNewRow, emptyFrameNewCol))
        {
            //Debug.Log("Small puzzle frame moved");
            updateCurrentRowAndCol(emptyFramePreviousRow, emptyFramePreviousCol);
        }

        if (isPuzzleFrameToTheRight())
        {
            //Debug.Log("Solved");
            smallPuzzleScript.OnSmallPuzzleTriggerToSolve();
        }
        else
        {
            //Debug.Log("UnSolved");
            smallPuzzleScript.OnSmallPuzzleTriggerToUnsolve();
        }
    }

    private bool isPuzzleFrameToTheRight()
    {
        int puzzleFrameRow = frameManager.PuzzleFrame.row;
        int puzzleFrameCol = frameManager.PuzzleFrame.col;

        return currentRow == puzzleFrameRow && currentCol == puzzleFrameCol - 1;
    }

    private void updateCurrentRowAndCol(int emptyFramePreviousRow, int emptyFramePreviousCol)
    {
        currentRow = emptyFramePreviousRow;
        currentCol = emptyFramePreviousCol;
    }

    private bool didSmallPuzzleMove(int emptyFrameNewRow, int emptyFrameNewCol)
    {
        //Debug.Log("did small puzzle move check: current row " + currentRow + " current col: " + currentCol + " emptyframenewrow: " + emptyFrameNewRow + " emptyframenewcol: " + emptyFrameNewCol);
        //Debug.Log(emptyFrameNewRow == currentRow && emptyFrameNewCol == currentCol);
        return emptyFrameNewRow == currentRow && emptyFrameNewCol == currentCol;
    }
}
