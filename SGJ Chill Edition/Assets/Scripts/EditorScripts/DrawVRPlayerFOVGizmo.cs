using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawVRPlayerFOVGizmo : MonoBehaviour {

	void OnDrawGizmos(){
		Gizmos.matrix = Matrix4x4.TRS(
         transform.position,
         transform.rotation,
         Vector3.one);
		Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, 2);
		Gizmos.DrawFrustum(Vector3.zero, 100, 5, 1, 1);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
