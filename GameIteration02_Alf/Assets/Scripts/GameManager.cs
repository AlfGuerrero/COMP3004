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
	Users users;
	Text playerTurn;

	public Text names;
	public Text ranks;
	public Text shields;
	public Text battlePoints;

	public Text names_last;
	public Text ranks_last;
	public Text shields_last;
	public Text battlePoints_last;


	void Start(){
			this.gameObject.name += netId.Value;
			GameObject.Find("HandCanvas").name += netId.Value;
		 sd = GameObject.Find("StoryManager").GetComponent<StoryDeck>(); // GLOBAL OBJECT.
		 ad = GameObject.Find("AdventureManager").GetComponent<AdventureDeck>(); // GLOBAL OBJECT.
		 playerTurn = GameObject.Find("PlayerTurnTextUI").GetComponent<Text>();
		 eventsManager = GameObject.Find("EventsManager").GetComponent<EventsManager>(); // GLOBAL OBJECT.

		 users = GameObject.Find("UsersManager").GetComponent<Users>();
		 users.AddUser("PlayerObject(Clone)" + netId.Value);

		 names = GameObject.Find("UsernamesTextUI").GetComponent<Text>();
		 ranks = GameObject.Find("RanksTextUI").GetComponent<Text>();
		 shields = GameObject.Find("ShieldsTextUI").GetComponent<Text>();
		 battlePoints = GameObject.Find("BattlePointsTextUI").GetComponent<Text>();



		 DisplayUserInfo();

	}

	void OnApplicationQuit(){
		users.RemoveUser("PlayerObject(Clone)" + netId.Value);
	}

	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		// if (names_last.text != names.text || ranks_last.text != ranks.text || shields_last.text != shields.text || battlePoints_last.text != battlePoints.text){
			DisplayUserInfo();
			// names_last.text = names.text;
			// ranks_last.text = ranks.text;
			// shields_last.text = shields.text;
			// battlePoints_last.text = battlePoints.text;
		// }

	}

	public void DisplayUserInfo(){
		names.text = "";
		ranks.text = "";
		shields.text = "";
		battlePoints.text = "";

		foreach (GameObject i in users.GetUsers()) {
			names.text += i.GetComponent<User>().GetUsername() + "\n";
			ranks.text += i.GetComponent<User>().getRank() + "\n";
			shields.text += i.GetComponent<User>().getShields() + "\n";
			battlePoints.text += i.GetComponent<User>().getTotalBattlePoints() + "\n";
		}
	}
	public void ControlPlayerTurn(){
		if (!isLocalPlayer) {return;}

		// if (playerTurn.text != netId.Value.ToString()) return;

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
		// Debug.Log(NetworkServer.connections.Count);
	 if (playerTurnInt == users.GetTotalUsers()){
		 playerTurnInt = 1;
	 }

	 else {
		 playerTurnInt++;
	 }
	 playerTurn.text = playerTurnInt.ToString();

	}


	public void PickUpStoryCard(){ // Local Button...
		if (!isLocalPlayer) {return;}
		if (playerTurn.text != netId.Value.ToString()) return;

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
			List<GameObject> lowestUsers = 	eventsManager.Queens_Favor(users);
				foreach (GameObject i in lowestUsers){
					PickUpAdventureCardss(i.GetComponent<User>().GetNetID());
				}
				// Debug.Log();
			}
			else if (eventCard.getName() == "Court Called to Camelot"){
				eventsManager.Court_Called_To_Camelot(users);
				// Debug.Log();
			}
			else if (eventCard.getName() == "Pox"){
				User currentUser = GameObject.Find("PlayerObject(Clone)" + netId.Value).GetComponent<User>();
				eventsManager.Pox(currentUser, users);
				// Debug.Log();
			}
			else if (eventCard.getName() == "Plague"){
				User currentUser = GameObject.Find("PlayerObject(Clone)" + netId.Value).GetComponent<User>();
				eventsManager.Plague(currentUser);
				// Debug.Log();
			}
			else if (eventCard.getName() == "Chivalrous Deed"){
				User currentUser = GameObject.Find("PlayerObject(Clone)" + netId.Value).GetComponent<User>();
				eventsManager.Chivalrous_Deed(currentUser, users);
				// Debug.Log();
			}
			else if (eventCard.getName() == "Prosperity Throughout the Realm"){
				// Debug.Log();
				// User currentUser = GameObject.Find("PlayerObject(Clone)" + netId.Value).GetComponent<User>();
				// eventsManager.Prosperity_Throughout_The_Realm(currentUser, users);
				foreach (GameObject i in users.GetUsers()){
					PickUpAdventureCardss(i.GetComponent<User>().GetNetID());
				}

			}
			else if (eventCard.getName() == "King's Call to Arms"){
				// Debug.Log();
				eventsManager.Kings_Call_To_Arms(users);
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

	public void PickUpAdventureCardss(uint n){
		if (!isLocalPlayer) {return;}
			if (isServer) {RpcPickUpAdventureCardss(n);}
			else					{CmdPickUpAdventureCardss(n);}
	}
	[Command]
	public void CmdPickUpAdventureCardss(uint n){
		RpcPickUpAdventureCardss(n);
	}
	[ClientRpc]
	public void RpcPickUpAdventureCardss(uint n){
		 // advDeck = GameObject.Find("AdventureManager").GetComponent<AdventureDeck>();
		 if (ad.adventureDeck	.Count == 0) 	{ ad.populateDeck();}
		 string nameOfCard = ad.NewCard();
		 GameObject.FindGameObjectWithTag("AdvCardTextUI").GetComponent<Text>().text = nameOfCard;
		 advCard = ad.Draw(nameOfCard);
		 advCard.transform.SetParent (GameObject.Find ("HandCanvas"+ n).transform);
		 advCard.transform.localPosition = new Vector3 (0f, 0f, 0f);
	}
}
