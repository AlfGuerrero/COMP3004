using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using UnityEngine.EventSystems;

public class allyzone : NetworkBehaviour, IDropHandler{

	public Logger logger;
	void Start(){
		logger = GameObject.Find("LoggerManager").GetComponent<Logger>().logger;

	}
	public void OnDrop(PointerEventData eventData){
		Draggable z = eventData.pointerDrag.GetComponent<Draggable> ();
		if(z.gameObject.GetComponent<AdventureCard>().getType() == "Ally"){
			logger.info ("allyzone.cs :: OnDrop() :: Adding Ally " + z.gameObject.GetComponent<AdventureCard>().getName());
			this.transform.parent.parent.GetComponent<User>().PlayAllies(z.gameObject.GetComponent<AdventureCard>());
			this.transform.parent.parent.GetComponent<User>().setbids(z.gameObject.GetComponent<AdventureCard>());

			Destroy(z.gameObject);
		}
	}

}
