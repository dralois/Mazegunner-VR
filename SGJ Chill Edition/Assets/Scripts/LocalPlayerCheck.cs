using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayerCheck : NetworkBehaviour
{
    void Start()
    {
        gameObject.SetActive(isLocalPlayer);
    }
}
