using System.Collections;
using System.Collections.Generic;

using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : NetworkBehaviour{

	public override void OnStartLocalPlayer(){
//		GetComponent<Renderer> ().material.color = new Color (255.0f, 255.0f, 0.0f);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
	
	}
	/*
	Use draggable script to move cards...
	Last call Command to update its positioning
	Then call ClientRpc to update it on everyone elses positioning.
	*/

	public void UpdatePosition(){
		if (!isLocalPlayer) {
			return;
		}

		GameObject playerCanvas = this.gameObject.transform.GetChild (0).gameObject;
		GameObject handCanvas = playerCanvas.gameObject.transform.GetChild (0).gameObject;

		for (int cardIndex = 0; cardIndex < handCanvas.transform.childCount; cardIndex++) {
			Vector3 cardPosition = handCanvas.gameObject.transform.GetChild (cardIndex).transform.position;
			CmdUpdatePosition(this.gameObject, cardPosition, cardIndex);
		}

//		Vector3 cardLocation = PlayerCanvas.gameObject.transform.GetChild (1).transform.position;


//		this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
	}

	public void UpdateTurn(){
		if (!isLocalPlayer) {
			return;
		}
		GameObject playerCanvas = this.gameObject.transform.GetChild (0).gameObject;
		GameObject handCanvas = playerCanvas.gameObject.transform.GetChild (0).gameObject;
		CmdUpdateTurn (this.gameObject);
		handCanvas.gameObject.SetActive (false);

	}

	[Command]
	public void CmdUpdateTurn(GameObject gObject){
		RpcUpdateTurn (gObject);
	}
	[ClientRpc]
	void RpcUpdateTurn(GameObject gObject){
		GameObject playerCanvas = this.gameObject.transform.GetChild (0).gameObject;
		GameObject handCanvas = playerCanvas.gameObject.transform.GetChild (0).gameObject;
		handCanvas.gameObject.SetActive (false);
	}
		

	[Command] 
	public void CmdUpdatePosition(GameObject gObject, Vector3 cardLocation, int cardIndex){
		RpcUpdatePosition (gObject, cardLocation, cardIndex);
	}
	[ClientRpc]
	void RpcUpdatePosition(GameObject gObject, Vector3 cardPosition, int cardIndex){
		
//		gObject.GetComponent<Renderer> ().material.color = color;
//		this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
//		GameObject canvas = this.gameObject.transform.GetChild (0).gameObject;


		GameObject playerCanvas = this.gameObject.transform.GetChild (0).gameObject;
		GameObject handCanvas = playerCanvas.gameObject.transform.GetChild (0).gameObject;
		GameObject cardObject = handCanvas.gameObject.transform.GetChild (cardIndex).gameObject;

		cardObject.transform.position = cardPosition;
	}


}
