using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Info : MonoBehaviour {
	public bool questInProgress = false;
	public bool sponsorRound = false;
	public bool participateRound = false;
	public bool startParticipantQuest = false;
	public bool endPlayerSubmit = false;

	public List<List<AdventureCard>> listOfStages = new List<List<AdventureCard>> ();
	public List<AdventureCard> currentStage = new List<AdventureCard> ();
	public int[] battlePointsPerStage;

	public string currentQuestCard = "";
	public int numStages = 0;
	public GameObject[] stages = null;
	public List<int> participants = new List<int>();
	public int participantPasses = 0;
	public int sponsor = 0;
	public int sponsorPasses = 0;
	public int trySponsor = 1;
	public int submitsForStage = 0;
	public int currentStageInt = 0;
	public int tempNumParticipants = 0;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}


	public void SetStages(GameObject[] stages){
		this.stages = stages;
	}

	public void SetStages(List<List<AdventureCard>> list){
		listOfStages = list;
	}

	public void ResetQuestValues(int CurrentPlayerTurn){
		questInProgress = false;
		sponsorRound = true;
		participateRound = false;

		currentQuestCard = "";
		numStages = 0;
		stages = new GameObject[0];
		listOfStages = new List<List<AdventureCard>> ();
		participants = new List<int>();
		participantPasses = 0;
		sponsor = 0;
		sponsorPasses = 0;
		trySponsor = CurrentPlayerTurn;
	}
}
