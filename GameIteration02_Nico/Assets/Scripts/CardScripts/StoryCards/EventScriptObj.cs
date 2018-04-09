using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Event", menuName = "Cards/Event")]
public class EventScriptObj : ScriptableObject {


	public new string name;
	public string function;
	public Sprite image;

}