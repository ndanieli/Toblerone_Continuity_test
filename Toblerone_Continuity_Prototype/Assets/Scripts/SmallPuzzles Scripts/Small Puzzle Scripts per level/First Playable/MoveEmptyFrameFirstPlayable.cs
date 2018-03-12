using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEmptyFrameFirstPlayable : MoveEmptyFrame{

    public FirstPlayableLevelSmallPuzzle0 firstPlayableLevelSmallPuzzle0;

    public override void switchEmptyFrameLocation(int row, int col)
    {
        base.switchEmptyFrameLocation(row,col);
        TriggerfirstPlayablePuzzle(row, col);
    }

    public void TriggerfirstPlayablePuzzle(int row, int col)
    {

            firstPlayableLevelSmallPuzzle0.TriggerSmallPuzzleIfNeeded(row, col, frameManager.EmptyFrame.row, frameManager.EmptyFrame.col);
    }
}
