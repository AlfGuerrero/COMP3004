using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentHolder : MonoBehaviour {
	public List<int> tournamentParticipants = new List<int>();
	public List<int> tournamentParticipantsLeft = new List<int>();
	public List<int> tournamentBattlePoints = new List<int>();
	public List<int> highestAmount= new List<int>();

	public bool tournamentInProgress;
	public int shieldReward;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}


}
