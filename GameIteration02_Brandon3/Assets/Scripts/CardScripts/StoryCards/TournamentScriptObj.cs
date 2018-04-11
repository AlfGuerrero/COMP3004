using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Tournament", menuName = "Cards/Tournament")]
public class TournamentScriptObj : ScriptableObject {

	public new string name;
	public Sprite image;
	public int bonusShields;

}
