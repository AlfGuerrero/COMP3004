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
	public bool statsToggle=false;
	GameObject[] 		storyCardDelete;
	public List<uint> PlayerIds=new List<uint>();
  GameObject[]    advCardDelete;

	void Start(){
		this.gameObject.name += netId.Value;
		GameObject.Find("HandCanvas").name += netId.Value;
//		CmdAddPlayer (playerSize);
	  	Debug.Log ("Player: " + netId.Value + " has joined.");
		GameObject.Find ("TabCanvas").SetActive(false);
			// advDeck = GameObject.Find("AdventureManager").GetComponent<AdventureDeck>();

			if (isServer && advDeck.adventureDeck.Count == 0)
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
		TabKeyCheck();
	}

	void OnApplicationQuit(){
		Debug.Log("Exiting...");

		advCardDelete = GameObject.FindGameObjectsWithTag("Card");
		foreach (GameObject i in advCardDelete){
			DestroyObject (i);
		}

	}

	//Checking to see if any of the keybaord keys are currently pressed.
	public void TabKeyCheck(){
		if (Input.GetKeyDown ("tab")) {
			statsToggle = true;
			Debug.Log ("her");
			GameObject.Find ("This").transform.GetChild(0).gameObject.SetActive(true);
			DisplayUserInfo ();
			/*
			PlayerStats = Resources.Load ("PreFabs/Stats") as GameObject;
			PlayerStats = Instantiate (PlayerStats, GameObject.Find("MainUI").transform);
			*/
			Debug.Log ("Showing the player data");
			//The tab key is no longer being pressed, remove the data from the screen.
		}if ((statsToggle == true) && Input.GetKeyUp ("tab")) {
			GameObject.Find ("TabCanvas").SetActive(false);
			Debug.Log ("Removing info from screen");
		}
		/*
			logger.info ("GameManager.cs :: 'tab' key has been pressed toggling Player Statistics.");
			Destroy (PlayerStats.gameObject);
			foreach (GameObject g in GameObject.FindGameObjectsWithTag ("SmallCard")) {
				Destroy (g);
			}
			statsToggle = false;
		}
		if(GameObject.Find ("Button (1)") != null){

		GameObject.Find ("Button (1)").GetComponent<Button>().onClick.AddListener(delegate {buttonToggle();});

		}

			*/
	}
	public void DisplayUserInfo(){
		Debug.Log ("Displaying information");

		foreach (int CurrentPlayer in PlayerIds) {
			//GameObject.Find (CurrentPlayer).GetComponent (name);
			//GameObject.Find("TabCanvas").transform.GetChild(0).GetComponent<Text>().text=;

			GameObject.Find("TabCanvas").transform.GetChild(0).GetComponent<Text>().text=GameObject.Find("PlayerObject(Clone)").GetComponent<User>().GetUsername();
			GameObject.Find("TabCanvas").transform.GetChild(1).GetComponent<Text>().text=GameObject.Find("PlayerObject(Clone)").GetComponent<User>().getRank();
			GameObject.Find("TabCanvas").transform.GetChild(2).GetComponent<Text>().text=GameObject.Find("PlayerObject(Clone)").GetComponent<User>().getShields().ToString();
			GameObject.Find("TabCanvas").transform.GetChild(3).GetComponent<Text>().text=GameObject.Find("PlayerObject(Clone)").GetComponent<User>().getTotalBattlePoints().ToString();
			//	GameObject.Find("TabCanvas").transform.GetChild(4).GetComponent<Text>().text=GameObject.Find("PlayerObject(Clone)").GetComponent<User>().numOfcards().ToString();
		}




		/*

		string names = "", Ranks = "", shields = "",BattlePoints="",CardsInHand="";
		//int BattlePoints = 0,CardsInHand;
		foreach (int CurrentPlayer in PlayerIds) {
			//GameObject.Find (CurrentPlayer).GetComponent (name);


			names += (GameObject.Find("PlayerObject(Clone)" +CurrentPlayer).GetComponent<User>().GetUsername());
			Ranks+= (GameObject.Find("PlayerObject(Clone)" +CurrentPlayer).GetComponent<User>().getRank())+ "\n";
			shields += (GameObject.Find("PlayerObject(Clone)" +CurrentPlayer).GetComponent<User>().getShields())+ "\n";
			BattlePoints += (GameObject.Find("PlayerObject(Clone)" +CurrentPlayer).GetComponent<User>().getTotalBattlePoints())+ "\n";
			CardsInHand += (GameObject.Find("PlayerObject(Clone)" +CurrentPlayer).GetComponent<User>().numOfcards())+ "\n";
		}
			*/
		/*
		PlayerStats.transform.GetChild(0).GetComponent<Text>().text =  "Player: " + names;
		PlayerStats.transform.GetChild(1).GetComponent<Text>().text =  "Rank: " + ranks;
		PlayerStats.transform.GetChild(2).GetComponent<Text>().text =  "Shields: " + shields;
		PlayerStats.transform.GetChild(3).GetComponent<Text>().text =  "Base Points: " 	+ battlePoint;
		PlayerStats.transform.GetChild(4).GetComponent<Text>().text =  "Cards In Hand: "	+ cardsinHand;
		logger.info ("GameManager.cs ::" + "Players: "+ names);
		logger.info ("GameManager.cs ::" + "Rank: "+ ranks);
		logger.info ("GameManager.cs ::" + "Shields: " + shields);
		logger.info ("GameManager.cs ::" + "Base Points: "	+ battlePoint);
		logger.info ("GameManager.cs ::" + "Cards In Hand: "+ cardsinHand);

		*/
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
	  RpcPopulateAdvDeck();
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
			string nameOfCard = advDeck.NewCard(); // changes array..
		  advCard = advDeck.Draw(nameOfCard); // creates prefab with name from that...
			advCard.transform.SetParent (GameObject.Find ("HandCanvas"+ netId.Value).transform);
			advCard.transform.localPosition = new Vector3 (0f, 0f, 0f);

			if (isServer) {RpcPickUpAdventureCards(nameOfCard);}
			else					{CmdPickUpAdventureCards(nameOfCard);}
	}
	[Command]
	public void CmdPickUpAdventureCards(string nameOfCard){
		RpcPickUpAdventureCards(nameOfCard);
	}
	[ClientRpc]
	public void RpcPickUpAdventureCards(string nameOfCard){
	   advCard = advDeck.Draw(nameOfCard);
	   advCard.transform.SetParent (GameObject.Find ("HandCanvas"+ netId.Value).transform);
		 advCard.transform.localPosition = new Vector3 (0f, 0f, 0f);
	}

}
