using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TournamentManager : NetworkBehaviour {
	// protected QuestGame.Logger	logger;
	public bool TournamentInProgress=false;
	public int shieldNumber=0;
	public List<uint> PlayInTourni;
	GameObject Tourni;
	List<GameObject> Temp = new List<GameObject> ();
	GameObject submitButton;
	TournamentHolder tournamentHolder;
	public void tourniP(){
	}

	// Use this for initialization

	void Start () {
		// logger = new QuestGame.Logger ();
		////logger.info ("TournamentManager.cs :: Initialzing Tournament Manager.");

		////logger.info ("TournamentManager.cs:: Loading PreFabs/Tourni");
		////logger.info ("TournamentManager.cs:: Loadinag PreFabs/SubmitButton");
		Tourni = (GameObject)Resources.Load("PreFabs/tourni");
		submitButton = (GameObject)Resources.Load("Prefabs/QuestStage");
		tournamentHolder = GameObject.Find("TournamentHolder").GetComponent<TournamentHolder>();
	}

	// Update is called once per frame
	void Update () {

	}

	public void tourniSetUp (Tournament currentTourni){
		////logger.info ("TournamentManager.cs::Setting up tournament");
		////logger.info ("TournamentManager.cs:: Getting Tournament Card info(bonus Sheilds)");
		shieldNumber = currentTourni.getBonusShields();
		tournamentHolder.tournamentInProgress = true;

		////logger.info ("TournamentManager.cs::Checking to see if there was a single winner or a tie");
		List<uint>Winner= new List<uint>();
		if (Winner.Count > 1) {
			////logger.info ("TournamentManager.cs::There was a tie");
			//call tourni again but with the list of the two players.
		} else {

			// GameObject.Find ("PlayerObject(Clone)" + Winner [0]).GetComponent<User> ().setShields (shieldNumber);
		}
	}//End of tourniSetup

	public void Joining(){  //local Button
		if (tournamentHolder.tournamentInProgress == false) return;
		if (!isLocalPlayer) {return;}
		if (isServer) {
			RpcJoining ();
			}else {CmdJoining();}
		}

		[Command]
		public void CmdJoining(){
			RpcJoining ();
		}
		[ClientRpc]
		public void RpcJoining(){
			if (tournamentHolder.tournamentParticipants.Contains((int)netId.Value)) {return;}

			tournamentHolder.tournamentParticipants.Add((int)netId.Value);

			//Sends a request to all players to see if they will join.
			//Add the number of plays to the number of total shields for the tourni.
			//Remove the buttons for the players who are not being part of the tourni.
			toggleZone();
		}
		public void toggleZone(){
			if (!isLocalPlayer) {return;}
			Instantiate (submitButton, this.transform.GetChild (0));
		}

		public void SubmitCards(){
			if (!isLocalPlayer) {return;}
			Debug.Log("I am here");
			int tempScore = 0;
				foreach (Transform j in GameObject.Find("QuestStage(Clone)").transform) {
					Debug.Log("Cards: " + j.GetComponent<AdventureCard>());
					this.GetComponent<User>().SetTourni(j.GetComponent<AdventureCard>());
						tempScore += j.GetComponent<AdventureCard>().getBattlePoints();
				}
				this.GetComponent<User>().setTourniBP(tempScore + this.GetComponent<User>().getBaseAttack());


			if (isServer) {RpcSubmitCards(netId.Value, this.GetComponent<User>().getTourniBP());}
			else{CmdSubmitCards(netId.Value, this.GetComponent<User>().getTourniBP());}
		}


		[Command]
		public void CmdSubmitCards(uint id, int score){
			RpcSubmitCards(id, score);
		}


		[ClientRpc]
		public void RpcSubmitCards(uint id, int score){
			GameObject.Find("PlayerObject(Clone)"+id).GetComponent<User>().setTourniBP(score);

			// CheckHighestBattlePoints(tournamentHolder.tournamentParticipants);
		}


		public List<int> CheckHighestBattlePoints(List<int> PlayersInTourni){
			List<int> highestAmount= new List<int>();
			//check the players totals and then send back the winning player, and add their newly gained shields to them.
			// foreach(int CurrentPlayer in PlayersInTourni){
			// 	List<AdventureCard> Addition = GameObject.Find ("PlayerObject(Clone)" + CurrentPlayer).GetComponent<User> ().GetTournmanetCards();
			// 	int Tempcalc = GameObject.Find ("PlayerObject(Clone)" + CurrentPlayer).GetComponent<User> ().getTourniBP ();
			// 	foreach (AdventureCard CurrentCard in Addition) {
			// 		////logger.info ("TournamentManager.cs::Calculating the total battle points of each player in tournament");
			// 		Tempcalc += CurrentCard.getBattlePoints ();
			// 	}
			// }
			highestAmount.Add(0);
			foreach (int CurrentPlayer in PlayersInTourni) {
				////logger.info ("TournamentManager.cs::Checking to see which player has the highest Tournament battle pointsg ");
				int Tempvarint=GameObject.Find ("PlayerObject(Clone)" + CurrentPlayer).GetComponent<User> ().getTourniBP();

				if (Tempvarint > GameObject.Find ("PlayerObject(Clone)" + highestAmount[0]).GetComponent<User> ().getTourniBP ()){
					highestAmount.Clear();
					highestAmount.Add(CurrentPlayer);
				}

			}
			return highestAmount;
		}


		bool sameName(string name, List<AdventureCard> cards){
			for(int i = 0; i < cards.Count; i++){
				if(cards[i].getName() == name){
					return true;
				}
			}
			return false;
		}




		public void SubmitWeaponsTourni(){

			if (!isLocalPlayer) {return;}
			if (isServer) {RpcSubmitWeaponsTourni();}
			else					{CmdSubmitWeaponsTourni();}

		}

		[Command]
		public void CmdSubmitWeaponsTourni(){
			RpcSubmitWeaponsTourni ();
		}



		[ClientRpc]
		public void RpcSubmitWeaponsTourni(){
		}
































	}//end of file
