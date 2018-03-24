using UnityEngine;
using UnityEngine.UI;

public class ChatBalloon : MonoBehaviour {
	[SerializeField] private Canvas canvas;
	[SerializeField] private Text text;
	[SerializeField] private Image box;

	[HideInInspector][System.NonSerialized] public float minWidth = 64f;
	[HideInInspector][System.NonSerialized] public float minHeight = 24f;

	[HideInInspector][System.NonSerialized] public float maxWidth = 206f;
	[HideInInspector][System.NonSerialized] public float maxHeight = 90f;

	[HideInInspector][System.NonSerialized] public float widthBorder = 20f;
	[HideInInspector][System.NonSerialized] public float heightBorder = 28f;

	//SetText Cache Variable
	private Vector2 size = Vector2.zero;
	private bool overFlow;

	public void SetBox(Sprite _boxSprite){
		#region method
		box.sprite = _boxSprite;
		#endregion
	}
	public void SetText(string _text){
		#region method
		text.text = _text.Replace("\\n","\n");
		size = Vector2.zero;
		overFlow = false;
	
		text.resizeTextForBestFit = false;
		text.horizontalOverflow = HorizontalWrapMode.Overflow;
		text.verticalOverflow = VerticalWrapMode.Overflow;

		if(text.preferredWidth+widthBorder <= minWidth ){
			size.x = minWidth;
		}else if(text.preferredWidth+widthBorder >= maxWidth){
			overFlow = true;
			size.x = maxWidth;
		}else{
			size.x = text.preferredWidth+widthBorder;
		}
		if(text.preferredHeight+heightBorder <= minHeight ){
			size.y = minHeight;
		}else if(text.preferredHeight+heightBorder >= maxHeight){
			overFlow = true;
			size.y = maxHeight;
		}else{
			size.y = text.preferredHeight+heightBorder;
		}

		text.resizeTextForBestFit = overFlow;

		if(overFlow){
			text.horizontalOverflow = HorizontalWrapMode.Wrap;
			text.verticalOverflow = VerticalWrapMode.Truncate;
		}

		box.rectTransform.sizeDelta = size;
		size.x -= widthBorder;
		size.y -= heightBorder;
		text.rectTransform.sizeDelta = size;

		#endregion
	}
	public void SetActive(bool status,float showTime = 1.5f){
		#region method
		CancelInvoke();
		canvas.enabled = status;
		if(status == true && showTime >= 0){
			Invoke("Hide",showTime);
		}
		#endregion
	}
	public void Hide(){
		#region method
		SetActive(false);
		#endregion
	}

	public void SetRenderLayer(string layerName){
		#region method
		canvas.sortingLayerName = layerName;
		#endregion
	}
	public void SetRenderLayer(int layerIndex){
		#region method
		canvas.sortingLayerID = layerIndex;
		#endregion
	}
	public void SetRenderOrder(int order){
		#region method
		canvas.sortingOrder = order;
		#endregion
	}
}