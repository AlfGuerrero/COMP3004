using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class discardzone : MonoBehaviour, IDropHandler{

	public void OnDrop(PointerEventData eventData){
		Draggable z = eventData.pointerDrag.GetComponent<Draggable> ();
		Destroy(z.gameObject);
	}
}
