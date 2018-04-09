using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ally : MonoBehaviour, Card {
	//protected static readonly string[] ALLY_NAME = {"Sir Gawain", "King Pellinore", "Sir Percival", "Sir Tristan", "King Arthur", "Queen Guinevere", "Merlin", "Queen Iseult", "Sir Lancelot", "Sir Galahad", "Armour"};
	protected new string name;
	protected string type;
	protected int battlePoints;
	protected int bonusBattlePoints;
	protected int bidPoints;
	protected int value;
	protected int bonusBidPoints;
	protected bool merlin = false;
	protected string card;

	public AllyScriptObj ally;

	void Start(){
		
		ally = Resources.Load<AllyScriptObj> ("Ally/"+card);
		name = ally.name;
		battlePoints = ally.battlePoints;
		bonusBattlePoints = ally.bonusBattlePoints;
		bidPoints = ally.bidPoints;
		bonusBidPoints = ally.bonusBidPoints;
		merlin = ally.merlin;
		value = ally.value;
		type = "ally";
		GetComponent<Image> ().sprite = ally.image;
		//Debug.Log (name + " " + type + " " + battlePoints);
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
	public int getbonusBidRequirements(){
		return 0;
	}
	public int getbidRequirements(){
		return 0;
	}
}
