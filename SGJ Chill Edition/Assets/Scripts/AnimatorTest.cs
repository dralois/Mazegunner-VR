using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour {
    Animator animator;
    public bool wakling = false;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        print(animator.GetBool("walking"));
        if (Input.GetKeyDown(KeyCode.A)){
            wakling = !wakling;
            animator.SetBool("walking", wakling);
        }
	}
}
