using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : MonoBehaviour {

	public bool isShooting = false;
	public bool isActive = false;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetBool("shooting", isShooting);
		animator.SetBool("activated", isActive);
	}

	public void FireLaser () {
		Debug.Log("Imma firin' mah lazor");
	}
}
