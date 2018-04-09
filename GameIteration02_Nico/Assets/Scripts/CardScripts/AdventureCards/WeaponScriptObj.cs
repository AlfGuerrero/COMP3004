using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Cards/Weapon")]
public class WeaponScriptObj : ScriptableObject {

	public new string name;
	public Sprite image;
	public int value;
	public int battlePoints;
}

