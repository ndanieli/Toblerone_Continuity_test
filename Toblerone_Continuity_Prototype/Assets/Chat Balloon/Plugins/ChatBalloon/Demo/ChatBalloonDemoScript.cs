using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatBalloonDemoScript : MonoBehaviour {
	[SerializeField] private ChatBalloon chatBalloon;

	[SerializeField] private InputField inputText;

	public float minWidth = 64f;
	public float minHeight = 24f;

	public float maxWidth = 206f;
	public float maxHeight = 90f;

	public float widthBorder = 20f;
	public float heightBorder = 28f;

	// Use this for initialization
	public void Awake(){
	}
	public void Update(){
		chatBalloon.minWidth = minWidth;
		chatBalloon.minHeight = minHeight;
		chatBalloon.maxWidth = maxWidth;
		chatBalloon.maxHeight = maxHeight;
		chatBalloon.widthBorder = widthBorder;
		chatBalloon.heightBorder = heightBorder;

		chatBalloon.SetText(inputText.text);
	}
	[ContextMenu("Print")]
	public void callPrintText(){
		#region method
		chatBalloon.SetText(inputText.text);
		chatBalloon.SetActive(true);
		#endregion
	}
}
