﻿/* Hassan Shaikley
 * This script makes a unit want to attack the player
 * When it is in the line of sight
 */

using UnityEngine;
using System.Collections;

public class EnemyComponent : MonoBehaviour {

	public LayerMask PlayerLayer;
	string positionFacing = "down";
	private EnemyController movementcontroller;

	private BoxCollider2D boxCollider;

	LineRenderer lineRenderer;
	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
		lineRenderer = GetComponent<LineRenderer> ();
		movementcontroller = GetComponent<EnemyController> ();
		lineRenderer.material = new Material (Shader.Find("Particles/Additive"));
		Color c1 = Color.white;

		Color c2 = Color.white;
		c1.a = .3f;
		c2.a = .3f;

		lineRenderer.SetColors(c1, c2);

	
	}
	
	// Update is called once per frame
	void Update () {

		//see if player is in raycast
		if (PlayerInRaycast ()) {
			//then attack the player
			movementcontroller.SetMoveState("Follow");
		}
	} 

	bool PlayerInRaycast(){
		Vector3 start = transform.position;

		//this should be based on the posiiton it is facing
		Vector3 end = start + (Vector3.Normalize(movementcontroller.GetWaypoint ()) * 3);


		// remove itself from box collider
		boxCollider.enabled = false;
		RaycastHit2D hit = Physics2D.Linecast (start, end, PlayerLayer);
		boxCollider.enabled = true;
//		Debug.Log (transform.position);
	//	Debug.Log (start + " " + end);

		start.z = -0.1f;
		end.z = -0.1f;
		lineRenderer.SetPosition (0, start);
		lineRenderer.SetPosition (1, end);
		lineRenderer.sortingLayerName = "Effects";

		//	Debug.DrawRay (start, end, Color.green, 1, false);
//		Debug.Log (hit.transform);
		//didnt hit anything
		if (hit.transform == null)
			return false;
		else { // hit player

			return true;
		}
	}
}
