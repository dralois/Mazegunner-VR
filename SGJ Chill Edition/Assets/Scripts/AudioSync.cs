using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class AudioSync : NetworkBehaviour {
    private AudioClip[] clips;

    private AudioSource src;

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void PlaySound(int id)
    {
        CmdSendServerSound(id); //Send to server
    }

    [Command]
    void CmdSendServerSound(int id)
    {
        RpcSendSound(id); //Send to all clients
    }

    [ClientRpc]
    void RpcSendSound(int id)
    {
        src.PlayOneShot(clips[id]); //play locally
    }
}
