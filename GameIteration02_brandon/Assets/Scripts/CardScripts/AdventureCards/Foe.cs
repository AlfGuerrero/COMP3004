using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Foe : MonoBehaviour, Card {
	protected new string name;
	protected int battlePoints;
	protected int bonusBattlePoints;
	protected bool mordred;
	protected string type;
	protected int value;
	protected FoeScriptObj foe;
	protected string card;
	// Use this for initialization
	void Start(){
		foe = Resources.Load<FoeScriptObj> ("Foe/"+card);
		name = foe.name;
		type = "foe";
		GetComponent<Image> ().sprite = foe.image;
		battlePoints = foe.battlePoints;
		bonusBattlePoints = foe.bonusBattlePoints;
		mordred = foe.mordred;
		value = foe.value;

		//Debug.Log (name + " " + type + " " + battlePoints);
	}
	public string getName(){
		return this.name;
	}
	public string getType(){
		return this.type;
	}
	public int getValue(){
		return this.value;
	}
	public int getBattlePoints(){
		return this.battlePoints;
	}
	public int getBonusBattlePoints(){
		return this.bonusBattlePoints;
	}
	public void setCard (string cardName){
		card = cardName;
	}
	public int getBidPoints(){
		return 0;
	}
	public int getBonusBidPoints(){
		return 0;
	}
	public int getbonusBidRequirements(){
		return 0;
	}
	public int getbidRequirements(){
		return 0;
	}
}
