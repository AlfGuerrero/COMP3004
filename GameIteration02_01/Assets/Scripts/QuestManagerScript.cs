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

	int currentPlayerTurn;

 	//Variables for Initialize Stages
 	public int spacing = 200;

	GameObject stage;
	GameObject submitButton;

 	string currentQuestCard = "";

 	int numStages;


 	GameObject[] stages = null;
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
 	}

 	// Update is called once per frame
 	void Update () {
		int temp = int.Parse(GameObject.Find ("PlayerTurnTextUI").GetComponent<Text> ().text);
		int temp2 = int.Parse(GameObject.Find ("PlayerTurnTextUI").GetComponent<Text> ().text)/4;
		//might be ahead by 1
		currentPlayerTurn = (temp+1) - temp2*4;
		Debug.Log (currentPlayerTurn);

		Debug.Log (i.stages.Length);
	}
	public void ChooseSponsor(int playerTurn){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcChooseSponsor(playerTurn);}
		else					{CmdChooseSponsor(playerTurn);}
	}

	[Command]
	public void CmdChooseSponsor(int playerTurn){
		RpcChooseSponsor (playerTurn);
	}
	[ClientRpc]
	public void RpcChooseSponsor(int playerTurn){
		if(netId.Value == playerTurn){
			//enable sponsor button, enable pass button, disable participate

		}
	}




 	public void InitializeStages(GameObject theSponsor){
 		//spawn stages for sponsor
		if(isLocalPlayer){
	 		currentQuestCard = GameObject.FindGameObjectWithTag("StoryCardTextUI").GetComponent<Text>().text;
	 		numStages = FindNumberOfStages (currentQuestCard);
	 		stages = new GameObject[numStages];
	 		for(int i = 0; i < numStages; i++){
	 			stages[i] = Instantiate (stage, theSponsor.transform);
				stages[i].transform.position = new Vector2((stages [i].transform.position.x + 2*i) /*+ 140*i) - spacing*/, stages[i].transform.position.y);
	 		}
		}

 		//spawn submit button for sponsor
 		//Instantiate(submitButton, theSponsor.transform);

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
		if(sponsor == 0){
			sponsor = (int)netId.Value;
			Debug.Log ("The sponsor is: " + sponsor);
			GameObject[] sponsorbuttons = GameObject.FindGameObjectsWithTag ("sponsorButton");
			ChangeActive (sponsorbuttons, false);
			this.gameObject.transform.GetChild(0).GetChild(5).GetComponent<Button>().interactable = false;
			Debug.Log ("initializing stages");
			InitializeStages (GameObject.Find ("PlayerObject(Clone)" + sponsor.ToString ()).transform.GetChild (0).gameObject);
			Debug.Log ("stages initialized");
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
		i.participants.Add((int)netId.Value);
		Debug.Log ("Player "+ (int)netId.Value +"is participating.");
		GameObject.Find("PlayerObject(Clone)"+netId.Value).transform.GetChild(0).GetChild(5).GetComponent<Button>().interactable = false;
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
 }
