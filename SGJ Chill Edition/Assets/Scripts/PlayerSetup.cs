using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{

    //[SerializeField]
    public Behaviour[] componentsToDisable;
    public Behaviour[] componentsToEnable;

    void Start()
    {
        // Disable components that should only be
        // active on the player that we control
        if (!isLocalPlayer)
        {
            DisableComponents();
            EnableComponents();
        }
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }
    void EnableComponents()
    {
        for (int i = 0; i < componentsToEnable.Length; i++)
        {
            componentsToEnable[i].enabled = true;
        }

    }
}
