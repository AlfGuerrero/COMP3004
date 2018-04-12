using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo : MonoBehaviour {
	//state info
	public bool startSponsor = false;
	public bool overallQuestStarted = false;
	public bool questSubmit = false;
	public bool participantsChosen = false;


	//sponsor info
	public int sponsor = 0;

	//stage info
	public GameObject[] stages;

	//participant info
	public List<int> participants = new List<int>();
	public List<int> nonParticipants = new List<int>();






	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
