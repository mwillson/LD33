using UnityEngine;
using System.Collections;

public class CharController : MovingUnit {

	public Vector3 direction;
	public Vector3 savedvelocity;
	public DashState dashState;
	public float speed = 5.0f;
	public float dashTimer;
	public float maxDash = 0.40f;

	// Use this for initialization
	void Start () {
		dashState = DashState.Ready;
		base.Prepare ();
	}

	// Update is called once per frame
	void Update () {
		direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		RaycastHit2D hit;
	//	Debug.Log (Input.GetAxisRaw ("Horizontal") + " --" +Input.GetAxisRaw ("Horizontal").GetType() );
		bool canMove = base.CanMove ( (int) Input.GetAxisRaw ("Horizontal"), (int)Input.GetAxisRaw ("Vertical"), out hit);//

		if (!canMove) {
			return;
		}

		//base.CanMove(Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw("Vertical"), out hit);
		//Debug.Log (Input.GetAxisRaw ("Horizontal"));
		switch (dashState){
		case DashState.Ready:

			transform.Translate(direction * speed * Time.deltaTime);
			break;
		case DashState.Dashing:
			transform.Translate(direction * 2 * speed * Time.deltaTime);
			break;
		case DashState.Cooldown:
			transform.Translate(0, 0, 0);
			break;
		}
	}

	void FixedUpdate () {
		switch (dashState){
		case DashState.Ready:
			if (Input.GetButtonDown("Trigger")){
				dashState = DashState.Dashing;
			}
			break;
		case DashState.Dashing:
			dashTimer += Time.deltaTime;
			if(dashTimer >= maxDash){
				dashTimer = maxDash;
				dashState = DashState.Cooldown;
			}
			break;
		case DashState.Cooldown:
			dashTimer -= Time.deltaTime;
			if(dashTimer <= -.3f){
				dashTimer = 0;
				dashState = DashState.Ready;
			}
			break;
		}
	}

	public enum DashState{
		Ready,
		Dashing,
		Cooldown
	}
}
