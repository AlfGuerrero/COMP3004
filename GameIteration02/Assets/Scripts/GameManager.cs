using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour {
	public StoryDeck storyDeck;
	public GameObject player;
	public GameObject storyCard;

	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
	}

	public void controlPlayerTurn(){

	}
		
	public void PickUpStoryCard(){
		if (!isLocalPlayer) {return;}
		if (storyCard != null) {DestroyObject (storyCard);}
		string nameOfCard = storyDeck.NewCard ();
		GameObject.FindGameObjectWithTag("StoryCardTextUI").GetComponent<Text>().text = nameOfCard;
		storyCard = storyDeck.Draw(nameOfCard);		
		storyCard.transform.SetParent (GameObject.Find ("GameCanvas").transform);
		storyCard.transform.localPosition = new Vector3 (-352f, 17f, 0f);
		CmdPickUpStoryCard(this.gameObject, nameOfCard);
	}

	[Command] // Server calls Clients...
	public void CmdPickUpStoryCard(GameObject gObject, string name){
		if (storyCard != null) {DestroyObject (storyCard);}
		RpcPickUpStoryCard(this.gameObject, name);
	}

	[ClientRpc] // Clients call Server... 
	public void RpcPickUpStoryCard(GameObject gObject, string name){
		if (storyCard != null) {DestroyObject (storyCard);}
		GameObject.FindGameObjectWithTag("StoryCardTextUI").GetComponent<Text>().text = name;
		storyCard = storyDeck.Draw(name);
		storyCard.transform.SetParent (GameObject.Find ("GameCanvas").transform);
		storyCard.transform.localPosition = new Vector3 (-352f, 17f, 0f);
	}


}
