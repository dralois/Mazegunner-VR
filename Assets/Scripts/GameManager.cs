using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum GamePhase {
	PRE_GAME,
	GAME,
	AFTER_GAME,
}

public class GameManager : NetworkBehaviour {

	public int livesPerPlayer = 3;
	public float gameDurationSeconds = 300;
	private float gameTimeLeft = 300;
	public GameObject[] spawnWalls;
	public PlayerStats[] playerStats;
	public VRPlayerScript vrPlayer;
	public GamePhase phase = GamePhase.PRE_GAME;
	public int numberOfTraps = 4;

	// VR Player gets how many traps they can place
	public int getTrapCount(){
		return numberOfTraps;
	}

	// VR Player calls this when they are ready to start the game
	public void trapsPlaced(){
		SetupPhase(GamePhase.GAME);
	}

	// Use this for initialization
	void Start () {
		((ExtendedNetworkManager) NetworkManager.singleton).gameManager = this;
		SetupPhase(phase);
	}
	
	// Update is called once per frame
	void Update () {
		if(phase == GamePhase.GAME){			
			gameTimeLeft -= Time.deltaTime;
			if(gameTimeLeft < 0){
				SetupPhase(GamePhase.AFTER_GAME);
			}		 	
		}
	}

	void SetupPhase(GamePhase newPhase) {
		phase = newPhase;
		switch(phase) {
			case GamePhase.PRE_GAME:
			//make sure the spawnWalls are all enabled:
			//TODO or reload Scene
			break;

			case GamePhase.GAME:
			//disable all spawnWalls:
			foreach(GameObject sw in spawnWalls){
				Destroy(sw);
			}
			//collect players
			vrPlayer = GameObject.FindObjectOfType<VRPlayerScript>();
			playerStats = GameObject.FindObjectsOfType<PlayerStats>();
			//notify players that the game has started
			foreach(PlayerStats ps in playerStats){
				ps.OnGameStarted(livesPerPlayer, gameDurationSeconds);
			}
			vrPlayer.OnGameStarted(livesPerPlayer, gameDurationSeconds);
			break;

			case GamePhase.AFTER_GAME:
			foreach(PlayerStats ps in playerStats){
				ps.OnGameFinished(playerStats);
			}
			vrPlayer.OnGameFinished(playerStats);
			break;
		}
	}
}
