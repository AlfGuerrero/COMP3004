using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Foe", menuName = "Cards/Foe")]
public class FoeScriptObj : ScriptableObject {


	public new string name;
	public Sprite image;
	public int value;
	public int battlePoints;
	public int bonusBattlePoints;
	public bool mordred;
}
