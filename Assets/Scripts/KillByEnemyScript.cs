using UnityEngine;
using System.Collections;

public class KillByEnemyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == GameObject.FindGameObjectWithTag ("KnightAttack")) {
			//GameManager.Notify (KillEvent, gameObject.name);
		}
	}

  

}
