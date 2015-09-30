using UnityEngine;
using System.Collections;

// Controls movement behaviors of enemy

public class EnemyController : MonoBehaviour {

	public MoveState moveState;
	public Vector3 waypoint;
	public float speed = 1.8f;
	public Transform player;
	//are we currently idling?
	//we need this so we can execute the waitforseconds ONCE and ONLY ONCE
	public bool idling;
	//need to know if stuck at a wall
	public bool onwall;

	public GameObject explosion;
	public GameObject attackpfab;
	GameObject attackobj;
	Animator anim;
	float step, movex, movey;
	//position on previous frame
	Vector3 previouspos;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		moveState = MoveState.Idle;
		idling = false;
		anim = GetComponent<Animator> ();
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
			StartCoroutine (DoIdleStuffs("idle"));
			}
			break;
		case MoveState.Wander:
			speed = 1.8f;
			idling = false;

			Vector3 normalVector = new Vector3( transform.position.x - waypoint.x, transform.position.y - waypoint.y, 0);
			normalVector.Normalize();
			RaycastHit2D hit;
			/*bool canMove = base.CanMove ( (int)normalVector.x, (int)normalVector.y, out hit);

			if (!canMove) {
				Debug.Log("CANT MOVE");
				SetMoveState("Idle");
				return;
			} else { // can move! : D

			}
			*/
			transform.position = Vector3.MoveTowards (transform.position, waypoint, step);
			onwall = false;
			break;
		case MoveState.Follow:
			idling = false;
			speed = 3.5f;
			waypoint = player.position;
			movex = player.position.x - transform.position.x;
			movey = player.position.y - transform.position.y;
			if (Mathf.Abs(movex) < 1.3 && Mathf.Abs (movey) < 1.3){
				//GameObject explosion = GameObject.CreatePrimitive(PrimitiveType.Cube);
				//Instantiate an explosion
				GameObject instance =
				Instantiate (explosion, transform.position, Quaternion.identity) as GameObject;
				//do attack

				moveState = MoveState.Attack;

			}
			transform.position = Vector3.MoveTowards(transform.position, waypoint, step);

			break;
		case MoveState.Attack:
			if(!idling){
				StartCoroutine(DoIdleStuffs("attack"));
			}
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
	IEnumerator DoIdleStuffs(string state){
			idling = true;
		//change animation state
			anim.SetBool ("Walking", false);
			float timev = 5.0f;
			switch (state) {
				case "idle":
					//for idle state, wait anywhere from 4 to 6 seconds
					timev = (Random.value + 1.0f) * 2.0f;
					break;
				case "attack":
					//for attack state, only stay still for .3 seconds
					Debug.Log ("starting attack");
					timev = 1.0f;
					Debug.Log ("Prepping");
					//the place to attack will be where the player is at attack prep time
					Vector3 attackplace = waypoint;
					yield return new WaitForSeconds(.1f);
					Debug.Log ("prepped");
					//after prepping for attack, instantiate one at players location at time of attack prep 
					attackobj = Instantiate(attackpfab, attackplace, transform.rotation) as GameObject;
					break;
			}
			//random values for wander movement between -5 and 5
			movex = Random.value * Random.Range(-1, 2) * 3;
			movey = Random.value * Random.Range(-1, 2) * 3;
		
			yield return new WaitForSeconds (timev);
			
			//we are now done with idle stuffs, so start walking again
			anim.SetBool ("Walking", true);
			//either wander or continue following player, depending on situation
			switch (state) {
				case "idle":
					waypoint = new Vector3 (transform.position.x + movex, transform.position.y + movey, transform.position.z);
					moveState = MoveState.Wander;
					break;
				case "attack":
					GameObject.Destroy(attackobj);
					Debug.Log("back to following");
					moveState = MoveState.Follow;
					break;
			}
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
			case "Attack":
				moveState = MoveState.Attack;
				break;
		}
	}

	//return current move state as a string
	public string GetMoveState(){
		return moveState.ToString();
	}

	public enum MoveState {
		Wander,
		Idle,
		Follow,
		Attack
	}

	void OnCollisionEnter2D(Collision2D coll){
		//get off a wall if stuck on it
		if (coll.gameObject.tag == "Wall" && moveState == MoveState.Wander && !onwall) {
			moveState = MoveState.Idle;
			idling = false;
			onwall = true;
		}
	}

}
