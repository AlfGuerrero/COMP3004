using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardType : MonoBehaviour {

	void Update(){
		/*string cardType = "null";


		if(this.GetComponent<Foe>() != null){
			cardType = "Foe";
			Debug.Log (cardType);
		}else if(this.GetComponent<Ally>() != null){
			cardType = "Ally";
			Debug.Log (cardType);

		}else if(this.GetComponent<Test>() != null){
			cardType = "Test";
			Debug.Log (cardType);

		}else if(this.GetComponent<Weapon>() != null){
			cardType = "Weapon";
			Debug.Log (cardType);

		}

		Debug.Log (cardType);*/
	}

	public string getCardType(){
		string cardType = "null";
		Debug.Log (cardType);

		if(this.GetComponent<Foe>() != null){
			cardType = "Foe";
			Debug.Log (cardType);

		}else if(this.GetComponent<Ally>() != null){
			cardType = "Ally";
			Debug.Log (cardType);

		}else if(this.GetComponent<Test>() != null){
			cardType = "Test";
			Debug.Log (cardType);

		}else if(this.GetComponent<Weapon>() != null){
			cardType = "Weapon";
			Debug.Log (cardType);

		}
		return cardType;
	}
	public string test(){
		return "asdf";
	}

}
