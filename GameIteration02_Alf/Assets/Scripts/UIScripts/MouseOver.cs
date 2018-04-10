using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour {

	Vector3 startPos;
	public GameObject blownUpCard;
	public bool mousingOver = false;

	public void OnMouseEnter()
	{
		//startPos = this.transform.localPosition;
//		blownUpCard = new GameObject("blownUpCard");
//		blownUpCard.AddComponent<SpriteRenderer>();
////		blownUpCard.GetComponent<SpriteRenderer> ().sprite = this.GetComponent<Image> ().sprite;
////
//		blownUpCard.transform.position = new Vector3 (this.transform.position.x,this.transform.position.y + 20f,0f);
//		blownUpCard.transform.localScale = new Vector3 (6f, 6f, 6f);
	}

	public void OnMouseOver()
	{
		mousingOver = true;
//		this.transform.localScale = new Vector3 (2,2,1);
//		this.transform.localPosition = new Vector3 (startPos.x, startPos.y + this.GetComponent<RectTransform>().rect.height / 2, startPos.z);
	}

	public void OnMouseExit()
	{
		mousingOver = false;
		//this.transform.localScale = new Vector3 (1,1,1);
		//this.transform.localPosition = startPos;
	}

}
