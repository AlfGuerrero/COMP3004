using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour {
	/*
	 * Event Cards
	 * 1. King's Recoginition
	 * - This next player(s) to complete a Quest will receive 2 extra shields.
	 * 2. Queen's Favor
	 * - The lowest rank player(s) immediately receives 2 Adventure Cards.
	 * 3. Court Called to Camelot
	 * - All Allies in play must be discarded.
	 * 4. Pox
	 * - All players except the player drawing this card lose 1 shield.
	 * 5. Plague
	 * - Drawer loses 2 shields if possible.
	 * 6. Chivalrous Deed
	 * - Player(s) with both lowest rank and least amount of shields, receives 3 shields.
	 * 7. Prosperity Throughout the Realm
	 * - All players may immediately draw 2 Adventure Cards.
	 * 8. King's Call to Arms
	 * - The Highest Ranked Player(s) must place 1 weapon in the discard pile. If unable to do so 2 Foe Cards must be discarded.
	 *
	 * Shields
	 * Allys
	 * Adventure Cards
	 * Weapon Cards
	 * 2 Foe Cards
	*/

	// - This next player(s) to complete a Quest will receive 2 extra shields.
	public void Kings_Recoginition(uint id){
		Debug.Log("EventsManager:: Kings_Recoginition :: setting shields for " + id);
		User user = GameObject.Find("PlayerObject(Clone)" + id).GetComponent<User>();
		int shields = user.getShields() + 2;
		user.setShields(shields);
	}

	public void Queens_Favor(){
		Debug.Log("Queens Favor has been called " );
	}

	public void Court_Called_To_Camelot(){

	}

	public void Pox(){

	}

	public void Plague(){

	}

	public void Chivalrous_Deed(){

	}

	public void Prosperity_Throughout_The_Realm(){

	}

	public void Kings_Call_To_Arms(){

	}







}
