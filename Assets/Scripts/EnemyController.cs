using UnityEngine;
using System.Collections;

// Controls movement behaviors of enemy

public class EnemyController : MovingUnit {

	public MoveState moveState;
	public Vector3 waypoint;
	public float speed = 2.0f;
	public Transform player;
	//are we currently idling?
	//we need this so we can execute the waitforseconds ONCE and ONLY ONCE
	public bool idling;
	float step, movex, movey;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		moveState = MoveState.Idle;
		idling = false;

		base.Prepare ();
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
			speed = 2.0f;
			idling = false;

			Vector3 normalVector = new Vector3( transform.position.x - waypoint.x, transform.position.y - waypoint.y, 0);
			normalVector.Normalize();
			RaycastHit2D hit;
			bool canMove = base.CanMove ( (int)normalVector.x, (int)normalVector.y, out hit);

			if (!canMove) {
				Debug.Log("CANT MOVE");
				SetMoveState("Idle");
				return;
			} else { // can move! : D

			}

			transform.position = Vector3.MoveTowards (transform.position, waypoint, step);
			break;
		case MoveState.Follow:
			idling = false;
			speed = 3.5f;
			waypoint = player.position;
			movex = player.position.x - transform.position.x;
			movey = player.position.y - transform.position.y;
			transform.position = Vector3.MoveTowards(transform.position, waypoint, step);
			break;
		}
		if(transform.position == waypoint) {
			moveState = MoveState.Idle;
		}
	}

	// Called at a fixed time interval
	void FixedUpdate () {


	}

	//wait for a random time, then change the waypoint
	IEnumerator DoIdleStuffs(){
			idling = true;
			//random values between -5 and 5
			movex = Random.value * Random.Range(-1, 2) * 3;
			movey = Random.value * Random.Range(-1, 2) * 3;

		//	Debug.Log (movex + ": " + movey);
			float timev = (Random.value + 2.0f) * 2.0f;
			yield return new WaitForSeconds (timev);
			waypoint = new Vector3 (transform.position.x + movex, transform.position.y + movey, transform.position.z);

			moveState = MoveState.Wander;
	}


	public Vector3 GetWaypoint(){
		return new Vector3(movex, movey, 0);
	}


	public void SetMoveState(string state){
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
