using UnityEngine;
using System.Collections;

public class MovingUnit : MonoBehaviour {
	public LayerMask blockingLayer;
	private BoxCollider2D boxCollider;

	protected void Prepare(){
		boxCollider = gameObject.GetComponent<BoxCollider2D> ();
	}

	protected bool CanMove (int xDir, int yDir, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);
		
		boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, blockingLayer);
		boxCollider.enabled = true;
		
		if (hit.transform == null) {
			return true;
		}
		
		return false;
	}
}
