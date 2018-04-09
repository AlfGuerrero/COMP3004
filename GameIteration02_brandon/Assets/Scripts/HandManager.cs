using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandManager : MonoBehaviour {

	HorizontalLayoutGroup layout;
	// QuestGame.Logger logger = new QuestGame.Logger ();

	// Use this for initialization
	void Start () {
		// logger.info ("HandManager.cs :: Initialzing HandManager.");
		layout = this.GetComponent<HorizontalLayoutGroup> ();
	}

	// Update is called once per frame
	void Update () {
		//SetCardsInList ();
		CardDistribution ();



	}

	void DrawCard(){

	}

	void CardDistribution(){
		int handSize = this.transform.childCount;
		if(handSize > 12){

			// Debug.Log ("YOU HAVE TOO MANY CARDS IN HAND");
		}

		if (handSize <= 12) {
			// layout.spacing = -8;
			//1;
			//boxCollider.offset = new Vector2(1f,boxCollider.offset.y);
			//ResizeCardColliders(f);
		}
		else {
		 layout.spacing = -1;
			//-26
			//boxCollider.offset = new Vector2(-26f,boxCollider.offset.y);
			//ResizeCardColliders(2f);
		}

	}

	/*void SetCardsInList(){
		cardsInHand = GameObject.FindGameObjectsWithTag ("Card");
	}

	void ResizeCardColliders(float offsetX){
		foreach(GameObject card in cardsInHand){
			BoxCollider2D col = card.GetComponent<BoxCollider2D> ();
			col.offset = new Vector2 (offsetX, col.offset.y);
		}
	}*/
}
