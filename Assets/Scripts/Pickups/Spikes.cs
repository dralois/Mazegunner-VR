using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spikes : NetworkBehaviour {

    private void OnTriggerEnter(Collider other) {
        PlayerStats ps = other.gameObject.GetComponent<PlayerStats>();
        ps.Kill();

        Destroy(gameObject);
    }
}
