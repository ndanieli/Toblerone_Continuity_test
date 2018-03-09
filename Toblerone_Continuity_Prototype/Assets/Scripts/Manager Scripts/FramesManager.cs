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
    public MoveEmptyFrameFirstPlayable moveEmptyFrameFirstPlayable;

    //Initial values that are defined in the heirarchy
    public int levelNum;
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
    void Start()
    {
        initFrameArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelNum != 4)
        {
            if (!GameObject.Find("CameraManager").GetComponent<CameraControl>().zoomIn)
            {
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
        //if in first playable level
        else
        {
            if (!GameObject.Find("CameraManager").GetComponent<CameraControl>().zoomIn)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    moveEmptyFrameFirstPlayable.switchEmptyFrameLocation(emptyFrame.row + 1, emptyFrame.col);
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    moveEmptyFrameFirstPlayable.switchEmptyFrameLocation(emptyFrame.row, emptyFrame.col - 1);
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    moveEmptyFrameFirstPlayable.switchEmptyFrameLocation(emptyFrame.row - 1, emptyFrame.col);
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    moveEmptyFrameFirstPlayable.switchEmptyFrameLocation(emptyFrame.row, emptyFrame.col + 1);
                }

            }
        }
        
    }

    /**
     * the virgin will always attempt to move as far away as possible from the hero
     * if the opposite direction from which the hero came is unavailable, the virgin will 
     * go to the first available from from LEFT-TOP-RIGHT-BOTTOM
     * 
     * The virgin cannot go to where the hero came from
    **/
    public void VirginEscapesToNextFrame(int heroDirection)
    {
        bool hasMoved = false;

        int availableNextFrames = calculateAvailableNextFramesForVirgin();

        // shouldn't happen, checking just in case
        if (availableNextFrames <= 0)
        {
            Debug.Log("Escape has been triggered without an available adjacent frame");
            throw new Exception("Escape has been triggered without an available adjacent frame");
        }

        // checks if the opposite direction is available
        bool isOppositeAvailable;
        switch (heroDirection)
        {
            case RIGHT:
                isOppositeAvailable = availableNextFrames % 10 == 1;
                if (isOppositeAvailable && !hasMoved)
                {
                    hasMoved = true;
                    moveVirginToNextFrame(LEFT);
                }
                break;

            case BOTTOM:
                isOppositeAvailable = (availableNextFrames / 10) % 10 == 1;
                if (isOppositeAvailable && !hasMoved)
                {
                    hasMoved = true;
                    moveVirginToNextFrame(TOP);
                }
                break;

            case LEFT:
                isOppositeAvailable = (availableNextFrames / 100) % 10 == 1;
                if (isOppositeAvailable && !hasMoved)
                {
                    hasMoved = true;
                    moveVirginToNextFrame(RIGHT);
                }
                break;

            case TOP:
                isOppositeAvailable = (availableNextFrames / 1000) % 10 == 1;
                if (isOppositeAvailable && !hasMoved)
                {
                    hasMoved = true;
                    moveVirginToNextFrame(BOTTOM);
                }
                break;
            default:
                break;
        }

        // Reached if the opposite direction isn't available
        int label = availableNextFrames % 10;

        if (!hasMoved && label == 1 && heroDirection != LEFT)
        {
            moveVirginToNextFrame(LEFT);
            hasMoved = true;
        }

        availableNextFrames /= 10;
        label = availableNextFrames % 10;

        if (!hasMoved && label == 1 && heroDirection != TOP)
        {
            moveVirginToNextFrame(TOP);
            hasMoved = true;
        }

        availableNextFrames /= 10;
        label = availableNextFrames % 10;

        if (!hasMoved && label == 1 && heroDirection != RIGHT)
        {
            moveVirginToNextFrame(RIGHT);
            hasMoved = true;
        }

        availableNextFrames /= 10;
        label = availableNextFrames % 10;

        if (!hasMoved && label == 1 && heroDirection != BOTTOM)
        {
            moveVirginToNextFrame(BOTTOM);
            hasMoved = true;
        }
    }

    /**
     *  checks if the virgin can escape to an adjacent frame from which the hero did NOT come from
    **/
    public bool CanVirginEscape(int heroDirection)
    {
        bool result = false;

        int availalbeNextFrames = calculateAvailableNextFramesForVirgin();

        int label = availalbeNextFrames % 10;

        // Checks if the virgin can move left and if the hero didn't come from the left frame
        if (label == 1 && heroDirection != LEFT)
        {
            result = true;
        }

        availalbeNextFrames /= 10;
        label = availalbeNextFrames % 10;

        // Checks if the virgin can move up and if the hero didn't come from the above frame
        if (label == 1 && heroDirection != TOP)
        {
            result = true;
        }

        availalbeNextFrames /= 10;
        label = availalbeNextFrames % 10;

        // Checks if the virgin can move right and if the hero didn't come from the right frame
        if (label == 1 && heroDirection != RIGHT)
        {
            result = true;
        }

        availalbeNextFrames /= 10;
        label = availalbeNextFrames % 10;

        // Checks if the virgin can move left and if the hero didn't come from the bottom frame
        if (label == 1 && heroDirection != BOTTOM)
        {
            result = true;
        }

        return result;
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
            if (virginFrame.row == nextFrameRow && virginFrame.col == nextFrameCol)
            {
                triggerVirginMovementIfNeeded(borderSide);
            }
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

    public void SwitchPuzzleFramePositionToEmptyFramePosition()
    {
        puzzleFrame.row = emptyFrame.row;
        puzzleFrame.col = emptyFrame.col;
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
        puzzleFrame.row = initialPuzzleFrameRow + 1;
        puzzleFrame.col = initialPuzzleFrameColumn + 1;
    }

    private bool checkLabels(int borderSide, int nextFrameRow, int nextFrameCol)
    {
        int originalFrameLabel = frames[activeFrame.row, activeFrame.col].GetComponent<Frame>().borderLabels[borderSide];
        int otherFrameLabel = -1;

        Frame otherFrame = frames[nextFrameRow, nextFrameCol].GetComponent<Frame>();

        if (otherFrame == null || originalFrameLabel == -1)
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
                hero.gameObject.transform.position = new Vector2(hero.gameObject.transform.position.x, hero.gameObject.transform.position.y + 10);
                break;

            case RIGHT:
                hero.gameObject.transform.position = new Vector2(hero.gameObject.transform.position.x + 5, hero.gameObject.transform.position.y);
                break;

            case BOTTOM:
                hero.gameObject.transform.position = new Vector2(hero.gameObject.transform.position.x, hero.gameObject.transform.position.y - 5);
                break;

            default:
                break;
        }
    }

    /**
     * returns a bitwise result of available frames that can be moved from the virgins' current location
     * if the singles' digit is one - the virgin can move left
     * if the tens' digit is one - the virgin can move up
     * if the hundreds' digit is one - the virgin can move right
     * if the thousands' digit is one - the virgin can move down
    **/
    private int calculateAvailableNextFramesForVirgin()
    {
        int result = 0;
        int virginLeftLabel = frames[virginFrame.row, virginFrame.col].GetComponent<Frame>().borderLabels[LEFT];
        int virginTopLabel = frames[virginFrame.row, virginFrame.col].GetComponent<Frame>().borderLabels[TOP];
        int virginRightLabel = frames[virginFrame.row, virginFrame.col].GetComponent<Frame>().borderLabels[RIGHT];
        int virginBottomLabel = frames[virginFrame.row, virginFrame.col].GetComponent<Frame>().borderLabels[BOTTOM];


        Frame leftFrame = frames[virginFrame.row, virginFrame.col - 1] != null ? frames[virginFrame.row, virginFrame.col - 1].GetComponent<Frame>() : null;
        Frame topFrame = frames[virginFrame.row - 1, virginFrame.col] != null ? frames[virginFrame.row - 1, virginFrame.col].GetComponent<Frame>() : null;
        Frame rightFrame = frames[virginFrame.row, virginFrame.col + 1] != null ? frames[virginFrame.row, virginFrame.col + 1].GetComponent<Frame>() : null;
        Frame bottomFrame = frames[virginFrame.row + 1, virginFrame.col] != null ? frames[virginFrame.row + 1, virginFrame.col].GetComponent<Frame>() : null;

        if (leftFrame != null && virginLeftLabel != -1 && virginLeftLabel == leftFrame.borderLabels[RIGHT])
        {
            result += 1;
        }

        if (topFrame != null && virginTopLabel != -1 && virginTopLabel == topFrame.borderLabels[BOTTOM])
        {
            result += 10;
        }

        if (rightFrame != null && virginRightLabel != -1 && virginRightLabel == rightFrame.borderLabels[LEFT])
        {
            result += 100;
        }

        if (bottomFrame != null && virginBottomLabel != -1 && virginBottomLabel == bottomFrame.borderLabels[TOP])
        {
            result += 1000;
        }

        return result;
    }

    private void moveVirginToNextFrame(int directionLabel)
    {
        int newVirginRow = virginFrame.row;
        int newVirginCol = virginFrame.col;

        switch (directionLabel)
        {
            case LEFT:
                newVirginCol--;
                break;

            case TOP:
                newVirginRow--;
                break;

            case RIGHT:
                newVirginCol++;
                break;

            case BOTTOM:
                newVirginRow++;
                break;

            default:
                break;
        }

        Vector2 currentDiff = frames[virginFrame.row, virginFrame.col].GetComponent<Frame>().transform.position - Virgin.gameObject.transform.position;
        Virgin.gameObject.transform.position = (Vector2)frames[newVirginRow, newVirginCol].GetComponent<Frame>().transform.position - currentDiff;

        virginFrame.row = newVirginRow;
        virginFrame.col = newVirginCol;
    }

    private void triggerVirginMovementIfNeeded(int borderSide)
    {
        switch (borderSide)
        {
            case LEFT:
                masterManager.virgin.GetComponent<VirginMovement>().OnHeroEntersFrame(RIGHT);
                break;

            case TOP:
                masterManager.virgin.GetComponent<VirginMovement>().OnHeroEntersFrame(BOTTOM);
                break;

            case RIGHT:
                masterManager.virgin.GetComponent<VirginMovement>().OnHeroEntersFrame(LEFT);
                break;

            case BOTTOM:
                masterManager.virgin.GetComponent<VirginMovement>().OnHeroEntersFrame(TOP);
                break;

            default:
                break;
        }
    }
}
