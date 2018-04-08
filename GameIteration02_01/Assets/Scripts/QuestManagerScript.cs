// ﻿using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class QuestManagerScript : MonoBehaviour {
// 	//Variables for Initialize Stages
// 	public int spacing = 200;
//
// 	GameObject stage = Resources.Load("PreFabs/QuestStage");
// 	GameObject submitButton = Resources.Load("PreFabs/SubmitButton");
//
// 	string currentQuestCard = "";
//
// 	GameObject sponsor = null;
// 	GameObject[] participants = null;
//
// 	int numStages = null;
// 	GameObject[] stages = null;
// 	//---------------------------------\\
//
// 	List<List<AdventureCards>> stagesToPlaythroughWith = null;
//
// 	Dictionary<string, int> questCards = {
// 		{ "Boar Hunt", 2 },
// 		{ "Search for the Holy Grail", 5 },
// 		{ "Test of the Green Knight", 4 },
// 		{ "Search for the Questing Beast", 4 },
// 		{ "Defend the Queen's Honor", 4 },
// 		{ "Rescue the Fair Maiden", 3 },
// 		{ "Journey Through the Enchanted Forest", 3 },
// 		{ "Vanquish King Arthur's Enemies", 3 },
// 		{ "Slay The Dragon", 3 },
// 		{ "Repel the Saxon Raiders", 2 }
// 	};
//
// 	// Use this for initialization
// 	void Start () {
//
// 	}
//
// 	// Update is called once per frame
// 	void Update () {
//
// 	}
//
// 	public void InitializeStages(GameObject theSponsor){
// 		//spawn stages for sponsor
// 		currentQuestCard = GameObject.FindGameObjectWithTag("StoryCardTextUI").GetComponent<Text>().text;
// 		numStages = FindNumberOfStages (currentQuestCard);
// 		stages = new GameObject[numStages];
// 		for(int i = 0; i < numStages; i++){
// 			stages[i] = Instantiate (stage, theSponsor.transform);
// 			stages[i].transform.position = new Vector2((stages [i].transform.position.x + 140*i) - spacing, stages[i].transform.position.y);
// 		}
//
// 		//spawn submit button for sponsor
// 		Instantiate(submitButton, theSponsor.transform);
//
// 		//then wait till submit button is pressed to continue to submissions for participants. i.e do nothing
//
// 	}
//
// 	int FindNumberOfStages(string questCard){
// 		foreach(KeyValuePair<string, int> q in questCards){
// 			if(q.Key == questCard){
// 				return q.Value;
// 			}
// 		}
// 	}
//
// 	public void SetStages(List<List<AdventureCard>> stagesToSet){
// 		stagesToPlaythroughWith = stagesToSet;
// 	}
//
// 	//public void RunThrough
//
//
//
//
//
// }
