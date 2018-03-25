using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour {
	public StoryDeck 	storyDeck;
	public GameObject 	storyCard;
	GameObject[] 		storyCardDelete; 


	void Start(){
//		CmdAddPlayer (playerSize);
		Debug.Log ("Player: " + netId.Value + " has joined.");
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
	}
	
	public void ControlPlayerTurn(){
		if (!isLocalPlayer) {return;}
		string playerTurnText = GameObject.FindGameObjectWithTag("PlayerTurnTextUI").GetComponent<Text>().text;
		int playerTurnInt;
		int.TryParse (playerTurnText, out playerTurnInt);
		playerTurnInt++;

		GameObject.FindGameObjectWithTag ("PlayerTurnTextUI").GetComponent<Text> ().text = playerTurnInt.ToString();
//		Debug.Log ("LOCAL - Player: " + netId.Value + " " + playerTurnInt);
		CmdControlPlayerTurn (this.gameObject, playerTurnInt.ToString());
	}
	[Command]
	public void CmdControlPlayerTurn(GameObject gObject, string playerTurn){
//		string playerTurnText = GameObject.FindGameObjectWithTag("PlayerTurnTextUI").GetComponent<Text>().text;
//		int.TryParse (playerTurnText, out playerTurnInt);
//		GameObject.FindGameObjectWithTag ("PlayerTurnTextUI").GetComponent<Text> ().text = playerTurnInt.ToString();
//		Debug.Log ("CMD - Player: " + netId.Value + " " + playerTurnInt);
		RpcControlPlayerTurn (this.gameObject, playerTurn);
	}
	[ClientRpc]
	public void RpcControlPlayerTurn(GameObject gObject, string playerTurn){
//		int.TryParse (playerTurnText, out playerTurnInt);
		GameObject.FindGameObjectWithTag ("PlayerTurnTextUI").GetComponent<Text> ().text = playerTurn;
		Debug.Log ("RPC - Player: " + netId.Value + " " + playerTurn);
	}


	public void PickUpStoryCard(){ // Local Button...
		if (!isLocalPlayer) {return;}
		storyCardDelete = GameObject.FindGameObjectsWithTag("StoryCard");
		foreach (GameObject i in storyCardDelete){
			DestroyObject (i);
		}
		string nameOfCard = storyDeck.NewCard ();
		GameObject.FindGameObjectWithTag("StoryCardTextUI").GetComponent<Text>().text = nameOfCard;
		storyCard = storyDeck.Draw(nameOfCard);		
		storyCard.transform.SetParent (GameObject.Find ("GameCanvas").transform);
		storyCard.transform.localPosition = new Vector3 (-352f, 17f, 0f);
		CmdPickUpStoryCard(this.gameObject, nameOfCard);
	}
	[Command] // Server calls Clients...
	public void CmdPickUpStoryCard(GameObject gObject, string name){
		storyCardDelete = GameObject.FindGameObjectsWithTag("StoryCard");
		foreach (GameObject i in storyCardDelete){
			DestroyObject (i);
		}
		RpcPickUpStoryCard(this.gameObject, name);
	}
	[ClientRpc] // Clients call Server... 
	public void RpcPickUpStoryCard(GameObject gObject, string name){
		storyCardDelete = GameObject.FindGameObjectsWithTag("StoryCard");
		foreach (GameObject i in storyCardDelete){
			DestroyObject (i);
		}
		storyCard = storyDeck.Draw(name);
		storyCard.transform.SetParent (GameObject.Find ("GameCanvas").transform);
		storyCard.transform.localPosition = new Vector3 (-352f, 17f, 0f);

		GameObject.FindGameObjectWithTag("StoryCardTextUI").GetComponent<Text>().text = name;


	}


}
