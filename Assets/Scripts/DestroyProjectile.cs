using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour {

	Vector2 startpos;

	// Use this for initialization
	void Start () {
		startpos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.x - startpos.x >= 10) || 
		    (transform.position.x - startpos.x <= -10) ||
		    (transform.position.y - startpos.y >= 10) ||
		    (transform.position.y - startpos.y <= -10)) {
			Destroy(this.gameObject);
		}
	}
}
