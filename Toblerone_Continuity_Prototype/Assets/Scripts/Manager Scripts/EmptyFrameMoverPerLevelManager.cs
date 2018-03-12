using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyFrameMoverPerLevelManager : MonoBehaviour
{

    public MoveEmptyFrame moveEmptyFrame;

    public void OnEmptyFrameMoved(int row, int col)
    {
        moveEmptyFrame.SwitchEmptyFrameLocation(row, col);
    }
}
