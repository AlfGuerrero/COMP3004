using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour {
	public StoryDeck storyDeck;
	public GameObject storyCard;
	public GameObject player;
	// Use this for initialization
	void Start () {
//		player = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
	}

	public void controlPlayerTurn(){

	}
		
	public void PickUpStoryCard(){
		if (!isLocalPlayer) {
			return;
		}


	
		CmdPickUpStoryCard ();

	}

	[Command]
	public void CmdPickUpStoryCard(){
//
		GameObject card = storyDeck.Draw ();

		card.transform.SetParent (GameObject.Find ("GameCanvas").transform);
		card.transform.localPosition = new Vector3 (-352f, 17f, 0f);
		NetworkServer.Spawn (card);
		Image image = card.GetComponent<Image> ();
		Sprite myImage = image.sprite;
		RpcPickUpStoryCard (card);		

	}
	[ClientRpc]
	public void RpcPickUpStoryCard(GameObject card){
       	
		// want to call the add image function here...! 
		card.transform.SetParent (GameObject.Find ("GameCanvas").transform);
		card.transform.localPosition = new Vector3 (-352f, 17f, 0f);
//		NetworkServer.Spawn (card);

	}

}
