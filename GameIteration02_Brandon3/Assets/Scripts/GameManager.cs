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
	TournamentHolder tournamentHolder;
	Users users;
	Text playerTurn;
	Info i;
	QuestInfo questInfo;
	public Text names;
	public Text ranks;
	public Text shields;
	public Text battlePoints;

	public Text names_last;
	public Text ranks_last;
	public Text shields_last;
	public Text battlePoints_last;
	public Logger logger;

	void Start(){

		 this.gameObject.name += netId.Value;
		GameObject.Find("HandCanvas").name += netId.Value;
		 sd = GameObject.Find("StoryManager").GetComponent<StoryDeck>(); // GLOBAL OBJECT.
		 ad = GameObject.Find("AdventureManager").GetComponent<AdventureDeck>(); // GLOBAL OBJECT.
		 playerTurn = GameObject.Find("PlayerTurnTextUI").GetComponent<Text>();
		 eventsManager = GameObject.Find("EventsManager").GetComponent<EventsManager>(); // GLOBAL OBJECT.
		 tournamentHolder = GameObject.Find("TournamentHolder").GetComponent<TournamentHolder>();
		 i = GameObject.Find("InfoHolder").GetComponent<Info>();
		 users = GameObject.Find("UsersManager").GetComponent<Users>();
		 users.AddUser("PlayerObject(Clone)" + netId.Value);
		 questInfo = GameObject.Find("QuestInfo").GetComponent<QuestInfo>();
		 names = GameObject.Find("UsernamesTextUI").GetComponent<Text>();
		 ranks = GameObject.Find("RanksTextUI").GetComponent<Text>();
		 shields = GameObject.Find("ShieldsTextUI").GetComponent<Text>();
		 battlePoints = GameObject.Find("BattlePointsTextUI").GetComponent<Text>();
		 logger = GameObject.Find("LoggerManager").GetComponent<Logger>().logger;
		 DisplayUserInfo();
		 logger.info ("GameManager.cs :: Initialzing Game Manager.");

	}

	void OnApplicationQuit(){
		users.RemoveUser("PlayerObject(Clone)" + netId.Value);
		logger.info ("GameManager.cs :: Qutting Game Manager.");

	}

	void Update () {
		if (!isLocalPlayer) {
			return;
		}
			DisplayUserInfo();
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
		// if (playerTurn.text != netId.Value.ToString()) return;

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
		logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Picking up story card... ");

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
				logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Event Card :: " + eventCard.getName());

				logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Connection " + netId.Value + " :: Calling Event Manager...");
				eventsManager.Kings_Recoginition(netId.Value);
			}
			else if (eventCard.getName() == "Queen's Favor"){
			logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Event Card :: " + eventCard.getName());
			logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Calling Event Manager...");

			List<GameObject> lowestUsers = 	eventsManager.Queens_Favor(users);

				foreach (GameObject i in lowestUsers){
					PickUpAdventureCardss(i.GetComponent<User>().GetNetID());
				}
				// Debug.Log();
			}
			else if (eventCard.getName() == "Court Called to Camelot"){
				logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Event Card :: " + eventCard.getName());

				eventsManager.Court_Called_To_Camelot(users);
				// Debug.Log();
			}
			else if (eventCard.getName() == "Pox"){
				logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Event Card :: " + eventCard.getName());

				User currentUser = GameObject.Find("PlayerObject(Clone)" + netId.Value).GetComponent<User>();
				eventsManager.Pox(currentUser, users);
				// Debug.Log();
			}
			else if (eventCard.getName() == "Plague"){
				logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Event Card :: " + eventCard.getName());

				User currentUser = GameObject.Find("PlayerObject(Clone)" + netId.Value).GetComponent<User>();
				eventsManager.Plague(currentUser);
				// Debug.Log();
			}
			else if (eventCard.getName() == "Chivalrous Deed"){
				logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Event Card :: " + eventCard.getName());

				User currentUser = GameObject.Find("PlayerObject(Clone)" + netId.Value).GetComponent<User>();
				eventsManager.Chivalrous_Deed(currentUser, users);
				// Debug.Log();
			}
			else if (eventCard.getName() == "Prosperity Throughout the Realm"){
				logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Event Card :: " + eventCard.getName());
				eventsManager.Prosperity_Throughout_The_Realm(users);
				// Debug.Log();
				// User currentUser = GameObject.Find("PlayerObject(Clone)" + netId.Value).GetComponent<User>();
				// eventsManager.Prosperity_Throughout_The_Realm(currentUser, users);
				foreach (GameObject i in users.GetUsers()){
					for (int s = 0; s < 2; s++)
					PickUpAdventureCardss(i.GetComponent<User>().GetNetID());
				}

			}
			else if (eventCard.getName() == "King's Call to Arms"){
				logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Event Card :: " + eventCard.getName());

				// Debug.Log();
				eventsManager.Kings_Call_To_Arms(users);
			}
			questInfo.startSponsor = false;

			tournamentHolder.tournamentInProgress = false;

		}
		else if (storyCard.GetComponent<Quest>() != null){
			Quest questCard = storyCard.GetComponent<Quest>();
			logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Quest Card :: " + questCard.getName());
			logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Connection " + netId.Value + " :: Calling Quest Manager... " );

			questInfo.startSponsor = true;
			tournamentHolder.tournamentInProgress = false;

			// i.ResetQuestValues ((int)netId.Value);
	  }
		else if (storyCard.GetComponent<Tournament>() != null){
		 	Tournament tournamentCard = storyCard.GetComponent<Tournament>();
			logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Tournament Card :: " + tournamentCard.getName());
			logger.info ("GameManager.cs :: RpcPickUpStoryCard() :: Connection " + netId.Value + " :: Calling Tournament Manager... " );

			this.GetComponent<TournamentManager>().tourniSetUp(tournamentCard);
			tournamentHolder.tournamentInProgress = true;

			questInfo.startSponsor = false;

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

		if (ad.adventureDeck.Count == 0) {
			ad.populateDeck ();
		}
		string nameOfCard = ad.NewCard ();
		GameObject.FindGameObjectWithTag ("AdvCardTextUI").GetComponent<Text> ().text = nameOfCard;
	//	if (isLocalPlayer) {
		advCard = ad.Draw (nameOfCard);
		logger.info ("GameManager.cs :: RpcPickUpAdventureCards() :: Connection " + netId.Value + " :: Draws :: " + nameOfCard );

		advCard.transform.SetParent (GameObject.Find ("HandCanvas" + netId.Value).transform);
		advCard.transform.localPosition = new Vector3 (0f, 0f, 0f);
	//}
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
		if (ad.adventureDeck.Count == 0) {
			ad.populateDeck ();
		}
		string nameOfCard = ad.NewCard ();
		GameObject.FindGameObjectWithTag ("AdvCardTextUI").GetComponent<Text> ().text = nameOfCard;
	//	if (!isLocalPlayer) {
			advCard = ad.Draw (nameOfCard);
			logger.info ("GameManager.cs :: RpcPickUpAdventureCards() :: Connection " + netId.Value + " :: Draws :: " + nameOfCard );
			advCard.transform.SetParent (GameObject.Find ("HandCanvas" + n).transform);
			advCard.transform.localPosition = new Vector3 (0f, 0f, 0f);

	//	}
	}
}
