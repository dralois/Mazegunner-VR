using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : MonoBehaviour {

	public bool isShooting {
		get {return _shooting;}
		set {
			animator.SetBool("shooting", value);
			_shooting = value;
		}
	}

	public bool isActive {
		get {return _active;}
		set {
			if(value && !_active &&  railgun_activate != null){
				audio.PlayOneShot(railgun_activate);
			}
			animator.SetBool("activated", value);
			_active = value;
		}
	}

	public AudioClip railgun_activate;
	Animator animator;
	AudioSource audio;

	private bool _shooting = false;
	private bool _active = false;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void FireLaser () {
		//Debug.Log("Imma firin' mah lazor");
		audio.Play();
	}
}
