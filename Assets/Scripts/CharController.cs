using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	public Vector3 direction;
	public Vector3 savedvelocity;
	public DashState dashState;
	public float speed = 3.5f;
	public float dashTimer;
	public float maxDash = 0.40f;
	public GameObject projectile;
	public Vector2 savedposition;
	Vector3 previouslocation, currentlocation, targetloc;
	Quaternion newrotation;

	// Use this for initialization
	void Start () {
		dashState = DashState.Ready;
	}

	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
		//transform.position = savedposition;
		direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		//base.CanMove(Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw("Vertical"), out hit);
		//Debug.Log (Input.GetAxisRaw ("Horizontal"));

		targetloc = transform.position + direction;
		newrotation = Quaternion.LookRotation(targetloc-transform.position, Vector3.forward);

		//do some rotation for movement
		
		if(direction != new Vector3(0,0,0)){
			
			
			Debug.Log (targetloc-transform.position);
			newrotation.x = 0;
			newrotation.y = 0;

			transform.GetChild(0).rotation = newrotation*Quaternion.Euler(0,0,270);
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

			transform.Translate(direction * 1.75f * speed * Time.deltaTime);

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
	
		if (Input.GetButtonDown ("Fire1")) {
			Vector2 shotdirection = new Vector2(Input.GetAxis("RightStickH"), Input.GetAxis("RightStickV"));
			GameObject projectileobj = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
			//if you're not pointing in a direction
			//shoot forward the way you are moving
			if(shotdirection == new Vector2(0,0)){
				if(direction == new Vector3(0,0,0)){
					projectileobj.GetComponent<Rigidbody2D>().velocity = new Vector2(10,0);
				}else {
					projectileobj.GetComponent<Rigidbody2D>().velocity = direction * 10;
				}
			}
			//else shoot in the direction you are holding
			else {
				projectileobj.GetComponent<Rigidbody2D>().velocity = shotdirection * 10;
			}
		}
	}

	public enum DashState{
		Ready,
		Dashing,
		Cooldown
	}

	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == GameObject.FindGameObjectWithTag ("KnightAttack")) {
			GameManager.Notify (Config.LOSE_NOTIFICATION);
		}
	}

}
