using UnityEngine;
using System.Collections;

public class EnemyAttackScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){
		Debug.Log ("ERERER");
		if (other.gameObject.tag == "Enemy") {
			Debug.Log ("ARG");
			other.gameObject.GetComponent<Animator>().SetBool("Attacking", true);
		}
	}
}
