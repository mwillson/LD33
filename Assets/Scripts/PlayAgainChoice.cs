using UnityEngine;
using System.Collections;

public class PlayAgainChoice : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (Input.GetKeyDown (KeyCode.Y)) {
			Application.LoadLevel("level1");
		} else if (Input.GetKeyDown (KeyCode.N)) {
			Application.Quit();
		}
	}
}
