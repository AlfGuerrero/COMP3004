 ﻿using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;
 using UnityEngine.Networking;

 public class QuestManagerScript : NetworkBehaviour {
	//Variables for sponsoring and participating
	[SyncVar]
	public int sponsor = 0;


	List<List<AdventureCard>> listOfStages = new List<List<AdventureCard>> ();
	//Info i;
	Info info;

	int currentPlayerTurn = 1;

 	//Variables for Initialize Stages
 	public int spacing = 200;

	GameObject stage;
	GameObject submitButton;
  public Logger logger;

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
		info = GameObject.Find("InfoHolder").GetComponent<Info>();
    logger = GameObject.Find("LoggerManager").GetComponent<Logger>().logger;
    logger.info ("QuestManager.cs :: Initialzing Quest Manager.");

		//info = i;
		//OnQuest ();
 	}

 	// Update is called once per frame
 	void Update () {
		int temp = int.Parse(GameObject.Find ("PlayerTurnTextUI").GetComponent<Text> ().text);
		int temp2 = int.Parse(GameObject.Find ("PlayerTurnTextUI").GetComponent<Text> ().text)/4;
		//might be ahead by 1
		currentPlayerTurn = int.Parse(GameObject.Find ("PlayerTurnTextUI").GetComponent<Text> ().text);//(temp+1) - temp2*4;
		// Debug.Log (currentPlayerTurn);

		// Debug.Log (info.stages.Length);

		/*if(info.startParticipantQuest){
			info.startParticipantQuest = false;
			SpawnStagesForParticipants ();
		}*/
	}

	public void PlaythroughFoe(){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcPlaythroughFoe();}
		else					{CmdPlaythroughFoe();}
	}

	[Command]
	public void CmdPlaythroughFoe(){
		RpcPlaythroughFoe ();
	}

	[ClientRpc]
	public void RpcPlaythroughFoe(){
		foreach(int f in info.participants){
			if(isLocalPlayer && netId.Value == f){
				Instantiate (stage, this.transform.GetChild (0));
			}
		}
		info.endPlayerSubmit = false;
	}



	public void PlaythroughQuest(){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcPlaythroughQuest();}
		else					{CmdPlaythroughQuest();}
	}

	[Command]
	public void CmdPlaythroughQuest(){
		RpcPlaythroughQuest();
	}

	[ClientRpc]
	public void RpcPlaythroughQuest(){
		if(info.participants.Count == 0){
			//quest is done
			Debug.Log("quest is done");
		}

		if(info.battlePointsPerStage.Length < info.currentStageInt){
			//quest is done
			Debug.Log("quest is done");
		}
		 else if (info.battlePointsPerStage [info.currentStageInt] == 0) {
			//test

		} else if (info.battlePointsPerStage [info.currentStageInt] > 0) {
			//foe
			PlaythroughFoe();
		}
		/*if(isLocalPlayer(){
		  	Instantiate(stage, GameObject.Find())
		  }
		*/

		//get participants

		//IF FOE THEN

		//spawn stages to submit for them

		//once submit is pressed #of participants, see if passed or failed

		//remove participants

		//IF TEST THEN

		//keep track of highest bid and who bid them

		//give submission box to sponsor +1 (record number of cards submit and hand them back

		//go to next participant (sponsor +2)

		//if submit number of cards is less than max, remove them from participate list

		//continue until participate list is length 1

		//END OF FOE AND TEST CHECKS

		//if is end of quest, dish out rewards

		//if not, runthrough again with stage number increase;


	}

	public void OnQuest(){
		info.ResetQuestValues (currentPlayerTurn);
	}

//	[Command]
//	public void CmdChooseSponsor(int playerTurn){
//		RpcChooseSponsor (playerTurn);
//	}
//	[ClientRpc]
//	public void RpcChooseSponsor(int playerTurn){
//		if(netId.Value == playerTurn){
//			info.sponsor = (int)netId.Value;
//			Debug.Log ("Sponsor ID: " + info.sponsor);
//		}
//	}
	public void SpawnStagesForParticipants(){
    logger.info ("QuestManager.cs :: SpawnStagesForParticipants() :: Spawning stages for Participants.");

		foreach(int f in info.participants){
			if(netId.Value == f){
        logger.info ("QuestManager.cs :: SpawnStagesForParticipants() :: Player " + f + " is a participant.");
				Instantiate (stage, GameObject.Find ("PlayerObject(Clone)" + f).transform.GetChild(0));
			}
		}
	}


 	public void InitializeStages(GameObject theSponsor){
 		//spawn stages for sponsor
		if(isLocalPlayer){
			info.currentQuestCard = GameObject.FindGameObjectWithTag("StoryCardTextUI").GetComponent<Text>().text;
			info.numStages = FindNumberOfStages (info.currentQuestCard);
			info.stages = new GameObject[info.numStages];
			for(int j = 0; j < info.numStages; j++){
				info.stages[j] = Instantiate (stage, theSponsor.transform);
				info.stages[j].transform.position = new Vector2((info.stages [j].transform.position.x + 2*j) /*+ 140*i) - spacing*/, info.stages[j].transform.position.y);
	 		}
		}

 		//then wait till submit button is pressed to continue to submissions for participants. info.e do nothing

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
		if(info.sponsorRound){
			if(info.trySponsor == netId.Value){
				// Debug.Log ("CurrentPLayerTurn Skipped Sponsoring");
        logger.info ("QuestManager.cs :: RpcPass() :: CurrentPLayerTurn Skipped Sponsoring ");

				info.trySponsor++;
				if(info.trySponsor > GameObject.Find("UsersManager").GetComponent<Users>().totalUsers ){
					info.trySponsor = 1;
				}
				info.sponsorPasses++;
				if (info.sponsorPasses == GameObject.Find("UsersManager").GetComponent<Users>().totalUsers) {
					// Debug.Log ("All Players Skipped Sponsoring");
          logger.info ("QuestManager.cs :: RpcPass() :: ALL players Skipped Sponsoring ");

					//resetQuestvalues
					//then skip quest and next card/turn
				}
			}
		}else if(info.participateRound){
			// Debug.Log ("Player Passed Participating");
      logger.info ("QuestManager.cs :: RpcPass() :: Player Passed Participating ");
			info.participantPasses++;
			this.gameObject.transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = false;
			this.gameObject.transform.GetChild(0).GetChild(6).GetComponent<Button>().interactable = false;
			if(info.participantPasses == GameObject.Find("UsersManager").GetComponent<Users>().totalUsers-1){
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
		if(netId.Value == info.trySponsor && info.sponsorRound){
			info.sponsor = (int)netId.Value;
			// Debug.Log ("The sponsor is: " + info.sponsor);
			GameObject[] sponsorbuttons = GameObject.FindGameObjectsWithTag ("sponsorButton");
			ChangeActive (sponsorbuttons, false);
			this.gameObject.transform.GetChild(0).GetChild(5).GetComponent<Button>().interactable = false;
			this.gameObject.transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = false;
			this.gameObject.transform.GetChild(0).GetChild(6).GetComponent<Button>().interactable = false;
			Debug.Log ("initializing stages");
			InitializeStages (GameObject.Find ("PlayerObject(Clone)" + info.sponsor.ToString ()).transform.GetChild (0).gameObject);
      logger.info ("QuestManager.cs :: RpcSponsorCurrentQuest() :: Initializing Stages ");

			// Debug.Log ("stages initialized");
			//info.questInProgress = true;
			info.sponsorRound = false;
			info.participateRound = true;
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
		if(info.participateRound){
			info.participants.Add((int)netId.Value);
      logger.info ("QuestManager.cs :: RpcParticipateInCurrentQuest() :: Player " + (int)netId.Value + "is participating.");

			this.gameObject.transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = false;
			Instantiate (stage, this.transform.GetChild (0));
			if(info.participants.Count + info.participantPasses + 1 == GameObject.Find("UsersManager").GetComponent<Users>().totalUsers){
        logger.info ("QuestManager.cs :: RpcParticipateInCurrentQuest() :: Starting The Quest");

				info.participateRound = false;
				info.startParticipantQuest = true;
				info.tempNumParticipants = info.participants.Count;
				//PlaythroughQuest ();
			}
		}
	}

	public void SetStages(){
		if (!isLocalPlayer) {return;}

		if (isServer) {RpcSetStages();}
		else					{CmdSetStages();}

	}

	[Command]
	public void CmdSetStages(){
		RpcSetStages ();
	}
	[ClientRpc]
	public void RpcSetStages(){
		info.SetStages (GameObject.FindGameObjectsWithTag ("Stage"));
		info.SetStages (listOfStages);
    logger.info ("QuestManager.cs :: RpcSetStages() ::Setting Stages! ");

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
	//	if(info.questInProgress == true && (int)netId.Value!=info.sponsor){
		foreach (int f in info.participants) {
			if (isLocalPlayer && (int)netId.Value == f) {
				GameObject submissionZone = GameObject.FindGameObjectWithTag ("Stage");
				//make a list of children (cards)
				List<AdventureCard> weaponsToSubmit = new List<AdventureCard> ();
				foreach (Transform j in submissionZone.transform) {
					//if contains a weapon
					Debug.Log (j.gameObject.GetComponent<AdventureCard> ().getType ());
					if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Weapon") {

						//check if duplicates of weapons
						if (sameName (j.gameObject.GetComponent<AdventureCard> ().getName (), weaponsToSubmit)) {
logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() :: Weapon of same name ");
						//	Debug.Log ("Weapons of same name");
							return;
						}

					}

					//if contains an ally then return
					if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Ally") {
            logger.info ("QuestManager.cs :: RpcSubmitWeaponsQueset() ::There is an ally ");
				//		Debug.Log ("Theres an ally");
						return;
					}
			//if contains a foe then return
			else if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Foe") {
        logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() :: There is a foe ");
					//	Debug.Log ("Theres a Foe");
						return;
					}
			//if contains a test then return
			else if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Test") {
        logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() :: There is a test ");
				//		Debug.Log ("Theres a Test");
						return;
					}

					weaponsToSubmit.Add (j.gameObject.GetComponent<AdventureCard> ());
					//Debug.Log (j.gameObject.GetComponent<AdventureCard>().getName());
				}
				IncreaseSubmites ();
				//		GameObject.Find ("QuestManager").GetComponent<QuestManager> ().setToggle (true);
				//		GameObject.Find ("QuestManager").GetComponent<QuestManager> ().setWeaponsSubmit (weaponsToSubmit);
				if (info.participants.Contains ((int)netId.Value)) {


					User participant = this.GetComponent<User> ();
					int userBP = participant.getBaseAttack ();

					foreach (AdventureCard bp in weaponsToSubmit) {
						userBP += bp.getBattlePoints ();
					}
          logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() :: " + participant.GetComponent<User> ().GetUsername () + " BP: " + userBP);


					List<List<AdventureCard>> currentQuest = info.listOfStages;
					List<AdventureCard> Stage = info.currentStage;

				//	Debug.Log ("Foe BP: " + info.battlePointsPerStage [info.currentStageInt]);
          logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() :: Foe BP: " + info.battlePointsPerStage [info.currentStageInt]);

					if (info.battlePointsPerStage [info.currentStageInt] > userBP) {
            logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() :: Player: " + netId.Value + " has too little BP to pass");

					//	Debug.Log ("Player: " + netId.Value + " has too little BP to pass");

						RemoveParticipant ((int)netId.Value);

					} else {
            logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() :: Player: " + netId.Value + " passed the stage");

						//Debug.Log ("Player: " + netId.Value + " passed the stage");
					}

					//NEED TO DESTROY SUB ZONE AND BUTTON
				//	Debug.Log ("Destroying zones");
          logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() :: Destorying Zones");



					//if all participants have submit something
					if (info.submitsForStage == info.tempNumParticipants) {
            logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() :: Hereos");

					//	Debug.Log ("hereos");
						foreach (GameObject g in GameObject.FindGameObjectsWithTag("Stage")) {
							Destroy (g);
              logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() ::  Destroyed a stage");

				//			Debug.Log ("destroyed a stagerino");
						}
						info.tempNumParticipants = info.participants.Count;
						info.endPlayerSubmit = true;
						info.currentStageInt++;
						//playthrough next questthing
					}
				}
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


	[ClientRpc]
	public void RpcSubmitCardsQuest(){
		if(info.questInProgress == false){
      logger.info ("QuestManager.cs :: RpcSubmitCardsQuest() :: Submitting stages for review");

	//	Debug.Log ("Submitting stages for review");
		//logger.info ("SubmitCards.cs :: Checking the current submission of cards for a quest");
		listOfStages = new List<List<AdventureCard>> ();
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
        logger.info ("QuestManager.cs :: RpcSubmitCardQuest() :: " + (j.gameObject.GetComponent<AdventureCard> ().getType ()));

		//		Debug.Log (j.gameObject.GetComponent<AdventureCard> ().getType ());
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
      // logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() :: bpps = " + bpps [listOfStages.IndexOf (k)]));

			//Debug.Log ("bpps = " + bpps [listOfStages.IndexOf (k)]);
		}

		for (int t = 1; t < numStages + 1; t++) {

			if (bpps [t - 1] != 0) {
				if (t < bpps.Length) {
					if (bpps [t - 1] > bpps [t]) {
						return;
					}
				}
			}
		}

    logger.info ("QuestManager.cs :: RpcSubmitWeaponsQuest() ::  Adding Stages");

	//	Debug.Log ("adding stages");
		//		foreach(List<AdventureCard> s in listOfStages){
		//			foreach(AdventureCard a in s){
		//				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ().advDeck.GetComponent<AdventureDeck> ().adventureDeck.Add (a.getName ());
		//			}
		//		}
		SetStages ();
		foreach (GameObject k in stages) {
			Destroy (k);
		}
			info.questInProgress = true;
			info.participateRound = true;
			SetBpps (bpps);

		}
	}

	public void SetBpps(int[] bpps){
		CmdSetBpps (bpps);
	}
	[Command]
	public void CmdSetBpps(int[] bpps){
		RpcSetBpps (bpps);
	}
	[ClientRpc]
	public void RpcSetBpps(int[] bpps){
		info.battlePointsPerStage = bpps;
	}

	public void RemoveParticipant(int i){
		CmdRemoveParticipant (i);
	}
	[Command]
	public void CmdRemoveParticipant(int i){
		RpcRemoveParticipant (i);
	}
	[ClientRpc]
	public void RpcRemoveParticipant(int i){
		info.participants.Remove (i);
	}

	public void IncreaseSubmites(){
		CmdIncreaseSubmites ();
	}
	[Command]
	public void CmdIncreaseSubmites(){
		RpcIncreaseSubmites ();
	}
	[ClientRpc]
	public void RpcIncreaseSubmites(){
		info.submitsForStage++;
	}
 }
