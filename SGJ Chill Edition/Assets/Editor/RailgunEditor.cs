using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Railgun))]
public class RailgunEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Railgun rg = (Railgun)target;
        if(GUILayout.Button("Toggle Active"))
        {
            rg.isActive = !rg.isActive;
        }

		if(GUILayout.Button("Toggle Shooting"))
        {
            rg.isShooting = !rg.isShooting;
        }
    }
}