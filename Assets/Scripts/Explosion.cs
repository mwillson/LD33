using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	int age = 0;
	GameObject mySelf;
	void Start (){
		mySelf = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		age++;
		if (age > 200) {
			Destroy(mySelf);

		}
	}
}
