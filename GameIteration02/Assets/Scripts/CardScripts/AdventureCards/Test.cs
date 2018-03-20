using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour, Card {
	//protected static readonly string[] TEST_NAME = {"Test of the Questing Beast", "Test of the Temptation", "Test of the Valor", "Test of the Morgen Le Fey"};

	protected new string name;
	protected int bidRequirements;
	protected int bonusBidRequirements;
	protected string type;
	protected string card;
	protected int value;
	public TestScriptObj test;


	void Start(){
		test = Resources.Load<TestScriptObj> ("Test/"+card);
		name = test.name;
		type = "test";
		bidRequirements = test.bidRequirements;
		value = test.value;

		GetComponent<Image> ().sprite = test.image;
	}

	public string getName(){
		return this.name;
	}
	public string getType(){
		return this.type;
	}
	public int getbidRequirements(){
		return this.bidRequirements;
	}
	public int getValue(){
		return this.value;
	}
	public int getbonusBidRequirements(){
		return this.bonusBidRequirements;
	}
	public void setCard (string cardName){
		card = cardName;
	}
	public int getBattlePoints(){
		return 0;
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

}
