using UnityEngine;
using System.Collections;

public class ConeSightScript : MonoBehaviour {

	GameObject player;
	public bool playerInSight;
	public Vector2 lineOfSight;
	public float fovAngle = 100f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay2D(Collision2D other){
		/*if (other.gameObject == player) {
			playerInSight = false;
			Vector3 direction = other.transform.position - transform.parent.position;
			float angle = Vector3.Angle(direction, transform.parent.right);
			Debug.Log (angle);
		}*/
	}
}
