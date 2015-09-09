using UnityEngine;
using System.Collections;

public class DeathAnimationScript : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Something")) {
			anim.SetBool("Done", true);
		}
		if (anim.GetBool ("Done") == true) {
			Application.LoadLevel("mainmenu");
		}
	}
}
