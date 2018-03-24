using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour {
	public StoryDeck storyDeck;
	public GameObject storyCard;
	public GameObject player;
	// Use this for initialization
	void Start () {
//		player = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
	}

	public void controlPlayerTurn(){

	}
		
	public void PickUpStoryCard(){
		if (!isLocalPlayer) {
			return;
		}
		if (storyCard != null) {
			DestroyObject (storyCard);
		}


//		storyCard = storyDeck.Draw();

		string nameOfCard = storyDeck.NewCard ();
		Debug.Log("[Local] " + nameOfCard);

//		storyCard = storyDeck.Draw();
//		storyCard.transform.SetParent (GameObject.Find ("GameCanvas").transform);
//		storyCard.transform.localPosition = new Vector3 (-352f, 17f, 0f);
		CmdPickUpStoryCard(this.gameObject, nameOfCard);
	}

	[Command]
	public void CmdPickUpStoryCard(GameObject gObject, string name){
		Debug.Log ("[Command]" + storyCard);
		if (storyCard != null) {
			DestroyObject (storyCard);
		}
		storyCard = storyDeck.Draw(name);
		storyCard.transform.SetParent (GameObject.Find ("GameCanvas").transform);
		storyCard.transform.localPosition = new Vector3 (-352f, 17f, 0f);
//		NetworkServer.Spawn (storyCard); 

		RpcPickUpStoryCard(this.gameObject, name);
	}

	[ClientRpc]
	public void RpcPickUpStoryCard(GameObject gObject, string name){
		if (storyCard != null) {
			DestroyObject (storyCard);
		}
//		// want to call the add image function here...! 
		Debug.Log ("[ClientRpc]" + storyCard);
		storyCard = storyDeck.Draw(name);
		storyCard.transform.SetParent (GameObject.Find ("GameCanvas").transform);
		storyCard.transform.localPosition = new Vector3 (-352f, 17f, 0f);
//		NetworkServer.Spawn (storyCard); 


	}

}
