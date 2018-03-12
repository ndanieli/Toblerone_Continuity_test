using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEmptyFrame : MonoBehaviour {

    public FramesManager frameManager;

    public virtual void switchEmptyFrameLocation(int row, int col)
    {
        if (frameManager.Frames[row, col] != null)
        {
            if (isActiveFrame(row, col))
            {
                Vector2 heroRelativePos = frameManager.Frames[row, col].transform.position - frameManager.hero.gameObject.transform.position;
                frameManager.hero.gameObject.transform.position = (Vector2)frameManager.Frames[frameManager.EmptyFrame.row, frameManager.EmptyFrame.col].gameObject.transform.position - heroRelativePos;
                frameManager.SwitchActiveFramePositionToEmptyFramePosition();
            }

            if (isVirginFrame(row, col))
            {
                Vector2 virginRelativePos = frameManager.Frames[row, col].transform.position - frameManager.Virgin.gameObject.transform.position;
                frameManager.Virgin.gameObject.transform.position = (Vector2)frameManager.Frames[frameManager.EmptyFrame.row, frameManager.EmptyFrame.col].gameObject.transform.position - virginRelativePos;
                frameManager.SwitchVirginFramePositionToEmptyFramePosition();
            }

            if (isPuzzleFrame(row, col))
            {
                frameManager.SwitchPuzzleFramePositionToEmptyFramePosition();
            }

            frameManager.SwitchFramePositionWithEmptyFramePosition(row, col);
        }
    }

    protected bool isActiveFrame (int row, int col)
    {
        return row == frameManager.ActiveFrame.row && col == frameManager.ActiveFrame.col;
    }

    protected bool isVirginFrame(int row, int col)
    {
        return row == frameManager.VirginFrame.row && col == frameManager.VirginFrame.col;
    }

    protected bool isPuzzleFrame(int row, int col)
    {
        return row == frameManager.PuzzleFrame.row && col == frameManager.PuzzleFrame.col;
    }
}
