﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New AdeventureCard", menuName = "AdventureCard")]
public class ACScriptObj : ScriptableObject {


	public new string name;
	public Sprite image;

	public int bidPoints;
	public int bonusBidPoints;
	public int battlePoints;
	public int bonusBattlePoints;
	public int value;
	public bool merlin;
	public bool mordred;
	public string type;


}