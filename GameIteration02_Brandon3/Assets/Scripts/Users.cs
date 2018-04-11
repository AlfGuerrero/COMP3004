using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Users : MonoBehaviour {
	public int totalUsers;
	public string playerTurn;
	public List<GameObject> users = new List<GameObject>();

	// Use this for initialization
	void Start () {
		totalUsers = 0;
		playerTurn = GameObject.Find("PlayerTurnTextUI").GetComponent<Text>().text;
	}

	public void AddUser(string i){
		GameObject user = GameObject.Find(i);
		users.Add(user);
		totalUsers++;
	}

	public void RemoveUser(string i){
		GameObject user = GameObject.Find(i);
		users.Remove(user);
		totalUsers--;
	}

public List<GameObject> GetUsers(){
	return users;
}


	public void SetTotalUsers(int i){
		totalUsers = totalUsers + i;
	}

	public int GetTotalUsers(){
		return this.totalUsers;
	}

	public List<GameObject> getHighestRankUser(){
		List<GameObject> result = new List<GameObject>();
		int maxAttack = 0;
		foreach (GameObject i in users) {
			if (i.GetComponent<User>().getShields () > maxAttack) {
				maxAttack = i.GetComponent<User>().getShields ();
				result.Clear ();
				result.Add (i);
				continue;
			}
			if (i.GetComponent<User>().getShields () == maxAttack) {
				result.Add(i);
			}
		}
		return result;
	}

	public List<GameObject> getLowestRankUser(){
		List<GameObject> result = new List<GameObject>();
		int lowAttack = 1000;
		foreach (GameObject i in users) {
			if (i.GetComponent<User>().getShields () < lowAttack) {
				lowAttack = i.GetComponent<User>().getShields ();
				result.Clear ();
				result.Add (i);
				continue;
			}
			if (i.GetComponent<User>().getShields () == lowAttack) {
				result.Add(i);
			}
		}
		return result;
	}


}
