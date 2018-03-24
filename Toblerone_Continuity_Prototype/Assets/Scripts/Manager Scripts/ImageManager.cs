using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour {

    public GameObject EndScreenWin;
    public GameObject EndScreenLose;
    public Sprite knight_without_sword;
    public Sprite knight_with_sword;
    public ChatBalloon princessChatBalloon;
    public ChatBalloon knightChatBalloon;
    public Sprite dragon_start;
    public Sprite dragon_dead;

    // Use this for initialization
    void Start () {
        EndScreenWin.GetComponent<Image>().enabled = false;
        EndScreenLose.GetComponent<Image>().enabled = false;
        knightChatBalloon.Hide();
        princessChatBalloon.Hide();
        GameObject.Find("Dragon").gameObject.GetComponent<SpriteRenderer>().sprite = dragon_start;
    }
	
	public void EnableScreenWinImage()
    {
        EndScreenWin.GetComponent<Image>().enabled = true;
    }

    public void EnableScreenLoseImage()
    {
        EndScreenLose.GetComponent<Image>().enabled = true;
    }

    public void DisableScreenWinImage()
    {
        EndScreenWin.GetComponent<Image>().enabled = false;
    }

    public void DisableScreenLoseImage()
    {
        EndScreenLose.GetComponent<Image>().enabled = false;
    }

    public void ShowPrincessChatBaloon()
    {
        princessChatBalloon.SetText("My gosh, there's a dragon in that room!!");
        princessChatBalloon.SetActive(true, 3.0f);
    }

    public void ShowKnightChatBaloon()
    {
        knightChatBalloon.SetText("I am not a knight butt naked! find it!");
        knightChatBalloon.SetActive(true, 3.0f);
    }
}
