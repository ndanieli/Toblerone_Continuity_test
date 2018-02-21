using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour {

    public const int LEFT_LABEL = 0;
    public const int TOP_LABEL = 1;
    public const int RIGHT_LABEL = 2;
    public const int BOTTOM_LABEL = 3;
    
    public double position;
    public int[] borderLabels;
    public GameObject hero;
    public Frame[] otherFrames;
    public Frame EmptyFrame;
    public bool isEmptyFrame;
    public bool isActiveFrame;
    public GameObject frameCam;

	public void OnBorderTrigger(int borderSide)
    {
        int label = borderLabels[borderSide];

        //Debug.Log("label: " + label + " position: " + position);

        if (label == -1)
        {
            return;
        }

        double nextFramePosition = calculateNextFramePosition(borderSide);

        //Debug.Log("nextFramePosition: " + nextFramePosition);

        Frame otherFrame = null;
        foreach (Frame f in otherFrames)
        {
            if (f.position == nextFramePosition)
            {
                otherFrame = f;
                break;
            }
        }

        if (otherFrame == null || otherFrame.isEmptyFrame)
        {
            //  Debug.Log("Other frame is null");
            return;
        }

        int otherFrameSide = calculateOtherFrameSide(borderSide);
        //Debug.Log("otherFrameSide: " + otherFrameSide);

        double otherFrameLabel = otherFrame.borderLabels[otherFrameSide];
        
        if (label == otherFrameLabel)
        {
            moveHeroToNextFrame(borderSide);
            changeActiveFrame(otherFrame);
            return;
        }

    }

    private void changeActiveFrame(Frame otherFrame)
    {
        isActiveFrame = false;
        otherFrame.isActiveFrame = true;
        frameCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 1;
        otherFrame.frameCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 2;
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1") && isActiveFrame)
        {
            double emptyFramePosition = EmptyFrame.position;

            double posCalc = position - emptyFramePosition;
            if (posCalc == 1f || posCalc == -1f || posCalc == 0.1f || posCalc == -0.1f)
            {
                switchFrames();
            }
        }
    }

    private void switchFrames()
    {
        Vector2 temp = gameObject.transform.position;
        Vector2 heroRelativePos = gameObject.transform.position - hero.gameObject.transform.position;

        gameObject.transform.position = EmptyFrame.gameObject.transform.position;
        EmptyFrame.gameObject.transform.position = temp;
        hero.transform.position = (Vector2)gameObject.transform.position - heroRelativePos;

        double tempPos = position;
        position = EmptyFrame.position;
        EmptyFrame.position = tempPos;
    }

    private void moveHeroToNextFrame(int borderSide)
    {
        switch (borderSide)
        {
            case LEFT_LABEL:
                hero.gameObject.transform.position = new Vector2(hero.gameObject.transform.position.x - 5, hero.gameObject.transform.position.y);
                break;

            case TOP_LABEL:
                hero.gameObject.transform.position = new Vector2(hero.gameObject.transform.position.x, hero.gameObject.transform.position.y - 5);
                break;

            case RIGHT_LABEL:
                hero.gameObject.transform.position = new Vector2(hero.gameObject.transform.position.x + 5, hero.gameObject.transform.position.y);
                break;

            case BOTTOM_LABEL:
                hero.gameObject.transform.position = new Vector2(hero.gameObject.transform.position.x, hero.gameObject.transform.position.y + 5);
                break;

            default:
                break;
        }
    }

    private static int calculateOtherFrameSide(int borderSide)
    {
        int otherFrameSide = 0;
        switch (borderSide)
        {
            case LEFT_LABEL:
                otherFrameSide = RIGHT_LABEL;
                break;

            case TOP_LABEL:
                otherFrameSide = BOTTOM_LABEL;
                break;

            case RIGHT_LABEL:
                otherFrameSide = LEFT_LABEL;
                break;

            case BOTTOM_LABEL:
                otherFrameSide = TOP_LABEL;
                break;

            default:
                break;
        }

        return otherFrameSide;
    }

    private double calculateNextFramePosition(int borderSide)
    {
        double nextFramePosition = position;

        switch (borderSide)
        {
            case LEFT_LABEL:
                nextFramePosition -= 0.1;
                break;

            case TOP_LABEL:
                nextFramePosition -= 1.0;
                break;

            case RIGHT_LABEL:
                nextFramePosition += 0.1;
                break;

            case BOTTOM_LABEL:
                nextFramePosition += 1.0;
                break;
        }

        return nextFramePosition;
    }
}
