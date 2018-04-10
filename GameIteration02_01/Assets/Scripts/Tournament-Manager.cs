// ﻿using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;
//
// public class TournamentManager : NetworkBehaviour {
//
// 	public int shieldNumber=0;
// 	public List<uint> PlayInTourni;
// 	GameObject Tourni;
// 	GameObject submitButton;
// 	List<GameObject> Temp;
//
// 	// Use this for initialization
//
// 	void Start () {
// 		Tourni = (GameObject)Resources.Load("PreFabs/tourni");
// 		submitButton = (GameObject)Resources.Load("PreFabs/SubmitButton");
// 	}
//
// 	// Update is called once per frame
// 	void Update () {
//
// 	}
// 	public void tourniSetUp (TournamentScriptObj currentTourni){
// 		List<uint> SPOTHOLDING;
// 		 List<uint>cunt,shit;
// 		shieldNumber=currentTourni.bonusShields;
// 		Joining(SPOTHOLDING); submitCards (shit, cunt);
// 		List<uint>Winner=CheckHighestBattlePoints(shit);
// 		if (Winner.Count > 1) {
// 			//call tourni again but with the list of the two players.
// 		} else {
//
// 			GameObject.Find ("PlayerObject(Clone)" + Winner [0]).GetComponent<User> ().setShields (shieldNumber);
// 		}
// 	}//End of tourniSetup
//
//
// 	[ClientRpc]
// 	public void Joining(List<uint> Players){
// 		foreach (int player in Players) {
// 			//If user Clicks participate button add them to the new list.
// 			//clear their tournicards, and tournibattlepoints
// 			//Allow them to click the button as well.
//
//
// 			//GameObject.Find("TabCanvas").transform.getChild(0);
// 			//Getcomponenet<Text>().text=GameObject.Find("PlayerObject(Clone)").GetComponent<User>().GetUserName();
// 			//If player joins set their tournicards to clear as well as increase the number of shields in tourni.
// 		}
// 	//Sends a request to all players to see if they will join.
// 		//Add the number of plays to the number of total shields for the tourni.
// 		//Remove the buttons for the players who are not being part of the tourni.
// 	}
//
// 	[ClientRpc]
// 	public void submitCards(List<uint>PlayInTourni, List<uint>Players){
// 		foreach (uint Play  in Players) {
// 			if (PlayInTourni.Contains (Play)) {
// 				//wait for the submit buttn to be pressed add all cards inside the box to the array
// 				//GameObject.Find("PlayerObject(Clone)"+PlayerId).getComponent<User>().setTournmanetCards;
// 				//TournmaentCards;
// 				int count= GameObject.FindGameObjectWithTag("Tourni").transform.childCount;
// 				for (int i = 0; i < count; i++) {
// 					Temp.Add(GameObject.FindGameObjectWithTag("Tourni").transform.GetChild(i).gameObject);
// 				}
// 				foreach (GameObject temp in Temp) {
// 					GameObject.Find ("PlayerObject(Clone)" + Play).GetComponent<User> ().SetTourni (temp.GetComponent<AdventureCard> ());
// 				}
// 			}else {
// 				return;
//
// 			}
// 	//Asking all the players to submit their cards for the tourni
// 		}
// 	}
// 	public List<uint> CheckHighestBattlePoints(List<uint> PlayersInTourni){
// 	//check the players totals and then send back the winning player, and add their newly gained shields to them.
// 		foreach(uint CurrentPlayer in PlayersInTourni){
// 			List<AdventureCard> Addition = GameObject.Find ("PlayerObject(Clone)" + CurrentPlayer).GetComponent<User> ().GetTournmanetCards();
// 			int Tempcalc = GameObject.Find ("PlayerObject(Clone)" + CurrentPlayer).GetComponent<User> ().getTourniBP ();
// 			foreach (AdventureCard CurrentCard in Addition) {
// 				Tempcalc += CurrentCard.getBattlePoints ();
// 				}
// 		}
// 		List<uint> highestAmount;
// 		highestAmount.Add(0);
// 		foreach (uint CurrentPlayer in PlayersInTourni) {
// 			int Temp=GameObject.Find ("PlayerObject(Clone)" + CurrentPlayer).GetComponent<User> ().getTourniBP();
// 				if (Temp > GameObject.Find ("PlayerObject(Clone)" + highestAmount[0]).GetComponent<User> ().getTourniBP ()){
// 				highestAmount.Clear();
// 				highestAmount.Add(CurrentPlayer);
// 			}
//
// 		}
// 		return highestAmount;
// 	}
//
//
// }
