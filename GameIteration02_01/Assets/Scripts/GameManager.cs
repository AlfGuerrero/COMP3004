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
			// advDeck = GameObject.Find("AdventureManager").GetComponent<AdventureDeck>();

			if (advDeck.adventureDeck.Count == 0)
			PopulateAdvDeck();

			if (storyDeck.storyDeck.Count == 0)
			PopulateStoryDeck();

			// for (int i = 0; i < 12; i++)
		  // PickUpAdventureCards();
	}
	// CurrentPlayer = "PlayerObject(Clone)" + netId.Value;
	// Update is called once per frame
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
		//
		// string playerTurnText = GameObject.FindGameObjectWithTag("PlayerTurnTextUI").GetComponent<Text>().text;
		// int playerTurnInt;
		// int.TryParse (playerTurnText, out playerTurnInt);
		// playerTurnInt++;
		//
		// GameObject.FindGameObjectWithTag ("PlayerTurnTextUI").GetComponent<Text> ().text = playerTurnInt.ToString();
		// CmdControlPlayerTurn ();

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
		storyCardDelete = GameObject.FindGameObjectsWithTag("StoryCard");
		foreach (GameObject i in storyCardDelete){
			DestroyObject (i);
		}
		string nameOfCard = storyDeck.NewCard ();
		if (isServer) {RpcPickUpStoryCard(nameOfCard);}
		else					{CmdPickUpStoryCard(nameOfCard);}

	}
	[Command] // Server calls Clients...
		public void CmdPickUpStoryCard(string nameOfCard){
		storyCardDelete = GameObject.FindGameObjectsWithTag("StoryCard");
		foreach (GameObject i in storyCardDelete){
					DestroyObject (i);
		}
		RpcPickUpStoryCard(nameOfCard);
	}
	[ClientRpc] // Clients call Server...
	public void RpcPickUpStoryCard(string nameOfCard){
		storyCardDelete = GameObject.FindGameObjectsWithTag("StoryCard");
		foreach (GameObject i in storyCardDelete){
			DestroyObject (i);
		}
		storyCard = storyDeck.Draw(nameOfCard);
		storyCard.transform.SetParent (GameObject.Find ("GameCanvas").transform);
		storyCard.transform.localPosition = new Vector3 (-352f, 17f, 0f);
		GameObject.FindGameObjectWithTag("StoryCardTextUI").GetComponent<Text>().text = nameOfCard;
	}

	public void PopulateAdvDeck(){
		if (!isLocalPlayer) {return;}
			if (isServer) {RpcPopulateAdvDeck();}
			else					{CmdPopulateAdvDeck();}	}
	[Command]
	public void CmdPopulateAdvDeck(){
		// RpcPopulateAdvDeck();
	}
	[ClientRpc]
	public void RpcPopulateAdvDeck(){
		advDeck.populateDeck();
	}

	public void PopulateStoryDeck(){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcPopulateStoryDeck();}
		else					{CmdPopulateStoryDeck();}
	}
	[Command]
	public void CmdPopulateStoryDeck(){
	RpcPopulateStoryDeck();
	}
	[ClientRpc]
	public void RpcPopulateStoryDeck(){
		storyDeck.populateDeck();
	}

	public void PickUpAdventureCards(){
		if (!isLocalPlayer) {return;}
			// string nameOfCard = advDeck.NewCard(); // changes array..
		  // advCard = advDeck.Draw(nameOfCard); // creates prefab with name from that...
			// advCard.transform.SetParent (GameObject.Find ("HandCanvas"+ netId.Value).transform);
			// advCard.transform.localPosition = new Vector3 (0f, 0f, 0f);
			string nameOfCard = advDeck.NewCard();

			if (isServer) {RpcPickUpAdventureCards(nameOfCard);}
			else					{CmdPickUpAdventureCards(nameOfCard);}
	}
	[Command]
	public void CmdPickUpAdventureCards( string nameOfCard){
		RpcPickUpAdventureCards(nameOfCard);
	}
	[ClientRpc]
	public void RpcPickUpAdventureCards( string nameOfCard){
	   advCard = advDeck.Draw(nameOfCard);
	   advCard.transform.SetParent (GameObject.Find ("HandCanvas"+ netId.Value).transform);
		 advCard.transform.localPosition = new Vector3 (0f, 0f, 0f);
	}

}
