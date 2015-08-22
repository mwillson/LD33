/* Hassan Shaikley
 * This script makes a unit want to attack the player
 * When it is in the line of sight
 */

using UnityEngine;
using System.Collections;

public class EnemyComponent : MonoBehaviour {

	public LayerMask PlayerLayer;
	string positionFacing = "down";

	private BoxCollider2D boxCollider;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();

	}
	
	// Update is called once per frame
	void Update () {

		//see if player is in raycast
		if (PlayerInRaycast ()) {
			//then attack the player
			Debug.Log("HITHITHIT");
		}
	}

	bool PlayerInRaycast(){
		Vector2 start = transform.position;

		//this should be based on the posiiton it is facing
		Vector3 end = start +  new Vector2(1,1);

		// remove itself from box collider
		boxCollider.enabled = false;
		RaycastHit2D hit = Physics2D.Raycast (start, end, PlayerLayer);
		boxCollider.enabled = true;

		//didnt hit anything
		if (hit.transform == null)
			return false;
		else { // hit player
			return true;
		}
	}
}
