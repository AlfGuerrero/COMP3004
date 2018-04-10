using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Info : MonoBehaviour {

	public int numberOfStages = 0;
	public GameObject[] stages = null;
	public List<int> participants = new List<int>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void SetStages(GameObject[] stages){
		this.stages = stages;
	}

}
