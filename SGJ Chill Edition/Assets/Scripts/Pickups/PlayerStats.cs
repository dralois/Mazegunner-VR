using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpeedBoost(float amount, float time) {
        Debug.Log("Speed");
    }

    public void Invisibility(float time) {
        Debug.Log("Invisibility");
    }

    public void Shield(float time) {
        Debug.Log("Shield");
    }
}
