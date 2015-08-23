using UnityEngine;
using System.Collections;

public class MovingUnit : MonoBehaviour {
	public LayerMask blockingLayer;
	public LayerMask princessLayer;

	private BoxCollider2D boxCollider;

	protected void Prepare(){
		boxCollider = gameObject.GetComponent<BoxCollider2D> ();
	}

	protected bool CanMove (int xDir, int yDir, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);
		boxCollider.enabled = false;

		/* If this is the player */
		if (this.GetType().Name.Equals ("CharController")) {
			hit = Physics2D.Linecast (start, end, princessLayer);
			if (hit.transform != null){
				GameManager.Notify(Config.WIN_NOTIFICATION);
			}
		} 


		hit = Physics2D.Linecast (start, end, blockingLayer);
		boxCollider.enabled = true;


		if (hit.transform == null) {
			return true;
		}
		
		return false;
	}
}
