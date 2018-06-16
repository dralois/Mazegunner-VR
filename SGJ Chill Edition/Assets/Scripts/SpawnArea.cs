using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnArea : MonoBehaviour {
	private int numPlayersJoined = 0;
	private List<Vector3> playerPositions = new List<Vector3>(); 
	public Vector3 dimensions = new Vector3(2, 2, 2);
	public int maxPlayers = 10;
	public int playerRadiusMin = 1;

	public Vector3 getPlayerSpawn(){
		if(numPlayersJoined == maxPlayers){
			Debug.LogWarning("SpawnArea: max number of Players reached. Please increase maxPlayers.");
		}
		numPlayersJoined++;
		return playerPositions[numPlayersJoined - 1 % maxPlayers];
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, dimensions);
		
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		foreach(Vector3 pos in playerPositions){
        	Gizmos.DrawWireSphere(pos, (float)playerRadiusMin / 2);
		}
	}

	// Use this for initialization
	void Start () {
		if(Application.isPlaying){
			generatePlayerSpawns();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.isEditor){
			generatePlayerSpawns();
		}
	}

	private void generatePlayerSpawns(){
		int gridUnitsX = (int)(dimensions.x) / playerRadiusMin;
		int gridUnitsZ = (int)(dimensions.z) / playerRadiusMin;
		int maxPlayerSpawns = gridUnitsX * gridUnitsZ;
		if(maxPlayerSpawns < maxPlayers){
			Debug.LogWarning("The SpawnArea does not provide enough space for " + maxPlayers + " players. Calculated max players: " + maxPlayerSpawns);
		}
		int ratio = maxPlayerSpawns / maxPlayers;
		playerPositions.Clear();
		for(int i = 0; i < maxPlayers; i++){
			int row = i % gridUnitsX,
				column = i / gridUnitsZ;
			float x = transform.position.x - dimensions.x / 2,
				z = transform.position.z - dimensions.z / 2;
			x += (dimensions.x / gridUnitsX) * (row + 0.5f);
			z += (dimensions.z / gridUnitsZ) * (column + 0.5f);
			

			playerPositions.Add(new Vector3(x, transform.position.y, z));
		}
	}
}
