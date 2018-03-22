using System.Collections;
using System.Collections.Generic;

using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : NetworkBehaviour{
	
	GameObject playerCanvas;
	GameObject handCanvas;
	GameObject storyCard;
	public override void OnStartLocalPlayer(){
	}

	// Use this for initialization
	void Start () {
		 playerCanvas = this.gameObject.transform.GetChild (0).gameObject;
		 handCanvas = playerCanvas.gameObject.transform.GetChild (0).gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
	}


	public void UpdateTurn(){
		if (!isLocalPlayer) {
			return;
		}
//		GameObject playerCanvas = this.gameObject.transform.GetChild (0).gameObject;
//		GameObject handCanvas = playerCanvas.gameObject.transform.GetChild (0).gameObject;
		CmdUpdateTurn (this.gameObject);
		handCanvas.gameObject.SetActive (false);
	}
	[Command]
	public void CmdUpdateTurn(GameObject gObject){
		RpcUpdateTurn (gObject);
	}
	[ClientRpc]
	void RpcUpdateTurn(GameObject gObject){
//		GameObject playerCanvas = this.gameObject.transform.GetChild (0).gameObject;
//		GameObject handCanvas = playerCanvas.gameObject.transform.GetChild (0).gameObject;
		handCanvas.gameObject.SetActive (false);
	}


	public void UpdatePosition(){
		if (!isLocalPlayer) {
			return;
		}
		// Go through all cards and get their position.
		for (int cardIndex = 0; cardIndex < handCanvas.transform.childCount; cardIndex++) {
			Vector3 cardPosition = handCanvas.gameObject.transform.GetChild (cardIndex).transform.position;
			CmdUpdatePosition(this.gameObject, cardPosition, cardIndex);
		}
	}
	[Command] 
	public void CmdUpdatePosition(GameObject gObject, Vector3 cardLocation, int cardIndex){
		RpcUpdatePosition (gObject, cardLocation, cardIndex);
	}
	[ClientRpc]
	void RpcUpdatePosition(GameObject gObject, Vector3 cardPosition, int cardIndex){
		GameObject cardObject = handCanvas.gameObject.transform.GetChild (cardIndex).gameObject;
		cardObject.transform.position = cardPosition;
	}
		



//	[Command]
//	public void CmdPickUpStoryCard(GameObject storyCard){
//		RpcPickUpStoryCard (storyCard);
//	}
//	[ClientRpc]
//	public void RpcPickUpStoryCard(GameObject storyCard){
//		if (storyCard != null) {
//			
//			storyCard.transform.SetParent (GameObject.Find ("GameCanvas").transform);
//			storyCard.transform.localPosition = new Vector3 (-352f, 17f, 0f);
//		}
//	}


}
