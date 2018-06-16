using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shield : NetworkBehaviour {

    public float time = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        PlayerStats ps = other.gameObject.GetComponent<PlayerStats>();
        ps.Shield(time);

        Destroy(gameObject);
    }
}
