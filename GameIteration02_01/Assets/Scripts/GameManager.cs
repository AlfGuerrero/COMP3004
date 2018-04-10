using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour {
	// private StoryDeck 	storyDeck;
  // public AdventureDeck advDeck;
	public GameObject 	storyCard;
	public GameObject   advCard;
	GameObject[] 		storyCardDelete;
	StoryDeck sd;
	AdventureDeck ad;
	EventsManager eventsManager;
	Text playerTurn;

	void Start(){
		this.gameObject.name += netId.Value;
		GameObject.Find("HandCanvas").name += netId.Value;
//		CmdAddPlayer (playerSize);
	  	Debug.Log ("Player: " + netId.Value + " has joined.");
		 sd = GameObject.Find("StoryManager").GetComponent<StoryDeck>(); // GLOBAL OBJECT.
		 ad = GameObject.Find("AdventureManager").GetComponent<AdventureDeck>(); // GLOBAL OBJECT.
		 playerTurn = GameObject.Find("PlayerTurnTextUI").GetComponent<Text>();
		 eventsManager = GameObject.Find("EventsManager").GetComponent<EventsManager>(); // GLOBAL OBJECT.
		 // 
		 // for(int i = 0; i < 12; i++)
		 // PickUpAdventureCards();

	}

	void Update () {
		if (!isLocalPlayer) {
			return;
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
		string playerTurnText = playerTurn.text;
		int playerTurnInt;
	  int.TryParse (playerTurnText, out playerTurnInt);
		Debug.Log(NetworkServer.connections.Count);
	 if (playerTurnInt == 4){
		 playerTurnInt = 1;
	 }
	 else {
		 playerTurnInt++;
	 }
	 playerTurn.text = playerTurnInt.ToString();

	}


	public void PickUpStoryCard(){ // Local Button...
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcPickUpStoryCard();}
		else					{CmdPickUpStoryCard();}
	}
	[Command] // Server calls Clients...
		public void CmdPickUpStoryCard(){
		// storyCardDelete = GameObject.FindGameObjectsWithTag("StoryCard");
		RpcPickUpStoryCard();
	}
	[ClientRpc] // Clients call Server...
	public void RpcPickUpStoryCard(){
		storyCardDelete = GameObject.FindGameObjectsWithTag("StoryCard");
		foreach (GameObject i in storyCardDelete){
			DestroyObject (i);
		}
		if (sd.storyDeck.Count == 0) sd.populateDeck();
		string nameOfCard = sd.NewCard ();
		GameObject.FindGameObjectWithTag("StoryCardTextUI").GetComponent<Text>().text = nameOfCard;
		storyCard = sd.Draw(nameOfCard);
		storyCard.transform.SetParent (GameObject.Find ("GameCanvas").transform);
		storyCard.transform.localPosition = new Vector3 (-352f, 17f, 0f);

	 	if (storyCard.GetComponent<Event>() != null){
			Event eventCard = storyCard.GetComponent<Event>();
			if (eventCard.getName() == "King's Recognition" ){
				eventsManager.Kings_Recoginition(netId.Value);
			}
			else if (eventCard.getName() == "Queen's Favor"){
				// Debug.Log();
			}
			else if (eventCard.getName() == "Court Called to Camelot"){
				// Debug.Log();
			}
			else if (eventCard.getName() == "Pox"){
				// Debug.Log();
			}
			else if (eventCard.getName() == "Plague"){
				// Debug.Log();
			}
			else if (eventCard.getName() == "Chivalrous Deed"){
				// Debug.Log();
			}
			else if (eventCard.getName() == "Prosperity Throughout the Realm"){
				// Debug.Log();
			}
			else if (eventCard.getName() == "King's Call to Arms"){
				// Debug.Log();
			}

		}
		else if (storyCard.GetComponent<Quest>() != null){
			Quest questCard = storyCard.GetComponent<Quest>();
			Debug.Log(questCard.getName());
	  }
		else  if (storyCard.GetComponent<Tournament>() != null){
		 	Tournament tournamentCard = storyCard.GetComponent<Tournament>();
		 	Debug.Log(tournamentCard.getName());
		 }
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
		// advDeck.populateDeck();
	}

	public void PopulateStoryDeck(){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcPopulateStoryDeck(netId.Value);}
		else					{CmdPopulateStoryDeck(netId.Value);}
	}
	[Command]
	public void CmdPopulateStoryDeck(uint player){
    RpcPopulateStoryDeck(player);
	}
	[ClientRpc]
	public void RpcPopulateStoryDeck(uint player){
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
		 if (ad.adventureDeck	.Count == 0) 	{ ad.populateDeck();}
		 string nameOfCard = ad.NewCard();
		 GameObject.FindGameObjectWithTag("AdvCardTextUI").GetComponent<Text>().text = nameOfCard;
	   advCard = ad.Draw(nameOfCard);
	   advCard.transform.SetParent (GameObject.Find ("HandCanvas"+ netId.Value).transform);
		 advCard.transform.localPosition = new Vector3 (0f, 0f, 0f);
	}

}
