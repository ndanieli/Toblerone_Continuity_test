using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyFrameMoverPerLevelManager : MonoBehaviour {

    private const int FIRST_PLAYABLE = 4;

    public MoveEmptyFrame moveEmptyFrameGeneral;
    public MoveEmptyFrameFirstPlayable moveEmptyFrameFirstPlayable;
    public MasterManager masterManager;

    public void OnEmptyFrameMoved(int row, int col)
    {
        switch (masterManager.LevelNumber)
        {
            case FIRST_PLAYABLE:
                moveEmptyFrameFirstPlayable.switchEmptyFrameLocation(row, col);
                break;

            default:
                moveEmptyFrameGeneral.switchEmptyFrameLocation(row, col);
                break;
        }
    }
}
