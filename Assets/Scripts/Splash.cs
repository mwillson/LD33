using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {
	private int i;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		i++;
		if ( i > 150){
			Application.LoadLevel("mainmenu");
		}
	}
}
