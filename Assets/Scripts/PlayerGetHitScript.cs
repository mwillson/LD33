using UnityEngine;
using System.Collections;

public class PlayerGetHitScript : MonoBehaviour {

	public float softfreezetime;
	Vector3 orig, target;
	bool beinghit;
	Transform theparent;
	Vector3 hitdirection;

	// Use this for initialization
	void Start () {
		beinghit = false;
		theparent = transform.parent;

	}
	
	// Update is called once per frame
	void Update () {
		if (beinghit) {
			GetComponentInChildren<BoxCollider2D>().enabled = false;
			theparent.position = Vector3.MoveTowards (theparent.position, target, Time.deltaTime*3f);
			Debug.Log (theparent.position);
		}
		if (theparent.position == target) {
			beinghit = false;
			GetComponentInChildren<BoxCollider2D>().enabled = true;

			//transform.position -= new Vector3(0f, 0f, 1f);
		}
	}

	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject == GameObject.FindGameObjectWithTag ("KnightAttack")) {
			GameManager.Notify (KillEvent, transform.parent.gameObject.name);
		} else if (other.gameObject == GameObject.FindGameObjectWithTag ("Soft")) {
			Debug.Log ("Soft Attacked!");
			StartCoroutine (FreezePlayer ());
		} else if (other.gameObject == GameObject.FindGameObjectWithTag ("Hard")) {
			Debug.Log ("Hard Attacked");
			StartCoroutine (FreezePlayer ());
			hitdirection = transform.parent.position - other.transform.parent.position;
			//hitdirection = hitdirection;
			orig = theparent.position;
			target = orig + hitdirection;
			//Debug.Log (orig + ", " + target);
			beinghit = true;
			//gameObject.GetComponentInParent<Rigidbody2D>().AddForce(hitdirection);
		} 
	}

	public void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			//Debug.Log ("dash hit!");
			
			// if the other thing is a player and its dashing into you
			if(coll.gameObject.GetComponentInParent<CharController>().dashState == CharController.DashState.Dashing){
				//throw you the opposite direction
				Debug.Log ("dash hit!");

				hitdirection = transform.parent.position - coll.transform.position;
				Debug.Log (hitdirection);
				hitdirection = new Vector3(hitdirection.x*.3f, hitdirection.y*.3f);
				Debug.Log("new: " + hitdirection);
				target = coll.transform.position - hitdirection;
				beinghit = true;
			}
		}
	}


	//freezes control of this players gameobject for a short bit of time
	IEnumerator FreezePlayer(){
		Debug.Log(transform.parent.gameObject.GetComponent<CharController>());
		gameObject.GetComponentInParent<CharController> ().enabled = false;
		yield return new WaitForSeconds(softfreezetime);
		gameObject.GetComponentInParent<CharController> ().enabled = true;
		//safeguard in case other flag reset doesn't get called
		beinghit = false;
	}

    public bool KillEvent(string playerName)
    {
        Debug.Log(playerName + " was killed");
        Application.LoadLevel("level1");
        return true;
    }
}
