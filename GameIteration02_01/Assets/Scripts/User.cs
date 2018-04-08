using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class User : NetworkBehaviour {
	public int shields;
	public int baseAttack;
	public string rank;
	public string username;

	protected Sprite Squire;
	protected Sprite Knight;
	protected Sprite ChampionKnight;



	// Use this for initialization
	void Start () {
		this.username 	= "player_" + netId.Value;
		this.rank 			= "Squire";
		this.baseAttack	= 0;
		this.shields		= 3;
		Debug.Log ("username : " + username);

	}

	// Update is called once per frame
	void Update () {

	}
}
