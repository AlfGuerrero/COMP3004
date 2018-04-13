using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class QuestManager : NetworkBehaviour {

	//Initial varables
	GameObject stage;
	QuestInfo info;
	Quest questCard;
	Logger logger;
	List<List<AdventureCard>> listOfStages = new List<List<AdventureCard>> ();
	int[] bpps = new int[5];


	[SyncVar]
	public int stage_1 = 0;
	[SyncVar]
	public int stage_2 = 0;
	[SyncVar]
	public int stage_3 = 0;
	[SyncVar]
	public int stage_4 = 0;
	[SyncVar]
	public int stage_5 = 0;


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

	//sponsor
	public bool sponsoringQuest = true;


	// Use this for initialization
	void Start () {
		stage = (GameObject)Resources.Load("Prefabs/QuestStage");
		info = GameObject.Find ("QuestInfo").GetComponent<QuestInfo> ();
		logger = GameObject.Find("LoggerManager").GetComponent<Logger>().logger;
		logger.info ("QuestManager.cs :: Initialzing Quest Manager.");
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnQuest(){
		if (isServer) {RpcSponsorQuest();}
		else					{CmdSponsorQuest();}
	}
	[Command]
	public void CmdOnQuest(){
		RpcOnQuest ();
	}

	[ClientRpc]
	public void RpcOnQuest(){
		questCard = GameObject.FindGameObjectWithTag ("StoryCard").GetComponent<Quest> ();
		logger.info("QuestManager.cs :: RpcOnQuest() :: " + questCard.getName());

		info.startSponsor = true;
	}


	public void SponsorQuest(){
		if (!isLocalPlayer) {return;}
		if (info.startSponsor) {
			logger.info("QuestManager.cs :: SponsorQuest() :: Spawning stages for sponsor");
			questCard = GameObject.FindGameObjectWithTag ("StoryCard").GetComponent<Quest> ();
			int numStages = FindNumberOfStages(GameObject.Find ("StoryCardTextUI").GetComponent<Text> ().text);
			info.stages = new GameObject[numStages];
			for(int j = 0; j < numStages; j++){
				info.stages[j] = Instantiate (stage, this.transform.GetChild(0));
				info.stages[j].transform.position = new Vector2((info.stages [j].transform.position.x + 2*j), info.stages[j].transform.position.y);
			}
		}

		if (isServer) {RpcSponsorQuest();}
		else					{CmdSponsorQuest();}
	}
	[Command]
	public void CmdSponsorQuest(){
		RpcSponsorQuest ();
	}

	[ClientRpc]
	public void RpcSponsorQuest(){
		//int numStages = FindNumberOfStages(GameObject.Find ("StoryCardTextUI").GetComponent<Text> ().text);
		// info.stages = GameObject.FindGameObjectsWithTag("Stage");
		logger.info("QuestManager.cs :: RpcSponsorQuest() :: Player " + (int)netId.Value + " has sponsored the quest.");

		info.sponsor = (int)netId.Value;
		info.startSponsor = false;
		info.overallQuestStarted = true;
	}



	public void SponsorSubmitCards(){
		if (!isLocalPlayer) {return;}
		// foreach (Transform j in GameObject.Find("QuestStage(Clone)").transform) {
		// 	// Debug.Log("Cards: " + j.GetComponent<AdventureCard>());
		// 	//this.GetComponent<User>().SetTourni(j.GetComponent<AdventureCard>());
		// 	// += j.GetComponent<AdventureCard>().getBattlePoints();
		// }
		//checkCards

		// if (info.overallQuestStarted == true){
		// 	bool test = false;
		// 	listOfStages = new List<List<AdventureCard>> ();
		//
		// 	Debug.Log("Submitting Stages..");
		//
		// 	//check each stage submit is correct
		// 	for (int i = 0; i < info.stages.Length; i++) {
		// 		logger.info ("SubmitCards.cs :: Checking if stage "+ i + " is eligible for submission");
		// 		//Debug.Log(stages[i].GetComponent<RectTransform>().position.x);  <-----------Goes negative to positive
		// 		bool foe = false;
		// 		bool testCurrentStage = false;
		// 		bool weapons = false;
		//
		// 		//make a list of children (cards)
		// 		List<AdventureCard> cards = new List<AdventureCard> ();
		// 		foreach (Transform j in info.stages[i].transform) {
		// 			//if contains a weapon
		// 			Debug.Log (j.gameObject.GetComponent<AdventureCard> ().getType ());
		// 			if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Weapon") {
		// 				//check if duplicates of weapons
		// 				logger.info ("QuestManager.cs :: There is a "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" card");
		// 				weapons = true;
		// 				if (sameName (j.gameObject.GetComponent<AdventureCard> ().getName (), cards)) {
		// 					logger.warn ("QuestManager.cs :: There are weapons of the same name. This submission is not eligible");
		// 					return;
		// 				}
		// 			}
		// 			//check if multiple tests in one stage and across all stages
		// 			if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Test" && testCurrentStage == true) {
		// 				logger.warn ("QuestManager.cs :: There are multiple 'Test' cards in this stage. This submission is not eligible");
		// 				return;
		// 				}
		// 				 else if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Test" && test == true) {
		// 					logger.warn ("QuestManager.cs :: There are multiple 'Test' cards among all stages. This submission is not eligible");
		// 					return;
		// 					} else if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Test" && test == false) {
		// 						test = true;
		// 						testCurrentStage = true;
		// 						logger.info ("QuestManager.cs :: There is a "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" card");
		// 					}
		// 					//check if multiple foes are in single stage
		// 					if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Foe" && foe == true) {
		// 						logger.warn ("QuestManager.cs :: There are multiple 'Foe' cards in this stage. This submission is not eligible");
		// 						return;
		// 						} else if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Foe" && foe == false) {
		// 							logger.info ("QuestManager.cs :: There is a "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" card");
		// 							foe = true;
		// 						}
		// 						//if contains an ally then return
		// 						if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Ally") {
		// 							logger.info ("QuestManager.cs :: This stage contains an 'Ally'. This submission is not eligible");
		// 							return;
		// 						}
		// 						cards.Add (j.gameObject.GetComponent<AdventureCard> ());
		// 						logger.info ("QuestManager.cs :: Adding the card "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" to the current stage");
		// 						//Debug.Log (j.gameObject.GetComponent<AdventureCard>().getName());
		// 					}
		// 					//return if both a foe and test are present or neither are present
		// 					if (testCurrentStage && foe) {
		// 						logger.warn ("QuestManager.cs :: This stage contains a 'Foe' and a 'Test' card. This submission is not elligible");
		// 						return;
		// 					} else if (!testCurrentStage && !foe) {
		// 						logger.warn ("QuestManager.cs :: This stage does not contain a 'Foe' or 'Test' card. This submission is not elligible");
		// 						return;
		// 					}
		//
		// 					if (testCurrentStage && weapons) {
		// 						logger.warn ("QuestManager.cs :: This stage contains a 'Test' and a 'Weapon' card. This submission is not eligible");
		// 						return;
		// 					}
		//
		// 					logger.info ("QuestManager.cs :: Adding the current stage to the quest");
		// 					info.listOfStages.Insert (i, cards);
		//
		// 				}
		// 				//
		// 				bpps = new int[info.stages.Length];
		//
		// 				for (int y = 0; y < info.stages.Length; y++) {
		// 					bpps [y] = 0;
		// 				}
		// 				// //
		// 				foreach (List<AdventureCard> k in info.listOfStages) {
		// 					foreach (AdventureCard h in k) {
		// 						//	logger.info ("SubmitCards.cs :: Calculating battle points for the current stage");
		// 						if (h.getType () == "Test") {
		// 							bpps [info.listOfStages.IndexOf (k)] = 0;
		// 							//		logger.info ("SubmitCards.cs :: This stage has a 'Test' and requires no battle points");
		// 						} else {
		// 							if (h.getType () == "Weapon") {
		// 								bpps [info.listOfStages.IndexOf (k)] += h.getBattlePoints ();
		//
		// 								} else if (h.getType () == "Foe") {
		// 									if (questCard.getBonusFoe () == h.getName () || questCard.getBonusFoe () == "All") {
		// 										//			logger.info ("SubmitCards.cs :: the 'Foe' name and bonus foe of the 'Quest' match. Using the higher of the two battle points");
		// 										bpps [info.listOfStages.IndexOf (k)] += h.getBonusBattlePoints ();
		// 									} else {
		// 										bpps [info.listOfStages.IndexOf (k)] += h.getBattlePoints ();
		// 										//			logger.info ("SubmitCards.cs :: the 'Foe' '"+ h.getName() +"' does not match the bonus foe of the 'Quest'. Using the lower of the two battle points");
		// 									}
		// 								}
		// 							}
		// 						}
		// 						Debug.Log ("bpps = " + bpps [info.listOfStages.IndexOf (k)]);
		// 					}
		//
		// 						for (int t = 1; t < info.stages.Length + 1; t++) {
		//
		// 							if (bpps [t - 1] != 0) {
		// 								if (t < bpps.Length) {
		// 									if (bpps [t - 1] > bpps [t]) {
		// 										return;
		// 									}
		// 								}
		// 							}
		// 						}
		// 						Debug.Log ("adding stages");
		// 						info.bpps = bpps;
		//
		// 						GameObject[] stagesToDestroy = GameObject.FindGameObjectsWithTag("Stage");
		// 						foreach(GameObject g in stagesToDestroy){
		// 							Destroy(g);
		// 						}
		//
		// 				}
						if (isServer) {RpcSponsorSubmitCards();}
						else					{CmdSponsorSubmitCards();}
					}
					[Command]
					public void CmdSponsorSubmitCards(){
						RpcSponsorSubmitCards ();
					}

					[ClientRpc]
					public void RpcSponsorSubmitCards(){
						if (info.overallQuestStarted == true){
							bool test = false;
							listOfStages = new List<List<AdventureCard>> ();
							// info.sponsor = 2;
							Debug.Log("Submitting Stages..");

							//check each stage submit is correct
							for (int i = 0; i < info.stages.Length; i++) {
								logger.info ("SubmitCards.cs :: Checking if stage "+ i + " is eligible for submission");
								//Debug.Log(stages[i].GetComponent<RectTransform>().position.x);  <-----------Goes negative to positive
								bool foe = false;
								bool testCurrentStage = false;
								bool weapons = false;

								//make a list of children (cards)
								List<AdventureCard> cards = new List<AdventureCard> ();
								foreach (Transform j in info.stages[i].transform) {
									//if contains a weapon
									Debug.Log (j.gameObject.GetComponent<AdventureCard> ().getType ());
									if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Weapon") {
										//check if duplicates of weapons
										logger.info ("QuestManager.cs :: There is a "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" card");
										weapons = true;
										if (sameName (j.gameObject.GetComponent<AdventureCard> ().getName (), cards)) {
											logger.warn ("QuestManager.cs :: There are weapons of the same name. This submission is not eligible");
											return;
										}
									}
									//check if multiple tests in one stage and across all stages
									if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Test" && testCurrentStage == true) {
										logger.warn ("QuestManager.cs :: There are multiple 'Test' cards in this stage. This submission is not eligible");
										return;
										}
										 else if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Test" && test == true) {
											logger.warn ("QuestManager.cs :: There are multiple 'Test' cards among all stages. This submission is not eligible");
											return;
											} else if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Test" && test == false) {
												test = true;
												testCurrentStage = true;
												logger.info ("QuestManager.cs :: There is a "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" card");
											}
											//check if multiple foes are in single stage
											if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Foe" && foe == true) {
												logger.warn ("QuestManager.cs :: There are multiple 'Foe' cards in this stage. This submission is not eligible");
												return;
												} else if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Foe" && foe == false) {
													logger.info ("QuestManager.cs :: There is a "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" card");
													foe = true;
												}
												//if contains an ally then return
												if (j.gameObject.GetComponent<AdventureCard> ().getType () == "Ally") {
													logger.info ("QuestManager.cs :: This stage contains an 'Ally'. This submission is not eligible");
													return;
												}
												cards.Add (j.gameObject.GetComponent<AdventureCard> ());
												logger.info ("QuestManager.cs :: Adding the card "+ j.gameObject.GetComponent<AdventureCard> ().getName() +" to the current stage");
												//Debug.Log (j.gameObject.GetComponent<AdventureCard>().getName());
											}
											//return if both a foe and test are present or neither are present
											if (testCurrentStage && foe) {
												logger.warn ("QuestManager.cs :: This stage contains a 'Foe' and a 'Test' card. This submission is not elligible");
												return;
											} else if (!testCurrentStage && !foe) {
												logger.warn ("QuestManager.cs :: This stage does not contain a 'Foe' or 'Test' card. This submission is not elligible");
												return;
											}

											if (testCurrentStage && weapons) {
												logger.warn ("QuestManager.cs :: This stage contains a 'Test' and a 'Weapon' card. This submission is not eligible");
												return;
											}

											logger.info ("QuestManager.cs :: Adding the current stage to the quest");
											info.listOfStages.Insert (i, cards);

										}
										//
										bpps = new int[info.stages.Length];

										for (int y = 0; y < info.stages.Length; y++) {
											bpps [y] = 0;
										}
										// //
										foreach (List<AdventureCard> k in info.listOfStages) {
											foreach (AdventureCard h in k) {
												//	logger.info ("SubmitCards.cs :: Calculating battle points for the current stage");
												if (h.getType () == "Test") {
													bpps [info.listOfStages.IndexOf (k)] = 0;
													//		logger.info ("SubmitCards.cs :: This stage has a 'Test' and requires no battle points");
												} else {
													if (h.getType () == "Weapon") {
														bpps [info.listOfStages.IndexOf (k)] += h.getBattlePoints ();

														} else if (h.getType () == "Foe") {
															if (questCard.getBonusFoe () == h.getName () || questCard.getBonusFoe () == "All") {
																//			logger.info ("SubmitCards.cs :: the 'Foe' name and bonus foe of the 'Quest' match. Using the higher of the two battle points");
																bpps [info.listOfStages.IndexOf (k)] += h.getBonusBattlePoints ();
															} else {
																bpps [info.listOfStages.IndexOf (k)] += h.getBattlePoints ();
																//			logger.info ("SubmitCards.cs :: the 'Foe' '"+ h.getName() +"' does not match the bonus foe of the 'Quest'. Using the lower of the two battle points");
															}
														}
													}
												}
												Debug.Log ("bpps = " + bpps [info.listOfStages.IndexOf (k)]);
											}

												for (int t = 1; t < info.stages.Length + 1; t++) {

													if (bpps [t - 1] != 0) {
														if (t < bpps.Length) {
															if (bpps [t - 1] > bpps [t]) {
																return;
															}
														}
													}
												}
												Debug.Log ("adding stages");


												GameObject[] stagesToDestroy = GameObject.FindGameObjectsWithTag("Stage");
												foreach(GameObject g in stagesToDestroy){
													Destroy(g);
												}

										}
						//info.bpps = infos.GetComponent<QuestInfo>().bpps;
						// info.bpps = GameObject.Find("PlayerObject(Clone)" + info.sponsor).GetComponent<QuestManager>().bpps;

						// for (int i = 0; i < bpps.Length; i++){
						// 	info.bpps[i] = bpps[i];
						// }

					}
					bool sameName(string name, List<AdventureCard> cards){
						for(int i = 0; i < cards.Count; i++){
							if(cards[i].getName() == name){
								return true;
							}
						}
						return false;
					}


					int FindNumberOfStages(string questCard){
						foreach(KeyValuePair<string, int> q in questCards){
							if(q.Key == questCard){
								return q.Value;
							}
						}
						return 0;
					}
				}
