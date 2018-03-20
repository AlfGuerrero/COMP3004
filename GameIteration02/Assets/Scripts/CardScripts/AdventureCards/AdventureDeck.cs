using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureDeck : MonoBehaviour {
	/* ADVENTURE DECK 					
	* Weapons 						= 49 Total
	* Excalibur						= 2
	* Lance							= 6
	* Battle-ax						= 8
	* Sword							= 16
	* Horse							= 11
	* Dagger						= 6
	* 
	* FOES 							= 50 Total
	* Dragon						= 1
	* Giant							= 2
	* Mordred						= 4
	* Green Knight					= 2
	* Black Knight					= 3
	* Evil Knight					= 6
	* Saxon Knight					= 8
	* Robber Knight					= 7 
	* Saxons						= 5
	* Boar							= 4
	* Thieves						= 8
	* 
	* TESTS 							= 8 Total
	* Test of Valor						= 2
	* Test of Temptation				= 2
	* Test of Morgan Le Fey				= 2
	* Test of the Questing Beast		= 2
	* 
	* ALLIES							= 10 Total
	* Sir Galahad						= 1
	* Sir Lancelot						= 1
	* King Arthur 						= 1
	* Sir Tristan						= 1
	* Sir Pellinore						= 1
	* Sir Gawain 						= 1
	* Sir Percival						= 1
	* Queen Quinevere					= 1
	* Queen Iseult						= 1
	* Merlin							= 1
	* 
	* AMOURS 							= 8 Total
	*/

	//public Text AdventureDCardText;
	public Dictionary<string, int> adventureDeck = new Dictionary<string, int>(){};
	public List<string> adventureDeck2 = new List<string> ();

	//public string TempCard = "";
	public string tempKey = "";
	public int deckSize;
	public int index;
	public int randInt;
	public GameObject adventureCardPrefab = Resources.Load ("Prefabs/AdvCardPrefab") as GameObject;

	//Weapons, Foes, Tests
	//public Sprite[] cards;
	// Use this for initialization

	void Start () {
		
	}

	void Update(){
	}

	public void populateDeck(){
		adventureDeck.Add("Dagger", 6);
		adventureDeck.Add("Sword", 16);
		adventureDeck.Add("Battle-ax", 8);
		adventureDeck.Add("Excalibur", 2);
		adventureDeck.Add("Horse", 11);
		adventureDeck.Add("Lance", 6);

		adventureDeck.Add("Thieves", 8);
		adventureDeck.Add("Boar", 4);
		adventureDeck.Add("Saxons", 5);
		adventureDeck.Add("Robber Knight", 7);
		adventureDeck.Add("Saxon Knight", 8);
		adventureDeck.Add("Evil Knight", 6);
		adventureDeck.Add("Black Knight", 3);
		adventureDeck.Add("Green Knight", 2);
		adventureDeck.Add("Giant", 2);
		adventureDeck.Add("Dragon", 1);
		adventureDeck.Add("Mordred", 4);

		adventureDeck.Add("Test of Valor", 2);
		adventureDeck.Add("Test of the Questing Beast", 2);
		adventureDeck.Add("Test of Morgan Le Fey", 2);
		adventureDeck.Add("Test of Temptation", 2);

		adventureDeck.Add("Queen Iseult", 1);
		adventureDeck.Add("Sir Galahad", 1);
		adventureDeck.Add("King Arthur", 1);
		adventureDeck.Add("Sir Lancelot", 1);
		adventureDeck.Add("Sir Tristan", 1);
		adventureDeck.Add("King Pellinore", 1);
		adventureDeck.Add("Sir Percival", 1);
		adventureDeck.Add("Sir Gawain", 1);
		adventureDeck.Add("Queen Guinevere", 1);
		adventureDeck.Add("Merlin", 1);

		adventureDeck.Add ("Amour", 8);
	}

	public GameObject Draw(){
		int index = 0;
		int randInt = 0;

		string tempKey = "";
		foreach(KeyValuePair<string, int> item in adventureDeck){
			randInt = Random.Range (0, getSizeOfDeck ());
			if(index == randInt){
				tempKey = item.Key;
				GameObject tempCard = Instantiate (adventureCardPrefab);   /**DO NOT FORGET TO PARENT TO COORECT HAND, MIGHT NEED TO TAKE IN HAND OBJECTS**/
				tempCard.AddComponent<AdventureCard> ();
				tempCard.GetComponent<AdventureCard> ().setCard (tempKey);
				RemoveCard (tempKey);
				return tempCard;
			}
			index += 1;
		}

		return Draw ();
	}

	public void RemoveCard(string tempKey){
		if (adventureDeck.ContainsKey (tempKey) == true) {
			adventureDeck [tempKey] -= 1;
			if(adventureDeck [tempKey] == 0){
				adventureDeck.Remove (tempKey);
			}
		}
		deckSize = getSizeOfDeck ();
		if(deckSize == 0){
			Debug.Log ("Reshuffling");
			populateDeck ();
		}
	}

	public int getSizeOfDeck(){
		return adventureDeck.Keys.Count;
	}

	public void populateDeck(List<string> thingsToAdd){
		adventureDeck2 = thingsToAdd;
	}

	public GameObject Draw2(string cardToMake){

		GameObject tempCard = Instantiate (adventureCardPrefab);   /**DO NOT FORGET TO PARENT TO COORECT HAND, MIGHT NEED TO TAKE IN HAND OBJECTS**/
		tempCard.AddComponent<AdventureCard> ();
		tempCard.GetComponent<AdventureCard> ().setCard (cardToMake);
		RemoveCard (cardToMake);
		return tempCard;
	}

}
