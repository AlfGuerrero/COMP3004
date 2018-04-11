using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour, Card {
	//protected static readonly string[] WEAPON_NAME = {"Horse", "Sword", "Dagger", "Excalibur", "Lance", "Battle-ax"};
	protected new string name;
	protected int battlePoints;
	protected string type;
	protected WeaponScriptObj weapon;
	protected string card;
	protected int value;
	void Start(){
		weapon = Resources.Load<WeaponScriptObj> ("Weapon/"+card);
		name = weapon.name;
		type = "weapon";
		value = weapon.value;
		battlePoints = weapon.battlePoints;

		GetComponent<Image> ().sprite = weapon.image;
	}

	public string getName(){
		return this.name;
	}
	public int getValue(){
		return this.value;
	}
	public int getBattlePoints(){
		return this.battlePoints;
	}
	public void setCard (string cardName){
		card = cardName;
	}
	public int getBidPoints(){
		return 0;
	}
	public int getBonusBattlePoints(){
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
	public string getType (){
		return null;
	}
}
