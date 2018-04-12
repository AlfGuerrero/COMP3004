using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TournamentManager : NetworkBehaviour {
	protected QuestGame.Logger	logger;

	public List<uint> shit; //List of players in the tourni
	List<uint> cunt;// 		list of players
	List<uint> SPOTHOLDING;//	list of players
	public int shieldNumber=0;
	public List<uint> PlayInTourni;
	GameObject Tourni;
	List<GameObject> Temp = new List<GameObject> ();
	GameObject submitButton;


	// Use this for initialization

	void Start () {
		logger = new QuestGame.Logger ();
		logger.info ("TournamentManager.cs :: Initialzing Tournament Manager.");
		cunt.Add (1);
		SPOTHOLDING.Add (1);
		logger.info ("TournamentManager.cs:: Loading PreFabs/Tourni");
		logger.info ("TournamentManager.cs:: Loadinag PreFabs/SubmitButton");
		Tourni = (GameObject)Resources.Load("PreFabs/tourni");
		submitButton = (GameObject)Resources.Load("PreFabs/SubmitButton");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void tourniSetUp (TournamentScriptObj currentTourni){
		logger.info ("TournamentManager.cs::Setting up tournament");
		logger.info ("TournamentManager.cs:: Getting Tournament Card info(bonus Sheilds)");
		shieldNumber=currentTourni.bonusShields;


		RpcJoining(SPOTHOLDING); RpcsubmitCards (shit,cunt);

		logger.info ("TournamentManager.cs::Checking to see if there was a single winner or a tie");
		List<uint>Winner=CheckHighestBattlePoints(shit);
		if (Winner.Count > 1) {
			logger.info ("TournamentManager.cs::There was a tie");
			//call tourni again but with the list of the two players.
		} else {
			
			GameObject.Find ("PlayerObject(Clone)" + Winner [0]).GetComponent<User> ().setShields (shieldNumber);
		}
	}//End of tourniSetup

	public void Joining(List<uint> Players){  //local Button
		if (!isLocalPlayer) {return;}
		if (isServer) {
			RpcJoining (shit);
		} else {}
	}


	[Command]
	public void CmdJoining(List<uint> Players){
		RpcJoining (shit);	
	}
	[ClientRpc]
	public void RpcJoining(List<uint> Players){
		foreach (int player in Players) {
			//If user Clicks participate button add them to the new list.
			//clear their tournicards, and tournibattlepoints
			//Allow them to click the button as well.

	
			//GameObject.Find("TabCanvas").transform.getChild(0);
			//Getcomponenet<Text>().text=GameObject.Find("PlayerObject(Clone)").GetComponent<User>().GetUserName();
			//If player joins set their tournicards to clear as well as increase the number of shields in tourni.
		}
	//Sends a request to all players to see if they will join.
		//Add the number of plays to the number of total shields for the tourni.
		//Remove the buttons for the players who are not being part of the tourni.
	}
	public void SubmitCards(List<uint>PlayInTourni, List<uint>Players){
		if (!isLocalPlayer) {return;}
		if (isServer) {RpcsubmitCards(shit,cunt);}
		else{CmdSubmitCards(shit,cunt);}
	}


	[Command]
	public void CmdSubmitCards(List<uint>PlayInTourni, List<uint>Players){
		RpcsubmitCards(shit,cunt);	
	}


	[ClientRpc]
	public void RpcsubmitCards(List<uint>PlayInTourni, List<uint>Players){
		logger.info ("TournamentManager.cs::Waiting for Submit button to be pressed by clients to collect their submitted cards.");
		foreach (uint Play  in Players) {
			if (PlayInTourni.Contains (Play)) {
				//wait for the submit buttn to be pressed add all cards inside the box to the array
				//GameObject.Find("PlayerObject(Clone)"+PlayerId).getComponent<User>().setTournmanetCards;
				//TournmaentCards;
				int count= GameObject.FindGameObjectWithTag("Tourni").transform.childCount;
				for (int i = 0; i < count; i++) {
					Temp.Add(GameObject.FindGameObjectWithTag("Tourni").transform.GetChild(i).gameObject);
				}
				foreach (GameObject temp in Temp) {
					GameObject.Find ("PlayerObject(Clone)" + Play).GetComponent<User> ().SetTourni (temp.GetComponent<AdventureCard> ());
					logger.info ("TournamentManager.cs::Setting each players tournamentCards array to the submitted cards");
				}		
			}else {
				return;
			
			}
	//Asking all the players to submit their cards for the tourni
		}
	}
	public List<uint> CheckHighestBattlePoints(List<uint> PlayersInTourni){
		List<uint> highestAmount= new List<uint>();
	//check the players totals and then send back the winning player, and add their newly gained shields to them.
		foreach(uint CurrentPlayer in PlayersInTourni){
			List<AdventureCard> Addition = GameObject.Find ("PlayerObject(Clone)" + CurrentPlayer).GetComponent<User> ().GetTournmanetCards();
			int Tempcalc = GameObject.Find ("PlayerObject(Clone)" + CurrentPlayer).GetComponent<User> ().getTourniBP ();
			foreach (AdventureCard CurrentCard in Addition) {
				logger.info ("TournamentManager.cs::Calculating the total battle points of each player in tournament");
				Tempcalc += CurrentCard.getBattlePoints ();
				}
		}
		highestAmount.Add(0);
		foreach (uint CurrentPlayer in PlayersInTourni) {
			logger.info ("TournamentManager.cs::Checking to see which player has the highest Tournament battle pointsg ");
			int Tempvarint=GameObject.Find ("PlayerObject(Clone)" + CurrentPlayer).GetComponent<User> ().getTourniBP();
				if (Tempvarint > GameObject.Find ("PlayerObject(Clone)" + highestAmount[0]).GetComponent<User> ().getTourniBP ()){
				highestAmount.Clear();
				highestAmount.Add(CurrentPlayer);
			}

		}
		return highestAmount;
	}


}
