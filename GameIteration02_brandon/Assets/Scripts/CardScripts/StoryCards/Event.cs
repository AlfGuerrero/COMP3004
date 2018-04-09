using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Event : MonoBehaviour {

	protected new string name;
	protected string function;

	protected string type;

	protected EventScriptObj event_;
	protected string card;
	// Use this for initialization
	void Start () {
//		event_ = Resources.Load<EventScriptObj> ("Event/"+card);
//		name = event_.name;
//		type = "event";
//		GetComponent<SpriteRenderer> ().sprite = event_.image;
//		function = event_.function;
	}
	public string getName(){
		return this.name;
	}
	public string getType(){
		return this.type;
	}
	public string getFunction(){
		return this.function;
	}
	public void setCard (string cardName){
		card = cardName;
				event_ = Resources.Load<EventScriptObj> ("Event/"+card);
				name = event_.name;
				type = "event";
				GetComponent<Image> ().sprite = event_.image;
				function = event_.function;
	}
}
