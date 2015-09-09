using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	public Vector3 direction;
	public Vector3 savedvelocity;
	public DashState dashState;
	public float speed = 1.75f;
	public float dashTimer;
	public float maxDash = 0.40f;
	public GameObject projectile;
	public Vector2 savedposition;
	Vector3 previouslocation, currentlocation, targetloc;
	Quaternion newrotation;
	Animator anim;
	//direction we are facing
	Vector3 facing;
	//melee attack gameobject that we instantiate
	GameObject attackobj;

	// Use this for initialization
	void Start () {
		dashState = DashState.Ready;
		anim = GetComponent<Animator> ();
		facing = new Vector3 (1, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
		//transform.position = savedposition;
		direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		//base.CanMove(Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw("Vertical"), out hit);
		//Debug.Log (Input.GetAxisRaw ("Horizontal"));

		//if we have pushed the control stick change facing direction
		if (direction != new Vector3 (0, 0, 0)) {
			facing = direction;
		}
	 

		if (direction != new Vector3 (0, 0, 0)) {
			anim.SetBool("Walking", true);
		} else {
			anim.SetBool("Walking", false);
		}
		//Quaternion.Slerp (transform.rotation, 
		//                                      newrotation,
		//                                      5*Time.fixedDeltaTime);

		//Doing position transformations in here seemed to cause weird wall collision behavior
		//This has been moved to fixedupdate

		/*switch (dashState){
		case DashState.Ready:
			
			transform.Translate(direction * speed * Time.deltaTime);
			break;
		case DashState.Dashing:
			transform.Translate(direction * 1.75f * speed * Time.deltaTime);
			break;
		case DashState.Cooldown:
			transform.Translate(0, 0, 0);
			break;
		}*/

	}

	void LateUpdate(){
		//do some rotation for movement
		//have to do this in lateupdate after all subparts of sprite are drawn
		targetloc = transform.position + direction;
		//newrotation = Quaternion.LookRotation(targetloc-transform.position, Vector3.forward);
		


		//if we are not idling, calculate the new rotation to switch to
		if (direction != new Vector3 (0, 0, 0)) {
			newrotation = Quaternion.LookRotation(targetloc-transform.position, Vector3.forward);
			newrotation = newrotation * Quaternion.Euler (0, 0, 270);
		}
		
			newrotation.x = 0;
			newrotation.y = 0;

			transform.GetChild (0).rotation = newrotation;
	}

	void FixedUpdate () {


		switch (dashState){
		case DashState.Ready:
			if (Input.GetButtonDown("Trigger")){
				dashState = DashState.Dashing;
			}

			transform.Translate(direction * speed * Time.deltaTime);


			break;
		case DashState.Dashing:
			dashTimer += Time.deltaTime;
			if(dashTimer >= maxDash){
				dashTimer = maxDash;
				dashState = DashState.Cooldown;
			}

			transform.Translate(direction * 2.75f * speed * Time.deltaTime);

			break;
		case DashState.Cooldown:
			dashTimer -= Time.deltaTime;
			if(dashTimer <= -.3f){
				dashTimer = 0;
				dashState = DashState.Ready;
			}

			transform.Translate(0, 0, 0);


			break;

		
		}
	
		//do an attack
		if (Input.GetButtonDown ("Fire1")) {
			//first create a normalized vector so the attack is always equidistantly in front of the player
			Vector2 toadd = new Vector2(facing.x*1.3f, facing.y*1.3f);
			toadd.Normalize();
			Vector3 toadd3 = toadd;
			//create hitbox object
			attackobj = (GameObject)Instantiate(projectile, transform.position + toadd3, transform.rotation);
			//let it do its thang
			StartCoroutine(AttackTime());
		}

	}

	//wait a bit before destroying attack hitbox object
	IEnumerator AttackTime(){
		yield return new WaitForSeconds (.2f);
		GameObject.Destroy(attackobj);
	}

	//enumerator describing states of dash-ability for player
	public enum DashState{
		Ready,
		Dashing,
		Cooldown
	}



}
