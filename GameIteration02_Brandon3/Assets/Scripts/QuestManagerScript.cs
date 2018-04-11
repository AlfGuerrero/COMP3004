 ﻿using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;
using UnityEngine.Networking;

 public class QuestManagerScript : NetworkBehaviour {
	//Variables for sponsoring and participating
	[SyncVar]
	public int sponsor = 0;



	Info i;
	Info info;

	int currentPlayerTurn = 1;

 	//Variables for Initialize Stages
 	public int spacing = 200;

	GameObject stage;
	GameObject submitButton;

// 	string currentQuestCard = "";
//
// 	int numStages;
//
//
// 	GameObject[] stages = null;
 	//---------------------------------\\

 	//List<List<AdventureCards>> stagesToPlaythroughWith = null;

	Dictionary<string, int> questCards = new Dictionary<string,int>{
 		{ "Boar Hunt", 2 },
 		{ "Search for the Holy Grail", 5 },
 		{ "Test of the Green Knight", 4 },
 		{ "Search for the Questing Beast", 4 },
 		{ "Defend the Queen's Honor", 4 },
 		{ "Rescue the Fair Maiden", 3 },
 		{ "Journey Through the Enchanted Forest", 3 },
 		{ "Vanquish King Arthur's Enemies", 3 },
 		{ "Slay The Dragon", 3 },
 		{ "Repel the Saxon Raiders", 2 }
 	};

 	// Use this for initialization
 	void Start () {
		stage = (GameObject)Resources.Load("PreFabs/QuestStage");
		submitButton = (GameObject)Resources.Load("PreFabs/SubmitButton");
		i = GameObject.Find("InfoHolder").GetComponent<Info>();
		info = i;
		OnQuest ();
 	}

 	// Update is called once per frame
 	void Update () {
		int temp = int.Parse(GameObject.Find ("PlayerTurnTextUI").GetComponent<Text> ().text);
		int temp2 = int.Parse(GameObject.Find ("PlayerTurnTextUI").GetComponent<Text> ().text)/4;
		//might be ahead by 1
		currentPlayerTurn = (temp+1) - temp2*4;
		Debug.Log (currentPlayerTurn);

		Debug.Log (i.stages.Length);

		/*if(info.startParticipantQuest){
			info.startParticipantQuest = false;
			SpawnStagesForParticipants ();
		}*/
	}

	public void OnQuest(){
		i.ResetQuestValues (currentPlayerTurn);
	}

//	[Command]
//	public void CmdChooseSponsor(int playerTurn){
//		RpcChooseSponsor (playerTurn);
//	}
//	[ClientRpc]
//	public void RpcChooseSponsor(int playerTurn){
//		if(netId.Value == playerTurn){
//			i.sponsor = (int)netId.Value;
//			Debug.Log ("Sponsor ID: " + i.sponsor);
//		}
//	}
	public void SpawnStagesForParticipants(){
		foreach(int f in info.participants){
			if(netId.Value == f){
				Instantiate (stage, GameObject.Find ("PlayerObject(Clone)" + f).transform.GetChild(0));
			}
		}
	}


 	public void InitializeStages(GameObject theSponsor){
 		//spawn stages for sponsor
		if(isLocalPlayer){
	 		i.currentQuestCard = GameObject.FindGameObjectWithTag("StoryCardTextUI").GetComponent<Text>().text;
	 		i.numStages = FindNumberOfStages (i.currentQuestCard);
	 		i.stages = new GameObject[i.numStages];
	 		for(int j = 0; j < i.numStages; j++){
	 			i.stages[j] = Instantiate (stage, theSponsor.transform);
				i.stages[j].transform.position = new Vector2((i.stages [j].transform.position.x + 2*j) /*+ 140*i) - spacing*/, i.stages[j].transform.position.y);
	 		}
		}

 		//then wait till submit button is pressed to continue to submissions for participants. i.e do nothing

 	}

 	int FindNumberOfStages(string questCard){
 		foreach(KeyValuePair<string, int> q in questCards){
 			if(q.Key == questCard){
 				return q.Value;
 			}
 		}
		return 0;
 	}

 	//public void RunThrough

	public void Pass(){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcPass();}
		else					{CmdPass();}

	}

	[Command]
	public void CmdPass(){
		RpcPass ();
	}
	[ClientRpc]
	public void RpcPass(){
		if(i.sponsorRound){
			if(i.trySponsor == netId.Value){
				Debug.Log ("CurrentPLayerTurn Skipped Sponsoring");
				i.trySponsor++;
				if(i.trySponsor > GameObject.Find("UsersManager").GetComponent<Users>().totalUsers ){
					i.trySponsor = 1;
				}
				i.sponsorPasses++;
				if (i.sponsorPasses == GameObject.Find("UsersManager").GetComponent<Users>().totalUsers) {
					Debug.Log ("All Players Skipped Sponsoring");
					//resetQuestvalues
					//then skip quest and next card/turn
				}
			}
		}else if(i.participateRound){
			Debug.Log ("Player Passed Participating");
			i.participantPasses++;
			this.gameObject.transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = false;
			this.gameObject.transform.GetChild(0).GetChild(6).GetComponent<Button>().interactable = false;
			if(i.participantPasses == GameObject.Find("UsersManager").GetComponent<Users>().totalUsers-1){
				Debug.Log ("All Players Passed Participating");
				//SPONSORDRAWS CARDS
				//resetQuestvalues
				//then skip quest and next card/turn
			}

		}
	}

	public void SponsorCurrentQuest(){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcSponsorCurrentQuest();}
		else					{CmdSponsorCurrentQuest();}

	}

	[Command]
	public void CmdSponsorCurrentQuest(){
		RpcSponsorCurrentQuest ();
	}
	[ClientRpc]
	public void RpcSponsorCurrentQuest(){
		Debug.Log ("called");
		if(netId.Value == i.trySponsor && i.sponsorRound){
			i.sponsor = (int)netId.Value;
			Debug.Log ("The sponsor is: " + i.sponsor);
			GameObject[] sponsorbuttons = GameObject.FindGameObjectsWithTag ("sponsorButton");
			ChangeActive (sponsorbuttons, false);
			this.gameObject.transform.GetChild(0).GetChild(5).GetComponent<Button>().interactable = false;
			this.gameObject.transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = false;
			this.gameObject.transform.GetChild(0).GetChild(6).GetComponent<Button>().interactable = false;
			Debug.Log ("initializing stages");
			InitializeStages (GameObject.Find ("PlayerObject(Clone)" + i.sponsor.ToString ()).transform.GetChild (0).gameObject);
			Debug.Log ("stages initialized");
			//i.questInProgress = true;
			i.sponsorRound = false;
			i.participateRound = true;
		}
	}

	public void ParticipateInCurrentQuest(){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcParticipateInCurrentQuest();}
		else					{CmdParticipateInCurrentQuest();}

	}

	[Command]
	public void CmdParticipateInCurrentQuest(){
		RpcParticipateInCurrentQuest ();
	}
	[ClientRpc]
	public void RpcParticipateInCurrentQuest(){
		Debug.Log ("called1");
		if(i.participateRound){
			i.participants.Add((int)netId.Value);
			Debug.Log ("Player "+ (int)netId.Value +"is participating.");
			this.gameObject.transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = false;
			if(i.participants.Count + i.participantPasses + 1 == GameObject.Find("UsersManager").GetComponent<Users>().totalUsers){
				Debug.Log ("STARTING THE QUEST");
				info.startParticipantQuest = true;

				foreach(int f in info.participants){
					if(isLocalPlayer){
						Instantiate (stage, GameObject.Find ("PlayerObject(Clone)" + f).transform.GetChild(0));
					}
				}
			}
		}
	}

	public void SetStages(){
		if (!isLocalPlayer) {return;}
		Debug.Log ("here1");
		if (isServer) {RpcSetStages();}
		else					{CmdSetStages();}

	}

	[Command]
	public void CmdSetStages(){
		RpcSetStages ();
	}
	[ClientRpc]
	public void RpcSetStages(){
		i.SetStages (GameObject.FindGameObjectsWithTag ("Stage"));
		Debug.Log ("Setting stages!");
	}

	void ChangeActive(GameObject[] array, bool tf){
		foreach (GameObject b in array) {
			Debug.Log ("Set a button to: "+tf);
			b.GetComponent<Button> ().interactable = tf;
		}
	}


	public void SubmitWeaponsQuest(){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcSubmitWeaponsQuest();}
		else					{CmdSubmitWeaponsQuest();}

	}

	[Command]
	public void CmdSubmitWeaponsQuest(){
		RpcSubmitWeaponsQuest ();
	}

	[ClientRpc]
	public void RpcSubmitWeaponsQuest(){
		if(info.questInProgress == false){
		GameObject submissionZone = GameObject.FindGameObjectWithTag ("Stage");
		//make a list of children (cards)
		List<AdventureCard> weaponsToSubmit = new List<AdventureCard>();
		foreach (Transform j in submissionZone.transform) {
			//if contains a weapon
			Debug.Log(j.gameObject.GetComponent<AdventureCard> ().getType ());
			if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Weapon") {

				//check if duplicates of weapons
				if (sameName (j.gameObject.GetComponent<AdventureCard> ().getName (), weaponsToSubmit)) {
					Debug.Log ("Weapons of same name");
					return;
				}

			}

			//if contains an ally then return
			if(j.gameObject.GetComponent<AdventureCard> ().getType () == "Ally"){
				Debug.Log ("Theres an ally");
				return;
			}
			//if contains a foe then return
			else if(j.gameObject.GetComponent<AdventureCard> ().getType () == "Foe"){
				Debug.Log ("Theres a Foe");
				return;
			}
			//if contains a test then return
			else if(j.gameObject.GetComponent<AdventureCard> ().getType () == "Test"){
				Debug.Log ("Theres a Test");
				return;
			}

			weaponsToSubmit.Add (j.gameObject.GetComponent<AdventureCard>());
			//Debug.Log (j.gameObject.GetComponent<AdventureCard>().getName());
		}

		//		GameObject.Find ("QuestManager").GetComponent<QuestManager> ().setToggle (true);
		//		GameObject.Find ("QuestManager").GetComponent<QuestManager> ().setWeaponsSubmit (weaponsToSubmit);
		if(i.participants.Contains((int)netId.Value)){


			User participant = this.GetComponent<User> ();
			int userBP = participant.getBaseAttack ();

			foreach (AdventureCard bp in weaponsToSubmit) {
				userBP += bp.getBattlePoints ();
			}
			Debug.Log(participant.GetComponent<User>().GetUsername() + "BP: " + userBP);

			List<List<AdventureCard>> currentQuest = info.listOfStages;
			List<AdventureCard> Stage = info.currentStage;

			Debug.Log ("Foe BP: " + foeBattlePoints (Stage));

			if (foeBattlePoints (Stage) > userBP) {
				Debug.Log("Player has too little BP to pass");
				info.participants.Remove ((int)netId.Value);

			}else{
				Debug.Log("Player passed the stage");
			}

			//NEED TO DESTROY SUB ZONE AND BUTTON
			Debug.Log("Destroying zones");
			//gm.keepPlaying = true;
			//		gm.playerTurn++;
			//		if(gm.playerTurn>4){
			//			gm.playerTurn = 0;
			//		}
			//gm.togglePlayerCanvas (gm.playerTurn);
			//gm.Quests.Playthrough (gm.currentUser.gameObject, gm.stageInt);
			Destroy (GameObject.FindGameObjectWithTag ("Stage"));
			//Destroy (GameObject.FindGameObjectWithTag ("Submit"));
		}
		}
		//GameObject.Find ("GameManager").GetComponent<GameManager> ().keepPlaying = true;
	}

	bool sameName(string name, List<AdventureCard> cards){
		for(int i = 0; i < cards.Count; i++){
			if(cards[i].getName() == name){
				return true;
			}
		}
		return false;
	}

	int foeBattlePoints(List<AdventureCard> stage){
		int bp = 0;
		Quest currentQuest = GameObject.FindGameObjectWithTag ("StoryCard").GetComponent<Quest> ();
		foreach(AdventureCard c in stage){
			if (c.getType () == "Foe" && c.getName () == currentQuest.getBonusFoe ()) {
				bp += c.getBonusBattlePoints ();
			} else {
				bp += c.getBattlePoints ();
			}
		}
		return bp;
	}

	public void SubmitCardsQuest(){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcSubmitCardsQuest();}
		else					{CmdSubmitCardsQuest();}
	}

	[Command]
	public void CmdSubmitCardsQuest(){
		RpcSubmitCardsQuest();
	}


	[Client]
	public void RpcSubmitCardsQuest(){
		if(info.questInProgress == false){	
		//logger.info ("SubmitCards.cs :: Checking the current submission of cards for a quest");
		List<List<AdventureCard>> listOfStages = new List<List<AdventureCard>> ();
		//get num stages and stage objects
		bool test = false;
		Quest storycard = GameObject.FindGameObjectWithTag ("StoryCard").GetComponent<Quest> ();
		int numStages = info.numStages;
		GameObject[] stages = info.stages;

		//check each stage submit is correct
		for (int i = 0; i < numStages; i++) {
			//logger.info ("SubmitCards.cs :: Checking if stage "+ i + " is eligible for submission");
			//Debug.Log(stages[i].GetComponent<RectTransform>().position.x);  <-----------Goes negative to positive
			bool foe = false;
			bool testCurrentStage = false;
			bool weapons = false;
			
			//make a list of children (cards)
			List<AdventureCard> cards = new List<AdventureCard> ();
			foreach (Transform j in stages[i].transform) {
				//if contains a weapon
				Debug.Log (j.gameObject.GetComponent<AdventureCard> ().getType ());
				if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Weapon") {
					//check if duplicates of weapons
					//	logger.info ("SubmitCards.cs :: There is a "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" card");
					weapons = true;
					if (sameName (j.gameObject.GetComponent<AdventureCard> ().getName (), cards)) {
						//		logger.warn ("SubmitCards.cs :: There are weapons of the same name. This submission is not eligible");
						return;
					}
				}
				//check if multiple tests in one stage and across all stages
				if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Test" && testCurrentStage == true) {
					//	logger.warn ("SubmitCards.cs :: There are multiple 'Test' cards in this stage. This submission is not eligible");
					return;
				} else if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Test" && test == true) {
					//	logger.warn ("SubmitCards.cs :: There are multiple 'Test' cards among all stages. This submission is not eligible");
					return;
				} else if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Test" && test == false) {
					test = true;
					testCurrentStage = true;
					//	logger.info ("SubmitCards.cs :: There is a "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" card");
				}

				//check if multiple foes are in single stage
				if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Foe" && foe == true) {
					//	logger.warn ("SubmitCards.cs :: There are multiple 'Foe' cards in this stage. This submission is not eligible");
					return;
				} else if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Foe" && foe == false) {
					//	logger.info ("SubmitCards.cs :: There is a "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" card");
					foe = true;
				}

				//if contains an ally then return
				if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Ally") {
					//	logger.info ("SubmitCards.cs :: This stage contains an 'Ally'. This submission is not eligible");
					return;
				}

				cards.Add (j.gameObject.GetComponent<AdventureCard> ());
				//logger.info ("SubmitCards.cs :: Adding the card "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" to the current stage");
				//Debug.Log (j.gameObject.GetComponent<AdventureCard>().getName());
			}
			//return if both a foe and test are present or neither are present
			if (testCurrentStage && foe) {
				//	logger.warn ("SubmitCards.cs :: This stage contains a 'Foe' and a 'Test' card. This submission is not elligible");
				return;
			} else if (!testCurrentStage && !foe) {
				//	logger.warn ("SubmitCards.cs :: This stage does not contain a 'Foe' or 'Test' card. This submission is not elligible");
				return;
			}

			if (testCurrentStage && weapons) {
				//	logger.warn ("SubmitCards.cs :: This stage contains a 'Test' and a 'Weapon' card. This submission is not eligible");
				return;
			}

			//	logger.info ("SubmitCards.cs :: Adding the current stage to the quest");
			listOfStages.Insert (i, cards);


		}
		int[] bpps = new int[numStages];

		for (int y = 0; y < numStages; y++) {
			bpps [y] = 0;
		}

		foreach (List<AdventureCard> k in listOfStages) {
			foreach (AdventureCard h in k) {
				//	logger.info ("SubmitCards.cs :: Calculating battle points for the current stage");
				if (h.getType () == "Test") {
					bpps [listOfStages.IndexOf (k)] = 0;
					//		logger.info ("SubmitCards.cs :: This stage has a 'Test' and requires no battle points");
				} else {
					if (h.getType () == "Weapon") {
						bpps [listOfStages.IndexOf (k)] += h.getBattlePoints ();

					} else if (h.getType () == "Foe") {
						if (storycard.getBonusFoe () == h.getName () || storycard.getBonusFoe () == "All") {
							//			logger.info ("SubmitCards.cs :: the 'Foe' name and bonus foe of the 'Quest' match. Using the higher of the two battle points");
							bpps [listOfStages.IndexOf (k)] += h.getBonusBattlePoints ();
						} else {
							bpps [listOfStages.IndexOf (k)] += h.getBattlePoints ();
							//			logger.info ("SubmitCards.cs :: the 'Foe' '"+ h.getName() +"' does not match the bonus foe of the 'Quest'. Using the lower of the two battle points");
						}
					}
				}
			}
			Debug.Log ("bpps = " + bpps [listOfStages.IndexOf (k)]);
		}
			info.battlePointsPerStage = bpps;
		for (int t = 1; t < numStages + 1; t++) {

			if (bpps [t - 1] != 0) {
				if (t < bpps.Length) {
					if (bpps [t - 1] > bpps [t]) {
						return;
					}
				}
			}
		}

		Debug.Log ("adding stages");
		//		foreach(List<AdventureCard> s in listOfStages){
		//			foreach(AdventureCard a in s){
		//				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ().advDeck.GetComponent<AdventureDeck> ().adventureDeck.Add (a.getName ());
		//			}
		//		}
		info.SetStages (listOfStages);
		foreach (GameObject k in stages) {
			Destroy (k);
		}
			info.questInProgress = true;
			info.participateRound = true;
		}
	}
 }
