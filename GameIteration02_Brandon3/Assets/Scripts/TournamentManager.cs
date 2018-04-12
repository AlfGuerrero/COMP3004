using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TournamentManager : NetworkBehaviour {
	// protected QuestGame.Logger	logger;
	public bool TournamentInProgress=false;
	public int shieldNumber=0;
	public List<uint> PlayInTourni;
	GameObject Tourni;
	List<GameObject> Temp = new List<GameObject> ();
	GameObject submitButton;
	Text textUI;
	[SyncVar]
	public int numberofpeople;
	[SyncVar]
	public int counter;
	TournamentHolder tournamentHolder;
	public Logger logger;

	public void tourniP(){
	}

	// Use this for initialization

	void Start () { // started once player is connected.
		// logger = new QuestGame.Logger ();
		////logger.info ("TournamentManager.cs :: Initialzing Tournament Manager.");

		////logger.info ("TournamentManager.cs:: Loading PreFabs/Tourni");
		////logger.info ("TournamentManager.cs:: Loadinag PreFabs/SubmitButton");
		textUI = GameObject.Find("AdvCardTextUI").GetComponent<Text>();
		Tourni = (GameObject)Resources.Load("PreFabs/tourni");
		submitButton = (GameObject)Resources.Load("Prefabs/QuestStage");
		tournamentHolder = GameObject.Find("TournamentHolder").GetComponent<TournamentHolder>();
		logger = GameObject.Find("LoggerManager").GetComponent<Logger>().logger;
		logger.info ("TournamentManager.cs :: Initialzing Tournament Manager.");

	}

	// Update is called once per frame
	void Update () {

	}

	public void tourniSetUp (Tournament currentTourni){ 		// NOT CALLED
		logger.info ("TournamentManager.cs :: tourniSetUp() :: Setting up tournament");
		logger.info ("TournamentManager.cs :: tourniSetUp() ::" + currentTourni);
		shieldNumber = currentTourni.getBonusShields();

		////logger.info ("TournamentManager.cs::Checking to see if there was a single winner or a tie");
		// GameObject.Find ("PlayerObject(Clone)" + Winner [0]).GetComponent<User> ().setShields (shieldNumber);

	}//End of tourniSetup

	public void Joining(){  //called when Participate button has been pressed.
		if (tournamentHolder.tournamentInProgress == false) return;
		if (!isLocalPlayer) {return;}
		if (isServer) 	{RpcJoining ();}
		else 	{CmdJoining();}
	}
	[Command]
	public void CmdJoining(){
		RpcJoining ();
	}
	[ClientRpc]
	public void RpcJoining(){ // Server updates tournamentHolder for ALL clients with the users (Who pressed the button) respective values...
		if (tournamentHolder.tournamentParticipants.Contains((int)netId.Value)) {return;}
		logger.info ("TournamentManager.cs :: RpcJoining() :: " + netId.Value + " is joining tournament.");

		tournamentHolder.tournamentParticipants.Add((int)netId.Value);
		tournamentHolder.tournamentParticipantsLeft.Add((int)netId.Value);
		toggleZone(); // Called to init local instantiation.
	}
	public void toggleZone(){ // Local function that will only instantiate objects for THAT local player...
		if (!isLocalPlayer) {return;}
		Instantiate (submitButton, this.transform.GetChild (0));
	}


	public void SubmitCards(){
		if (!isLocalPlayer) {return;}
		counter++;
		int tempScore = 0;
		if (tournamentHolder.tournamentInProgress == false) return;
		foreach (Transform j in GameObject.Find("QuestStage(Clone)").transform) {
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
		// Debug.Log(counter);
		logger.info ("TournamentManager.cs :: CmdSubmitCards() :: " + netId.Value + " has submitted cards.");
		tournamentHolder.tournamentParticipantsLeft.Remove((int)id);
		if ((tournamentHolder.tournamentParticipantsLeft.Count == 0 ))
		CheckHighestBattlePoints(tournamentHolder.tournamentParticipants);
	}
	public void UpdateLists(){
		//called when Participate button has been pressed.
		if (!isLocalPlayer) {return;}
		if(true){
			//if (tournamentHolder.tournamentParticipants.Contains(){
			Instantiate (submitButton, this.transform.GetChild (0));
		}
		if (isServer) 	{RpcUpdateLists();}
		else 	{CmdUpdateLists();}

	}
	[Command]
	public void CmdUpdateLists(){
		RpcUpdateLists();
	}
	[ClientRpc]
	public void RpcUpdateLists(){
		toggleZone2();
	}
	public void toggleZone2(){
		Debug.Log("PLayer, here" + netId.Value) ;
		// foreach (int currentPlayer in tournamentHolder.tournamentParticipants){
		Debug.Log("Participants go here... 2");

	}

	public List<int> CheckHighestBattlePoints(List<int> PlayersInTourni){
		// List<int> highestAmount= new List<int>();
		tournamentHolder.highestAmount.Add(PlayersInTourni[0]);
		int	Tempvarint = 0;

		foreach (int CurrentPlayer in PlayersInTourni) {
			////logger.info ("TournamentManager.cs::Checking to see which player has the highest Tournament battle pointsg ");
			Tempvarint = GameObject.Find ("PlayerObject(Clone)" + CurrentPlayer).GetComponent<User> ().getTourniBP();
			if(Tempvarint==GameObject.Find ("PlayerObject(Clone)" + tournamentHolder.highestAmount[0]).GetComponent<User> ().getTourniBP ()){
				tournamentHolder.highestAmount.Add(CurrentPlayer);
				// Debug.Log(tournamentHolder.highestAmount.Count);
			}
			if (Tempvarint > GameObject.Find ("PlayerObject(Clone)" + tournamentHolder.highestAmount[0]).GetComponent<User> ().getTourniBP ()){
				tournamentHolder.highestAmount.Clear();
				tournamentHolder.highestAmount.Add(CurrentPlayer);
				// Debug.Log(tournamentHolder.highestAmount.Count);
			}

		}
		// bool OneWinner=true;
		//
		// if(tournamentHolder.highestAmount.Count>1){
		// 	Debug.Log("This is a tie");
		// 	Debug.Log(tournamentHolder.highestAmount);
		// 	tournamentHolder.tournamentParticipants.Clear();
		// 	tournamentHolder.tournamentParticipants=tournamentHolder.highestAmount;
		// 	OneWinner=false;
		// 	UpdateLists();
		// 	toggleZone2();
		// 	//Debug.Log("This is a tie");
		// 	//Debug.Log(tournamentHolder.highestAmount);
		// 	//tournamentHolder.tournamentParticipants.Clear();
		// 	//
		// 	//	tournamentHolder.tournamentParticipants=tournamentHolder.highestAmount;
		//
		// 	// toggleZone();
		//
		// }
		// if(OneWinner==true){
		User Temp = GameObject.Find("PlayerObject(Clone)"+ tournamentHolder.highestAmount[0]).GetComponent<User>();
		Temp.setShields(Temp.getShields()+PlayersInTourni.Count+shieldNumber);
		// 	// numberofpeople = PlayersInTourni.Count;
		// 	// CLear everything here
		//
		// }
		textUI.text = "We have a winner! Player " + tournamentHolder.highestAmount[0].ToString() ;

		Destroy(GameObject.Find("QuestStage(Clone)"));
		tournamentHolder.tournamentParticipants.Clear();
		tournamentHolder.highestAmount.Clear();
		tournamentHolder.tournamentInProgress = false;
		logger.info ("TournamentManager.cs :: CheckHighestBattlePoints() :: Calculated a winner... ");
		logger.info ("TournamentManager.cs :: CheckHighestBattlePoints() :: Player " + tournamentHolder.highestAmount[0]);
		return tournamentHolder.highestAmount;
	}




	bool sameName(string name, List<AdventureCard> cards){
		for(int i = 0; i < cards.Count; i++){
			if(cards[i].getName() == name){
				return true;
			}
		}
		return false;
	}



}//end of file
