using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExtendedNetworkManager : NetworkManager {

#if (UNITY_ANDROID)
    private bool inVR = true;
#else
    private bool inVR = false;
#endif
    public GameObject VRPlayerSpawn;
    public SpawnArea PCPlayerSpawns;
    public GameManager gameManager;

    private short currentID = 0;

    public class NetworkMessage : MessageBase {
        public bool isVR;
        
    }

    // Called on the server when a client adds a new player with ClientScene.AddPlayer.
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader) {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        bool isVRPlayer = message.isVR;
        

        GameObject player;
        if (isVRPlayer) {
            player = Instantiate(Resources.Load("VRPlayer", typeof(GameObject)), VRPlayerSpawn.transform) as GameObject;
        } else {
            player = Instantiate(Resources.Load("PC_Player", typeof(GameObject)), PCPlayerSpawns.getPlayerSpawn(), Quaternion.identity) as GameObject;
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    // Called on the client when connected to a server.
    public override void OnClientConnect(NetworkConnection conn) {
        NetworkMessage playerType = new NetworkMessage {
            isVR = inVR
        };

        ClientScene.AddPlayer(conn, currentID, playerType);
        currentID++;
    }

    // Called on clients when a scene has completed loaded, when the scene load was initiated by the server.
    public override void OnClientSceneChanged(NetworkConnection conn) {
        //base.OnClientSceneChanged(conn);
    }

    public void EngageVRMode() {
        inVR = true;
    }

#if (UNITY_ANDROID)
    void Start() {
        StartHost();
    }
#endif
}
