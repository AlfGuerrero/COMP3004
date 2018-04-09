using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour {
	public StoryDeck 	storyDeck;
  public AdventureDeck advDeck;
	public GameObject 	storyCard;
	public GameObject   advCard;
	GameObject[] 		storyCardDelete;
  GameObject[]    advCardDelete;

	void Start(){
		this.gameObject.name += netId.Value;
		GameObject.Find("HandCanvas").name += netId.Value;
//		CmdAddPlayer (playerSize);
	  	Debug.Log ("Player: " + netId.Value + " has joined.");
	}

	void Update () {
		if (!isLocalPlayer) {
			return;
		}
	}

	void OnApplicationQuit(){
		Debug.Log("Exiting...");
		advCardDelete = GameObject.FindGameObjectsWithTag("Card");
		foreach (GameObject i in advCardDelete){
			DestroyObject (i);
		}
	}

	public void ControlPlayerTurn(){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcControlPlayerTurn();}
		else					{CmdControlPlayerTurn();}
	}

	[Command]
	public void CmdControlPlayerTurn(){
		RpcControlPlayerTurn ();
	}
	[ClientRpc]
	public void RpcControlPlayerTurn(){
		string playerTurnText = GameObject.FindGameObjectWithTag("PlayerTurnTextUI").GetComponent<Text>().text;
		int playerTurnInt;
		int.TryParse (playerTurnText, out playerTurnInt);
		// if (playerTurnInt % netId.Value == 0){
			playerTurnInt++;
			GameObject.FindGameObjectWithTag ("PlayerTurnTextUI").GetComponent<Text> ().text = playerTurnInt.ToString();
		// }

	}


	public void PickUpStoryCard(){ // Local Button...
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcPickUpStoryCard();}
		else					{CmdPickUpStoryCard();}

	}
	[Command] // Server calls Clients...
		public void CmdPickUpStoryCard(){
		storyCardDelete = GameObject.FindGameObjectsWithTag("StoryCard");
		RpcPickUpStoryCard();
	}
	[ClientRpc] // Clients call Server...
	public void RpcPickUpStoryCard(){
		storyCardDelete = GameObject.FindGameObjectsWithTag("StoryCard");
		foreach (GameObject i in storyCardDelete){
			DestroyObject (i);
		}

		 // storyDeck = GameObject.Find("StoryManager").GetComponent<StoryDeck>();
		 if (storyDeck.storyDeck.Count == 1) {	PopulateStoryDeck();}

		string nameOfCard = storyDeck.NewCard ();
		GameObject.FindGameObjectWithTag("StoryCardTextUI").GetComponent<Text>().text = nameOfCard;
		storyCard = storyDeck.Draw(nameOfCard);
		storyCard.transform.SetParent (GameObject.Find ("GameCanvas").transform);
		storyCard.transform.localPosition = new Vector3 (-352f, 17f, 0f);
	}

	public void PopulateAdvDeck(){
		if (!isLocalPlayer) {return;}
			if (isServer) {RpcPopulateAdvDeck();}
			else					{CmdPopulateAdvDeck();}	}
	[Command]
	public void CmdPopulateAdvDeck(){
	  RpcPopulateAdvDeck();
	}
	[ClientRpc]
	public void RpcPopulateAdvDeck(){
		advDeck.populateDeck();
	}

	public void PopulateStoryDeck(){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcPopulateStoryDeck(this.gameObject);}
		else					{CmdPopulateStoryDeck(this.gameObject);}
	}
	[Command]
	public void CmdPopulateStoryDeck(GameObject gameObject){
    RpcPopulateStoryDeck(gameObject);
	}
	[ClientRpc]
	public void RpcPopulateStoryDeck(GameObject gameObject){
		 gameObject.GetComponent<GameManager>().storyDeck.populateDeck();
		 this.storyDeck = gameObject.GetComponent<GameManager>().storyDeck;
		 Debug.Log(this.storyDeck.storyDeck[1]);
		 Debug.Log(gameObject.GetComponent<GameManager>().storyDeck.storyDeck[1]);

	}


	public void PickUpAdventureCards(){
		if (!isLocalPlayer) {return;}
			if (isServer) {RpcPickUpAdventureCards();}
			else					{CmdPickUpAdventureCards();}
	}
	[Command]
	public void CmdPickUpAdventureCards(){
		RpcPickUpAdventureCards();
	}
	[ClientRpc]
	public void RpcPickUpAdventureCards(){
		 // advDeck = GameObject.Find("AdventureManager").GetComponent<AdventureDeck>();
		 // if (advDeck.adventureDeck.Count == 1) 	{ PopulateAdvDeck();}
		 string nameOfCard = advDeck.NewCard();
		 GameObject.FindGameObjectWithTag("AdvCardTextUI").GetComponent<Text>().text = nameOfCard;
	   advCard = advDeck.Draw(nameOfCard);
	   advCard.transform.SetParent (GameObject.Find ("HandCanvas"+ netId.Value).transform);
		 advCard.transform.localPosition = new Vector3 (0f, 0f, 0f);
	}

}
