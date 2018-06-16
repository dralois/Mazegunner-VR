using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SnapToLabyrinth : MonoBehaviour {

     public float snapValue = 2;
     public float heightLevel = 0;    
     
     void Update() {
         float snapInverse = 1/snapValue;
         
         float x, y, z;
         
         // if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
         // so 1.45 to nearest .5 is 1.5
         x = Mathf.Round(transform.position.x * snapInverse)/snapInverse;
         y = heightLevel;   
         z = Mathf.Round(transform.position.z * snapInverse)/snapInverse;  // depth from camera
         
         transform.position = new Vector3(x, y, z);
     }
 
}
