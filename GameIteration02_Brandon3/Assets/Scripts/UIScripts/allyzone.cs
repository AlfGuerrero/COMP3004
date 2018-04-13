using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class allyzone : MonoBehaviour, IDropHandler{

	public void OnDrop(PointerEventData eventData){
		Draggable z = eventData.pointerDrag.GetComponent<Draggable> ();
		if(z.gameObject.GetComponent<AdventureCard>().getType() == "Ally"){
			Debug.Log("Adding an Ally");
			this.transform.parent.parent.GetComponent<User>().PlayAllies(z.gameObject.GetComponent<AdventureCard>());
			this.transform.parent.parent.GetComponent<User>().setbids(z.gameObject.GetComponent<AdventureCard>());

			Destroy(z.gameObject);
		}
	}

}
