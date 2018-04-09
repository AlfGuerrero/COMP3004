using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class Draggable : NetworkBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

//	Vector2 dragOffset = new Vector2(0f, 0f);
	public Transform parentToReturnTo;
	public Vector2 positionToReturnTo;

	public void OnBeginDrag(PointerEventData eventData)    {

		parentToReturnTo = this.transform.parent;
		positionToReturnTo = this.transform.position;
//		this.transform.SetParent (this.transform.parent);
//		dragOffset = eventData.position - (Vector2)this.transform.localPosition;
//		dragOffset = (Vector2)Input.mousePosition - (Vector2)this.transform.position;
		// Debug.Log ((Vector2)this.transform.position);
		// Debug.Log ((Vector2)Input.mousePosition);

		this.GetComponent<BoxCollider2D> ().enabled = false;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData) {

		//this.transform.position = eventData.position;

		this.transform.position = 0.01f*(Vector2)Input.mousePosition - new Vector2(6f,3f);


	}

	public void OnEndDrag(PointerEventData eventData) {
		this.transform.SetParent (parentToReturnTo);
		this.transform.position = positionToReturnTo;
		this.GetComponent<BoxCollider2D> ().enabled = true;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
}
