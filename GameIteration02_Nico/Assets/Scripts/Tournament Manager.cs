using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentManager : MonoBehaviour {
	public int shieldNumbe=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void tourniSetUp(){
		Joining(); submitCards ();
		CheckHighestBattlePoints ();
	}

	public void Joining(){
	//Sends a request to all players to see if they will join.
		//Add the number of plays to the number of total shields for the tourni.
		//Remove the buttons for the players who are not being part of the tourni.
	}
	public void submitCards(){
	//Asking all the players to submit their cards for the tourni
	}
	public void CheckHighestBattlePoints(){
	//check the players totals and then send back the winning player, and add their newly gained shields to them.
	}

}
