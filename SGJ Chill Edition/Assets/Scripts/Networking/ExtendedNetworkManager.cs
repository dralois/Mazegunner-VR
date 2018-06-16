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

    public class NetworkMessage : MessageBase {
        public bool isVR;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader) {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        bool isVRPlayer = message.isVR;

        GameObject player;
        if (isVRPlayer) {
            player = Instantiate(Resources.Load("VR_Player", typeof(GameObject))) as GameObject;
        } else {
            player = Instantiate(Resources.Load("PC_Player", typeof(GameObject))) as GameObject;
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnClientConnect(NetworkConnection conn) {
        NetworkMessage playerType = new NetworkMessage {
            isVR = inVR
        };

        ClientScene.AddPlayer(conn, 0, playerType);
    }

    public override void OnClientSceneChanged(NetworkConnection conn) {
        //base.OnClientSceneChanged(conn);
    }

    public void EngageVRMode() {
        inVR = true;
    }
}
