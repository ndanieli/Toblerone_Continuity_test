using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramesManager : MonoBehaviour {

    public const int TOP = 0;
    public const int RIGHT = 1;
    public const int BOTTOM = 2;
    public const int LEFT = 3;

    public int rows, cols;
    public struct position
    {
        public int row, col;
    }

    private position activeFrame;
    public int initialHeroFrameRow;
    public int initialHeroFrameColumn;

    public position emptyFrame;
    public GameObject[,] frames;
    public GameObject hero;

    GameObject tempFrame;

    // Use this for initialization
    void Start () {
        initFrameArray();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!GameObject.Find("CameraController").GetComponent<CameraControl>().zoomIn) {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                switchFrames(emptyFrame.row + 1, emptyFrame.col);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                switchFrames(emptyFrame.row, emptyFrame.col - 1);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                switchFrames(emptyFrame.row - 1, emptyFrame.col);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                switchFrames(emptyFrame.row, emptyFrame.col + 1);
            }

        }
    }

    void initFrameArray()
    {
        frames = new GameObject[rows + 2, cols + 2];
        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= cols; j++)
            {
                frames[i, j] = GameObject.Find("Frame" + (i - 1) + (j - 1));
                if ((frames[i,j] != null) && (frames[i, j].GetComponent<Frame>().isEmptyFrame))
                {
                    emptyFrame.row = i;
                    emptyFrame.col = j;
                }
            }
        }

        activeFrame.row = initialHeroFrameRow + 1;
        activeFrame.col = initialHeroFrameColumn + 1;
    }

    void switchFrames(int row, int col)
    {
        if (frames[row, col] != null)
        {
            Debug.Log("Need to switch between empty frame - " + frames[emptyFrame.row, emptyFrame.col] + " and not empty frame - " + frames[row, col]);

            // Check if hero is in the moving frame
            bool isActiveFrame = row == activeFrame.row && col == activeFrame.col;

            // Switch the frames game position
            GameObject empty = frames[emptyFrame.row, emptyFrame.col];
            GameObject change = frames[row, col];

            if (isActiveFrame)
            {
                Vector2 heroRelativePos = frames[row, col].transform.position - hero.gameObject.transform.position;
                //hero.gameObject.transform.position = frames[emptyFrame.row, emptyFrame.col].gameObject.transform.position;
                hero.gameObject.transform.position = (Vector2)frames[emptyFrame.row, emptyFrame.col].gameObject.transform.position - heroRelativePos;
            }

            Vector2 emptyFramePosition = empty.GetComponent<Transform>().position;
            empty.GetComponent<Transform>().position = change.GetComponent<Transform>().position;
            change.GetComponent<Transform>().position = emptyFramePosition;


            // Switch the frames array indices
                 //switch active frame values if needed
            if (isActiveFrame)
            {
                activeFrame.row = emptyFrame.row;
                activeFrame.col = emptyFrame.col;
            }

            frames[row, col] = empty;
            frames[emptyFrame.row, emptyFrame.col] = change;
            emptyFrame.row = row;
            emptyFrame.col = col;

        }

    }
}
