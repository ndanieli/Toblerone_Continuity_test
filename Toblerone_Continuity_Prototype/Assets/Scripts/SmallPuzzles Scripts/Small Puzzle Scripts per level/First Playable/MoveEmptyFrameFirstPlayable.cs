using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEmptyFrameFirstPlayable : MonoBehaviour {

    public FramesManager frameManager;
    public FirstPlayableLevelSmallPuzzle0 firstPlayableLevelSmallPuzzle0;

    
    public void TriggerfirstPlayablePuzzle(int row, int col)
    {

            firstPlayableLevelSmallPuzzle0.TriggerSmallPuzzleIfNeeded(row, col, frameManager.EmptyFrame.row, frameManager.EmptyFrame.col);
    }
}
