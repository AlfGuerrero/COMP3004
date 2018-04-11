using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : NetworkBehaviour {
	public GameObject objectToSpawn;
	public Transform spawnPoint;

	public void spawnPrefab(){
		if (!isLocalPlayer) {
			return;
		}


	}
		

}
