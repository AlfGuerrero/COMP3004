using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventureCard : MonoBehaviour {
	protected new string name;
	protected string type;
	protected int battlePoints;
	protected int bonusBattlePoints;
	protected int bidPoints;
	protected int bonusBidPoints;
	protected int value;
	protected bool merlin = false;
	protected bool mordred = false;
	protected string card;

	public ACScriptObj adventureCard;

	void Start(){

		adventureCard = Resources.Load<ACScriptObj> ("AdventureCards/"+card);
		name = adventureCard.name; 
		battlePoints = adventureCard.battlePoints;
		bonusBattlePoints = adventureCard.bonusBattlePoints;
		bidPoints = adventureCard.bidPoints;
		bonusBidPoints = adventureCard.bonusBidPoints;
		merlin = adventureCard.merlin;
		mordred = adventureCard.mordred;
		value = adventureCard.value;
		type = adventureCard.type;
		GetComponent<Image> ().sprite = adventureCard.image;
	}


	public string getName(){
		return this.name;
	}
	public string getType(){
		return this.type;
	}
	public int getBattlePoints(){
		return this.battlePoints;
	}
	public int getBidPoints(){
		return this.bidPoints;
	}
	public int getBonusBattlePoints(){
		return this.bonusBattlePoints;
	}
	public int getValue(){
		return this.value;
	}
	public int getBonusBidPoints(){
		return this.bonusBidPoints;
	}
	public void setCard (string cardName){
		card = cardName;
	}
}
