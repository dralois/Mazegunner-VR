using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    private void OnTriggerEnter(Collider other) {
        PlayerStats ps = other.gameObject.GetComponent<PlayerStats>();
        ps.Kill();

        Destroy(gameObject);
    }
}
