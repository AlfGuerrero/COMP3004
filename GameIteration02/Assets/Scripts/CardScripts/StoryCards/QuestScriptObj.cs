using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Quest", menuName = "Cards/Quest")]
public class QuestScriptObj : ScriptableObject {

	public new string name;
	public string bonusFoe;
	public int stages;
	public Sprite image;

}
