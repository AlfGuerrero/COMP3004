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
	List<List<AdventureCard>> listOfStages = new List<List<AdventureCard>> ();

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
		stage = (GameObject)Resources.Load("PreFabs/QuestStage");
		info = GameObject.Find ("QuestInfo").GetComponent<QuestInfo> ();
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
		info.startSponsor = true;
	}


	public void SponsorQuest(){
		if (!isLocalPlayer) {return;}

		if (info.startSponsor) {
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
		info.stages = GameObject.FindGameObjectsWithTag("Stage");

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

		if (info.overallQuestStarted == true){
			listOfStages = new List<List<AdventureCard>> ();
			Debug.Log("Submitting Stages..");
		}
		if (isServer) {RpcSponsorSubmitCards();}
		else					{CmdSponsorSubmitCards();}
	}
	[Command]
	public void CmdSponsorSubmitCards(){
		RpcSponsorSubmitCards ();
	}

	[ClientRpc]
	public void RpcSponsorSubmitCards(){

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
