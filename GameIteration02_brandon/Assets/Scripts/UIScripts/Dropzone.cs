using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IDropHandler {
	//QuestManager q = GameObject.Find("QuestManager").GetComponent<QuestManager>();
	bool foe = false;
	bool test = false;
	int strength = 0;

	Dictionary<string, bool> weapons = new Dictionary<string, bool>();

	void Start(){
		weapons.Add ("Dagger", false);
		weapons.Add ("Sword", false);
		weapons.Add ("Horse", false);
		weapons.Add ("Battle Ax", false);
		weapons.Add ("Lance", false);
		weapons.Add ("Excalibur", false);
	}

	public void OnDrop(PointerEventData eventData){
		Draggable z = eventData.pointerDrag.GetComponent<Draggable> ();
		z.parentToReturnTo = this.transform;
		z.positionToReturnTo = new Vector2 (0f, 0f);
		/*if (z.type == "foe" && !foe) {
			z.parentToReturnTo = this.transform;
			foe = true;
			strength = getFoe.strength;
		}else if (z.type == "test" && !test) {
			z.parentToReturnTo = this.transform;
			test = true;
		}if (foe && z.type == "weapon") {
			foreach(KeyValuePair<string, bool> s in weapons){
				if(getWeaponName == s.Key && !s.Value){
					z.parentToReturnTo = this.transform;
					strength += getWeapon.strength;
					s.Value = true;
				}
			}
		}*/
	}

	//checks to see if its elligible for a stage
	bool checkChildren(){
		bool enemy = false;
		bool challenge = false;
		bool weapon = false;

		Transform[] children = this.transform.GetComponentsInChildren<Transform> ();
		/*foreach (Transform t in children) {
			if (t.type == "foe") {
				enemy = true;
				foe = true;
			} else if (t.type == "test") {
				challenge = true;
				test = true;
			} else if (t.type == "weapon") {
				weapon = true;
			}
		}*/
		if(!enemy){
			foe = false;
		}
		if(!challenge){
			test = false;
		}


		if (challenge && enemy) {
			return false;
		} else if (challenge && weapon) {
			return false;
		} else if (weapon && !enemy && !challenge) {
			return false;
		} else if (!enemy && !challenge && !weapon) {
			return false;
		} else {
			return true;
		}
	}

	public int getStrengthScore(){
		return strength;
	}

	public void OnPointerEnter(PointerEventData eventData){

	}
	public void OnPointerExit(PointerEventData eventData){

	}
}