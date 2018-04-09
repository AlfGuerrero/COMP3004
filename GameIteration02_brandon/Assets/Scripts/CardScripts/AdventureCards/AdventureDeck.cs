using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

	//public Dictionary<string, int> adventureDeck = new Dictionary<string, int>(){};
	public List<string> adventureDeck = new List<string>();
	// QuestGame.Logger logger = new QuestGame.Logger ();
	//public string TempCard = "";
	public string tempKey = "";
	public int deckSize;
	public int index;
	public int randInt;
	// public GameObject adventureCardPrefab = Resources.Load ("PreFab/AdvCardPrefab") as GameObject;
	// public GameObject adventureCardPrefab;
	// List<string> foes = new List<string>(){"Thieves","Boar","Saxons","Robber Knight","Saxon Knight","Evil Knight","Black Knight","Green Knight","Giant","Dragon","Mordred"};
	// List<string> weapons = new List<string>(){"Dagger","Sword","Horse","Battle-ax","Lance","Excalibur"};
	// List<string> allies = new List<string>(){"Queen Iseult","Sir Galahad","King Arthur","Sir Lancelot","Sir Tristan","King Pellinore","Sir Percival","Sir Gawain","Queen Guinevere","Merlin","Armour"};
	// List<string> tests = new List<string>(){"Test of Valor","Test of the Questing Beast","Test of Morgan Le Fey","Test of Temptation"};



	//Weapons, Foes, Tests
	//public Sprite[] cards;
	// Use this for initialization

	void Start () {
		// populateDeck();
	}

	void Update(){

	}


	public void populateDeck(){
		List<string> RList1 = new List<string>(){"Sword","Saxon Knight","Battle-ax","Dagger","Dragon","Sir Lancelot","Lance","Boar","Horse","Test of Valor", "Boar","Mordred","Black Knight","Horse","Amour","Sword","Robber Knight","Battle-ax","Thieves"};
		List<string> RList2 = new List<string>(){"Horse","Boar","Sword","Saxon Knight","Horse","Dagger","King Pellinore", "Dagger","Sir Percival","Giant","Amour","Dagger","Robber Knight","Sword","Evil Knight","Thieves","Sword"};
		List<string> RList3 = new List<string>(){"Queen Iseult","Saxon Knight","Sword","Merlin","Giant","Thieves","Mordred","Horse","Saxons","Amour","Dagger","Lance","Robber Knight","Battle-ax","Evil Knight","Thieves","Sword"};
		List<string> RList4 = new List<string>(){"Battle-ax","Battle-ax","Queen Guinevere","Sword","Saxon Knight","Test of Temptation","Green Knight","Saxons","Horse","Amour","Amour","Lance","Robber Knight","Battle-ax","Sword"};
		List<string> RList5 = new List<string>(){"Mordred","King Arthur","Lance","Saxons","Test of Temptation","Sword","Saxon Knight","Green Knight","Mordred","Dagger","Evil Knight","Sword","Evil Knight","Thieves","Sword"};
		List<string> RList6 = new List<string>(){"Sir Gawain","Excalibur","Amour","Horse","Test of Morgan Le Fey","Black Knight","Sword","Saxon Knight","Battle-ax","Horse","Saxons","Robber Knight","Thieves"};
		List<string> RList7 = new List<string>(){"Sir Galahad","Horse","Excalibur","Boar","Test of Morgan Le Fey","Robber Knight","Horse","Amour","Sword","Saxon Knight","Saxons","Sword","Battle-ax","Evil Knight","Thieves"};
		List<string> RList8 = new List<string>(){"Sir Tristan","Test of Valor",	"Lance", "Evil Knight","Thieves","Test of the Questing Beast","Test of the Questing Beast","Sword","Saxon Knight","Lance","Black Knight","Horse","Amour","Robber Knight"};

		// logger.info ("AdventureDeck.cs :: Populating Deck with cards.");

		int ranStart = 0;
		while(RList1.Count!=0&&RList2.Count!=0&&RList3.Count!=0&&RList4.Count!=0&&RList5.Count!=0&&RList6.Count!=0&&RList7.Count!=0&&RList8.Count!=0){

			ranStart = Random.Range (1,8);

			if (ranStart == 1) {
			if (RList1.Count >= 1) {
				int Ran = Random.Range (0, RList1.Count);
//					Debug.Log (RList1[Ran]);
				adventureDeck.Add (RList1[Ran]);
				RList1.RemoveAt (Ran);
			} else {
					ranStart = 2;
			}
		}

			if (ranStart == 2) {
				if (RList2.Count >= 1) {
					int Ran = Random.Range (0, RList2.Count);
//					Debug.Log (RList2[Ran]);
					adventureDeck.Add (RList2[Ran]);
					RList2.RemoveAt (Ran);
				} else {
					ranStart = 3;
				}
			}  if (ranStart == 3) {
				if (RList3.Count >= 1) {
					int Ran = Random.Range (0, RList3.Count);
					adventureDeck.Add (RList3[Ran]);
					RList3.RemoveAt (Ran);
				} else {
					ranStart = 4;
				}
			}  if (ranStart == 4) {
				if (RList4.Count >= 1) {
					int Ran = Random.Range (0, RList4.Count);
					adventureDeck.Add (RList4[Ran]);
					RList4.RemoveAt (Ran);
				} else {
					ranStart = 5;
				}
			}  if (ranStart == 5) {
				if (RList1.Count >= 1) {
					int Ran = Random.Range (0, RList5.Count);
					adventureDeck.Add (RList5[Ran]);
					RList5.RemoveAt (Ran);
				} else {
					ranStart = 6;
				}
			}  if (ranStart == 6) {
				if (RList6.Count >= 1) {
					int Ran = Random.Range (0, RList6.Count);
					adventureDeck.Add (RList6[Ran]);
					RList6.RemoveAt (Ran);
			} else {
					ranStart = 7;
			}
			}  if (ranStart == 7) {
				if (RList7.Count >= 1) {
					int Ran = Random.Range (0, RList7.Count);
					adventureDeck.Add (RList7[Ran]);
					RList7.RemoveAt (Ran);
				} else {
					ranStart = 8;
				}
			} if (ranStart == 8) {
				if (RList8.Count >= 1) {
					int Ran = Random.Range (0, RList8.Count);
					adventureDeck.Add (RList8[Ran]);
					RList8.RemoveAt (Ran);
				} else {
					break;
			}
			}
	}
		// logger.test ("AdventureDeck.cs :: Adventure advDeck has been created. with sizes of " + getSizeOfDeck());
	}
	public string NewCard(){
		if(adventureDeck.Count==0){ populateDeck (); }
		tempKey = adventureDeck[0];
		adventureDeck.RemoveAt (0);
		return tempKey;
	}
	public GameObject Draw(string name){
		GameObject tempCard = Instantiate (Resources.Load ("Prefabs/AdvCardPrefab") as GameObject);
		tempCard.AddComponent<AdventureCard> ();
		tempCard.GetComponent<AdventureCard> ().setCard (name);
		return tempCard;
	}



// 	public GameObject Draw(){
// 		//8 lists
// 			//adventureDeck[0]
// 		if(adventureDeck.Count==0){
// 			// logger.info ("AdventureDeck.cs :: ." + adventureDeck.Count + "Is empty.");
// 			populateDeck ();
// 		}
// 		tempKey = adventureDeck[0];
// 		adventureDeck.RemoveAt (0);
// 		GameObject tempCard = Instantiate (Resources.Load ("Prefabs/AdvCardPrefab") as GameObject);   //DO NOT FORGET TO PARENT TO COORECT HAND, MIGHT NEED TO TAKE IN HAND OBJECTS
// 		tempCard.AddComponent<AdventureCard> ();
// 		tempCard.GetComponent<AdventureCard> ().setCard (tempKey);
// //		RemoveCard (tempKey);
// 		// logger.info ("AdventureDeck.cs :: Adventure Deck has drawn Card: " + tempKey);
//
// 		return tempCard;
//
// 		/*
// 		int index = 0;
// 		int randInt = 0;
//
// 		string tempKey = "";
// 		foreach(KeyValuePair<string, int> item in adventureDeck){
// 			randInt = Random.Range (0, getSizeOfDeck ());
//
// 			if (randInt >= getSizeOfDeck()/2) {
// 				randInt = Random.Range (0, getSizeOfDeck ());
// 				if(randInt>= (getSizeOfDeck()/2)){
// 					randInt = Random.Range (0, getSizeOfDeck ());
// 				}else if(randInt<=getSizeOfDeck()/2){
// 					randInt = Random.Range (0, getSizeOfDeck ());
// 				}
//
// 			} else if (randInt <= getSizeOfDeck() / 2) {
// 				randInt = Random.Range (0, getSizeOfDeck ());
// 				if(randInt>=getSizeOfDeck()/2){
// 					randInt = Random.Range (0, getSizeOfDeck ());
// 				}else if(randInt<=getSizeOfDeck()/2){
// 					randInt = Random.Range (0, getSizeOfDeck ());
// 				}
// 			}
//
// 			if(index == randInt){
//
// 				tempKey = item.Key;
// 				GameObject tempCard = Instantiate (adventureCardPrefab);   //DO NOT FORGET TO PARENT TO COORECT HAND, MIGHT NEED TO TAKE IN HAND OBJECTS
// 				tempCard.AddComponent<AdventureCard> ();
// 				tempCard.GetComponent<AdventureCard> ().setCard (tempKey);
// 				RemoveCard (tempKey);
// 				return tempCard;
// 			}
// 			index += 1;
// 		}
//
// 		return Draw ();
// 			*/
// 	}
	/*
	public void RemoveCard(string tempKey){
		if (adventureDeck.Contains (tempKey) == true) {
		//	adventureDeck [tempKey] -= 1;
		//	if(adventureDeck [tempKey] == 0){
		//		adventureDeck.Remove (tempKey);
		//	}
		}
		deckSize = getSizeOfDeck ();
		if(deckSize == 0){
			Debug.Log ("Reshuffling");
			populateDeck ();
		}
	}
*/
	public int getSizeOfDeck(){
		return adventureDeck.Count;
	}


}
