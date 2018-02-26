using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramesManager : MonoBehaviour {

    public const int LEFT = 0;
    public const int TOP = 1;
    public const int RIGHT = 2;
    public const int BOTTOM = 3;

    public struct position
    {
        public int row, col;
    }

    //Game Objects that need to be defined in the heirarchy
    public MasterManager masterManager;
    public MoveEmptyFrame moveEmptyFrame;
    public GameObject hero;
    public GameObject Virgin;

    //Initial values that are defined in the heirarchy
    public int rows, cols;
    public int initialHeroFrameRow;
    public int initialHeroFrameColumn;
    public int initialEmptyFrameRow;
    public int initialEmptyFrameColumn;
    public int initialVirginFrameRow;
    public int initialVirginFrameColumn;
    public int initialPuzzleFrameRow;
    public int initialPuzzleFrameColumn;

    //Internal values that are used in other files
    private GameObject[,] frames;
    private position activeFrame;
    private position virginFrame;
    private position puzzleFrame;
    private position emptyFrame;
    
    //public properties that allow to get, but not set the values
    public GameObject[,] Frames { get { return frames; } }
    public position ActiveFrame { get { return activeFrame; } }
    public position VirginFrame { get { return virginFrame; } }
    public position PuzzleFrame { get { return puzzleFrame; } }
    public position EmptyFrame { get { return emptyFrame; } }

    // Use this for initialization
    void Start () {
        initFrameArray();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!GameObject.Find("CameraManager").GetComponent<CameraControl>().zoomIn) {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveEmptyFrame.switchEmptyFrameLocation(emptyFrame.row + 1, emptyFrame.col);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveEmptyFrame.switchEmptyFrameLocation(emptyFrame.row, emptyFrame.col - 1);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveEmptyFrame.switchEmptyFrameLocation(emptyFrame.row - 1, emptyFrame.col);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveEmptyFrame.switchEmptyFrameLocation(emptyFrame.row, emptyFrame.col + 1);
            }

        }
    }
    
    public void SwitchHeroFrame(int borderSide)
    {
        int nextFrameRow = activeFrame.row;
        int nextFrameCol = activeFrame.col;

        switch (borderSide)
        {
            case LEFT:
                nextFrameCol -= 1;
                break;

            case RIGHT:
                nextFrameCol += 1;
                break;

            case TOP:
                nextFrameRow -= 1;
                break;

            case BOTTOM:
                nextFrameRow += 1;
                break;

            default:
                break;
        }

        if (frames[nextFrameRow,nextFrameCol] != null && checkLabels(borderSide, nextFrameRow, nextFrameCol))
        {
            moveHeroToNextFrame(borderSide);
            moveCameraToActiveFrame(nextFrameRow, nextFrameCol);
            changeActiveFrame(nextFrameRow, nextFrameCol);
        }
    }

    public void SwitchActiveFramePositionToEmptyFramePosition()
    {
        activeFrame.row = emptyFrame.row;
        activeFrame.col = emptyFrame.col;
    }

    public void SwitchVirginFramePositionToEmptyFramePosition()
    {
        virginFrame.row = emptyFrame.row;
        virginFrame.col = emptyFrame.col;
    }

    public void SwitchFramePositionWithEmptyFramePosition(int row, int col)
    {
        GameObject empty = frames[EmptyFrame.row, EmptyFrame.col];
        GameObject change = frames[row, col];

        Vector2 emptyFramePosition = empty.GetComponent<Transform>().position;
        empty.GetComponent<Transform>().position = change.GetComponent<Transform>().position;
        change.GetComponent<Transform>().position = emptyFramePosition;

        frames[row, col] = empty;
        frames[emptyFrame.row, emptyFrame.col] = change;
        emptyFrame.row = row;
        emptyFrame.col = col;
    }

    private void initFrameArray()
    {
        frames = new GameObject[rows + 2, cols + 2];
        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= cols; j++)
            {
                frames[i, j] = GameObject.Find("Frame" + (i - 1) + (j - 1));
            }
        }

        activeFrame.row = initialHeroFrameRow + 1;
        activeFrame.col = initialHeroFrameColumn + 1;
        emptyFrame.row = initialEmptyFrameRow + 1;
        emptyFrame.col = initialEmptyFrameColumn + 1;
        virginFrame.row = initialVirginFrameRow + 1;
        virginFrame.col = initialVirginFrameColumn + 1;
        puzzleFrame.row = initialPuzzleFrameRow;
        puzzleFrame.col = initialVirginFrameColumn;
    }

    private bool checkLabels(int borderSide, int nextFrameRow, int nextFrameCol)
    {
        int originalFrameLabel = frames[activeFrame.row, activeFrame.col].GetComponent<Frame>().borderLabels[borderSide];
        int otherFrameLabel = -1;

        Frame otherFrame = frames[nextFrameRow, nextFrameCol].GetComponent<Frame>();

        if (otherFrame == null)
        {
            return false;
        }

        switch (borderSide)
        {
            case LEFT:
                otherFrameLabel = otherFrame.borderLabels[RIGHT];
                break;

            case TOP:
                otherFrameLabel = otherFrame.borderLabels[BOTTOM];
                break;

            case RIGHT:
                otherFrameLabel = otherFrame.borderLabels[LEFT];
                break;

            case BOTTOM:
                otherFrameLabel = otherFrame.borderLabels[TOP];
                break;

            default:
                break;
        }

        return originalFrameLabel == otherFrameLabel;

    }

    private void moveCameraToActiveFrame(int nextFrameRow, int nextFrameCol)
    {
        frames[activeFrame.row, activeFrame.col].GetComponent<Frame>().frameCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 1;
        frames[nextFrameRow, nextFrameCol].GetComponent<Frame>().frameCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 2;
    }

    private void changeActiveFrame(int newFrameRow, int newFrameCol)
    {
        activeFrame.row = newFrameRow;
        activeFrame.col = newFrameCol;
    }

    private void moveHeroToNextFrame(int borderSide)
    {
        switch (borderSide)
        {
            case LEFT:
                hero.gameObject.transform.position = new Vector2(hero.gameObject.transform.position.x - 5, hero.gameObject.transform.position.y);
                break;

            case TOP:
                hero.gameObject.transform.position = new Vector2(hero.gameObject.transform.position.x, hero.gameObject.transform.position.y - 5);
                break;

            case RIGHT:
                hero.gameObject.transform.position = new Vector2(hero.gameObject.transform.position.x + 5, hero.gameObject.transform.position.y);
                break;

            case BOTTOM:
                hero.gameObject.transform.position = new Vector2(hero.gameObject.transform.position.x, hero.gameObject.transform.position.y + 5);
                break;

            default:
                break;
        }
    }

}
