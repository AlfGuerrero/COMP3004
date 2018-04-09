using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class User : NetworkBehaviour {
	public int shields;
	public int baseAttack;
	public int totalBP;
	public int AllyBattlePoints;
	public string rank;
	public string username;
	public List<AdventureCard> AlliesInHand;
	public List<AdventureCard> TournmanetCards;
	public int TourniBP;


	protected Sprite Squire;
	protected Sprite Knight;
	protected Sprite ChampionKnight;



	// Use this for initialization
	void Start () {
		this.username 			  = "player_" + netId.Value;
		this.rank 				  = "Squire";
		this.totalBP 		 	  = 0;
		this.baseAttack			  = 0;
		this.AllyBattlePoints	  = 0;
		this.shields			  = 0;
		this.totalBP 			  = 0;
		//Debug.Log ("username : " + username);

	}

	// Update is called once per frame
	void Update () {
		CheckCardInHand();
		UpdateRank();
		setTotalBattlePoints ();
	}

	public void CheckCardInHand(){
		if (GameObject.Find("HandCanvas").transform.childCount > 12) {
			Debug.Log ("Too Many Cards In Hand");
		}
	}



//*********************************************************************//
	//						GET METHODS                         //

	public int numOfcards(){
		GameObject hand = GameObject.Find ("HandCanvas"+netId.Value);
		return hand.transform.childCount;
	}
	public List<GameObject> getCardsInHand(){
		List<GameObject> CIH = new List<GameObject> ();
		GameObject hand = GameObject.Find ("HandCanvas"+netId.Value);
		int HandCount = hand.transform.childCount;
		for(int counter=0;counter<HandCount;counter++){
			CIH.Add(hand.transform.GetChild (counter).gameObject);
		}	
		return CIH;
	} //End of get cards in hand.

	public GameObject getHand(){
		foreach (Transform temp in transform) {
			if (temp.name == "Hand") {
				return temp.gameObject;
				}
		}
		return null;
	}	//End of get Hand

	public List<AdventureCard> getAllies(){
		return AlliesInHand;
	}//End of Get Allies


	public string GetUsername(){
		return username;
	}//End of Get username

	public int getShields(){
		return this.shields;
	}// End of get shields

	public int getBaseAttack(){
		return this.baseAttack;
	}//End of Get Base Attack

	public string getRank(){
		return this.rank;
	}
	public int getTotalBattlePoints(){
		return this.totalBP;
	}
	public void UpdateRank(){

		if (12 > this.shields && this.shields >= 5) {
			//logger.info ("User.cs :: Ranking Up: " + this.username);
			this.rank = "Knight";
			this.baseAttack = 5;
			//logger.info("User.cs :: BaseATTACK:" + this.baseAttack);
			this.gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = Knight;

		} else if (22 > this.shields && this.shields >= 12) {
			//logger.info ("User.cs :: Ranking Up: " + this.username);
			this.gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = ChampionKnight;
			this.baseAttack = 10;
			//logger.info("User.cs :: BaseATTACK:" + this.baseAttack);
			this.rank = "Champion Knight";

		} else if (this.shields >= 22) {
			//	logger.info ("User.cs :: Ranking Up: " + this.username);
			this.rank = "Knight Of the Round Table";
			this.baseAttack = 20;
			//logger.info("User.cs :: BaseATTACK:" + this.baseAttack);
		}

		//this.gameObject.transform.GetChild(4).GetComponent<Text>().text =  ("Rank: " + this.rank);
		

	}//end of updating rank
	
	public int getAllyBattlePoints(){
	/*int returnPoints = 0;
		foreach (AdventureCard CurrentCard int this.AlliesInHand){
			//returnPoints+= CurrentCa
			}
		//logger.info ("User.cs :: getAllyBattlePoints function has been called for Player:  " + this.user_name + " BattlePoints: " + totalBattlePoints);
		//	logger.info ("User.cs :: getAllyBattlePoints function has been called for Player:  " + this.user_name + " New BattlePoints: " + totalBattlePoints);

		return null;//return returnPoints;
*/
		return -1;
	}
	public List<AdventureCard> GetTournmanetCards(){
		return TournmanetCards;
	}public void SetTourni(AdventureCard Temp){
		TournmanetCards.Add(Temp);
	}

	public int getTourniBP(){
		return TourniBP;
	}
	public void setTourniBP(int points){
		TourniBP=points;
	}





	//*****************************************************************************************************//
		//											Set Functions


	public void setName(string GivenUsername){
		this.username=GivenUsername;		
	}//End of setName


	public void setShields(int NewShieldAmount){
		//this.gameObject.transform.GetChild(5).GetComponent<Text>().text =  ("Shields: " + this.shields);
		this.shields = NewShieldAmount;
	}

	public void setTotalBattlePoints(){
		this.totalBP=(getAllyBattlePoints()+baseAttack);
	}//end of Setting total battle points




	//***********************************************************************************************//
			// Remove and Add Allies//
	public void PlayAllies(AdventureCard CurrentAlly){
	AlliesInHand.Add(CurrentAlly);
	//	logger.info ("User.cs :: addAlly function has been called for Player:  " + this.user_name + " Adding Ally: " + Ally.getName());
	//logger.info ("User.cs :: addAlly function has been called for Player:  " + this.user_name + " Ally Battle Points: " + Ally.getBonusBattlePoints());
}//End of playallies
	/*
	public void removeAllies(AdventureCard AllyToBeRemoved){
		foreach(AdventureCard CurrentCard int AlliesInHand){
		//	if(CurrentCard.getName()==ally){
				AlliesInHand.remove(CurrentCard);
			//}
		}
	}//End of removeAllies

*/
}