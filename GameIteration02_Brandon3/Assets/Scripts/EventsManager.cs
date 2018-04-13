using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
//	public //logger //logger;

	void Start(){
		//logger = GameObject.Find("//loggerManager").GetComponent<//logger>().//logger;
		//logger.info ("EventsManager.cs :: Initialzing Event Manager.");
	}

	// - This next player(s) to complete a Quest will receive 2 extra shields.
	public void Kings_Recoginition(uint id){
		//logger.info("EventsManager.cs :: Kings_Recoginition() :: setting shields for " + (int)id);
		User user = GameObject.Find("PlayerObject(Clone)" + id).GetComponent<User>();
		int shields = user.getShields() + 2;
		//logger.info("EventsManager.cs :: Kings_Recoginition() :: shield is " + (shields - 2));
		//logger.info("EventsManager.cs :: Kings_Recoginition() :: shield is " + shields);

		user.setShields(shields);
	}

	// 2. Queen's Favor
	// - The lowest rank player(s) immediately receives 2 Adventure Cards.
	public List<GameObject> Queens_Favor(Users players){
		//logger.info("EventsManager.cs :: Queens_Favor() :: Get Lowest Rank User(s) ");
		List<GameObject> lowestRankPlayers = players.getLowestRankUser();
		return lowestRankPlayers;
	}
	// 3. Court Called to Camelot
	// - All Allies in play must be discarded.
	public void Court_Called_To_Camelot(Users players){
		//logger.info("EventsManager.cs :: Court_Called_To_Camelot() :: Remove All Allies. ");
		// Call Hand Manager from each user to discard all cards.
		foreach (GameObject i in players.GetUsers()) {
			string rank = i.GetComponent<User> ().getRank ();
			// int baseAttack = i.GetComponent<User> ().getbaseAttack ();
				i.GetComponent<User> ().baseAttack = 5;
		}
	}
	// 4. Pox
	// - All players except the player drawing this card lose 1 shield.
	public void Pox(User player, Users players){
		//logger.info("EventsManager.cs :: Pox() :: All players except Loses one shield. ");
		//logger.info("EventsManager.cs :: Pox() :: " + player + " will NOT lose shields.");

		foreach (GameObject i in players.GetUsers()) {

			int shields = i.GetComponent<User> ().getShields ();
			if(!player.Equals(i.GetComponent<User>())){
				if (shields != 0) {
					//logger.info("EventsManager.cs :: Pox() :: " + i.GetComponent<User>() + " will lose shields.");
					//logger.info("EventsManager.cs :: Pox() :: " + i.GetComponent<User>().getShields());
					i.GetComponent<User> ().setShields (shields - 1);
					//logger.info("EventsManager.cs :: Pox() :: " + i.GetComponent<User>().getShields());
				}
			}
		}
	}
	// 5. Plague
	// - Drawer loses 2 shields if possible.
	public void Plague(User player){
		//logger.info("EventsManager.cs :: Plague() :: Drawer loses 2 shields if possible. ");
		//logger.info("EventsManager.cs :: Plague() :: " + player + "Loses 2 shields");
		int shields = player.getShields ();
		//logger.info("EventsManager.cs :: Plague() :: has " + shields + ".");
		if (shields != 0) {
			player.setShields (shields - 2);
			//logger.info("EventsManager.cs :: Plague() :: has " + shields + ".");

		}
	}
	// 6. Chivalrous Deed
	// - Player(s) with both lowest rank and least amount of shields, receives 3 shields.
	public void Chivalrous_Deed(User player, Users players){
		//logger.info("EventsManager.cs :: Chivalrous_Deed() :: Player(s) with both lowest rank and least amount of shields, receives 3 shields. ");
		List<GameObject> lowestRankPlayer = players.getLowestRankUser();
		foreach (GameObject i in lowestRankPlayer) {
			int shields = i.GetComponent<User>().getShields ();
			//logger.info("EventsManager.cs :: Chivalrous_Deed() :: Shields " + i + " is : " + shields);
			i.GetComponent<User>().setShields (shields + 3);
			//logger.info("EventsManager.cs :: Chivalrous_Deed() :: Shields " + i + " is : " + shields);
		}
	}

	// 7. Prosperity Throughout the Realm
	// - All players may immediately draw 2 Adventure Cards.
	public List<GameObject> Prosperity_Throughout_The_Realm(Users players){
		//logger.info("EventsManager.cs :: Prosperity_Throughout_The_Realm() :: All players may immediately draw 2 Adventure Cards. ");

		List<GameObject> allPlayers = players.GetUsers();
		return allPlayers ;
	}
	// 8. King's Call to Arms
	// - The Highest Ranked Player(s) must place 1 weapon in the discard pile. If unable to do so 2 Foe Cards must be discarded.
	public List<GameObject> Kings_Call_To_Arms(Users players){
		//logger.info("EventsManager.cs :: Kings_Call_To_Arms() :: The Highest Ranked Player(s) must place 1 weapon in the discard pile. If unable to do so 2 Foe Cards must be discarded.");
		List<GameObject> highestRankPlayer = players.getHighestRankUser();
		// highestRankPlayer.DiscardWeaponFunction();
		return highestRankPlayer;
	}

}
