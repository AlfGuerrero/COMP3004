using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Test", menuName = "Cards/Test")]
public class TestScriptObj : ScriptableObject {


	public new string name;
	public Sprite image;
	public int value;
	public int bidRequirements;
	public int bonusBidRequirements;
}
