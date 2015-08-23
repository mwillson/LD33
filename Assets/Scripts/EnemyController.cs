using UnityEngine;
using System.Collections;

// Controls movement behaviors of enemy

public class EnemyController : MonoBehaviour {

	public MoveState moveState;
	public Vector3 waypoint;
	public float speed = 2.0f;
	public Transform player;
	//are we currently idling?
	//we need this so we can execute the waitforseconds ONCE and ONLY ONCE
	public bool idling;
	float step;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		moveState = MoveState.Idle;
		idling = false;
	}
	
	// Update is called once per frame
	void Update () {
		step = speed * Time.deltaTime;
	//	if (moveState == MoveState.Wander) {
	//		Debug.Log (transform.position);
	//	}
		switch (moveState) {
		case MoveState.Idle:
			if(!idling){
			StartCoroutine (DoIdleStuffs());
			}
			break;
		case MoveState.Wander:
			idling = false;
			transform.position = Vector3.MoveTowards (transform.position, waypoint, step);
			
			break;
		case MoveState.Follow:
			Debug.Log (player.position);
			break;
		}
		if(transform.position == waypoint) {
			Debug.Log ("We can idle " + transform.position);
			moveState = MoveState.Idle;
		}
	}

	// Called at a fixed time interval
	void FixedUpdate () {


	}

	//wait for a random time, then change the waypoint
	IEnumerator DoIdleStuffs(){
			idling = true;
			Debug.Log ("We Idled!" + waypoint);
			waypoint = new Vector3 (transform.position.x + 2, transform.position.y, transform.position.z);
			float timev = (Random.value + 2.0f) * 2.0f;
			Debug.Log (timev);
			yield return new WaitForSeconds (timev);
			moveState = MoveState.Wander;
	}


		


	void SetMoveState(string state){
		switch (state) {
			case "Idle":
				moveState = MoveState.Idle;
				break;
			case "Wander":
				moveState = MoveState.Wander;
				break;
			case "Follow":
				moveState = MoveState.Follow;
				break;
		}
	}

	public enum MoveState {
		Wander,
		Idle,
		Follow
	}

}
