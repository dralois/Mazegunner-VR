using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{

    //[SerializeField]
    public Behaviour camera;
    public Renderer meshRender;

    void Start()
    {
        // Disable components that should only be
        // active on the player that we control
        if (!isLocalPlayer)
        {
            camera.enabled = false;
            meshRender.enabled = true;
        }
        else
        {
            camera.enabled = true;
            meshRender.enabled = false;
        }
    }
}
