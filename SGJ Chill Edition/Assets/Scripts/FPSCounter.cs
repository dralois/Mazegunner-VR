using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

    private Text fps;

	// Use this for initialization
	void Start () {
        fps = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        fps.text = 1 / Time.deltaTime + " FPS";
	}
}
