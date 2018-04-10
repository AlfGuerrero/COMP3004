using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Card  {
	string getName (); //ally, foe, weapon, test
	string getType (); //ally, foe, weapon, test
	int getBattlePoints (); //ally, foe, weapon
	int getBidPoints (); //ally
	int getBonusBattlePoints (); //ally, foe
	int getValue (); //ally, foe, weapon, test
	int getBonusBidPoints(); //ally
	int getbonusBidRequirements (); //test
	int getbidRequirements(); //test
	void setCard (string cardName);
}
