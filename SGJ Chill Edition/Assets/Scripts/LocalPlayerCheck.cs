using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayerCheck : NetworkBehaviour
{

    public override void OnStartClient()
    {
        print(isLocalPlayer);
        print(isServer);
        print(isClient);

        gameObject.active = isLocalPlayer ;
    }
}
