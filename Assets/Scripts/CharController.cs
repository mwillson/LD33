using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	public Vector3 direction;
	public Vector3 savedvelocity;
	public DashState dashState;
	public float speed = 1.75f;
	public float dashTimer;
	public float maxDash = 0.40f;
	public GameObject projectile, softattack, hardattack;
	public Vector2 savedposition;
	Vector3 previouslocation, currentlocation, targetloc;
	Quaternion newrotation;
	Animator anim;
	Vector3 facing;					//direction we are facing
	GameObject attackobj;			//melee attack gameobject that we instantiate
	public Camera maincamera;		//the camera

	public int attackbuffer;		//represents how many frames attack button has been held down

	public string horizcontrol = "Horizontal_P1";
	public string vertcontrol = "Vertical_P1";
	public string attackbutton = "Fire1_P1";
	public string dashbutton = "Trigger_P1";

	// Use this for initialization
	void Start () {
		dashState = DashState.Ready;
		anim = GetComponent<Animator> ();
		maincamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		facing = new Vector3 (1, 0, 0);
	}

	// Update is called once per frame
	void Update () {

		// check buffer for attack
		if (Input.GetButton(attackbutton)) {
			attackbuffer++;
		}
		

		gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
		//transform.position = savedposition;
		direction = new Vector3(Input.GetAxisRaw(horizcontrol), Input.GetAxisRaw(vertcontrol), 0);
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


		//do an attack
		if (Input.GetButtonUp (attackbutton)) {
			//first create a normalized vector so the attack is always equidistantly in front of the player
			Vector2 toadd = new Vector2(facing.x*1.2f, facing.y*1.2f);
			toadd.Normalize();
			Vector3 wheretoattack = toadd;
			wheretoattack = new Vector3(wheretoattack.x*1.2f, wheretoattack.y*1.2f);

			//let it do its thang
			anim.SetBool("Attacking", true);
			StartCoroutine(AttackTime(wheretoattack, attackbuffer));
		}
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

		savedposition = transform.position;

		switch (dashState){
		case DashState.Ready:
			if (Input.GetButtonDown(dashbutton)){
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
		 
		//convert player position from world to screen space
		savedposition = maincamera.WorldToScreenPoint ( savedposition);
		//clamp to within the edges of the screen
		//Debug.Log("ScreenSpace " + gameObject.name + ": " + savedposition.x + ": " + savedposition.y);
		savedposition.x = Mathf.Clamp (savedposition.x, 0f, Screen.width);
		savedposition.y = Mathf.Clamp (savedposition.y, 0f, Screen.height);
		//double check
		//Debug.Log("ScreenSpace " + gameObject.name + ": " + savedposition.x + ": " + savedposition.y);

		//if we are clamped (AKA trying to leave the screen)
		if (savedposition.x == 0f) {
			savedposition = savedposition + new Vector2 (.01f, 0f);
			transform.position = GetWorldPositionOnPlane (savedposition, .3f);
		}else if(savedposition.x == Screen.width){
			savedposition = savedposition + new Vector2 (-.01f, 0f);
			transform.position = GetWorldPositionOnPlane (savedposition, .3f);
		}else if(savedposition.y == 0f){
			savedposition = savedposition + new Vector2 (0f, .01f);
			transform.position = GetWorldPositionOnPlane (savedposition, .3f);
		}else if (savedposition.y == Screen.height) {
			savedposition = savedposition + new Vector2 (0f, -.01f);
			//set actual position to appropriate new position
			transform.position = GetWorldPositionOnPlane (savedposition, .3f);
		}
		
	}

	public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
		Ray ray = Camera.main.ScreenPointToRay(screenPosition);
		Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
		float distance;
		xy.Raycast(ray, out distance);
		return ray.GetPoint(distance);
	}

	//create attack 'box'
	//create a different one depending on buffer amount, which represents how long the
	//player has held down the attack button
	//wait a bit before destroying attack hitbox object
	IEnumerator AttackTime(Vector3 where, int buffertotal){
		//attack buffer reset
		attackbuffer = 0;
		//wait to spawn attack

		//yield return new WaitForSeconds (.3f);
		//create hitbox object
		if (buffertotal > 0 && buffertotal <= 45) {
			attackobj = (GameObject)Instantiate (softattack, transform.position + where, transform.rotation);
		}else if (buffertotal > 45){
			attackobj = (GameObject)Instantiate(hardattack, transform.position + where, transform.rotation);
		}
		attackobj.transform.SetParent (transform);
		//wait the length of the attack
		yield return new WaitForSeconds (.3f);
		//then destroy attack object
		GameObject.Destroy(attackobj);
		//stop attack animation
		anim.SetBool ("Attacking", false);
	}

	//enumerator describing states of dash-ability for player
	public enum DashState{
		Ready,
		Dashing,
		Cooldown
	}



}
