using UnityEngine;
using System.Collections;

public class WinOnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			GameManager.Notify(WinEvent, "ooohhh");		
		}
	}

    public bool WinEvent(string something)
    {
        Debug.Log(something);
        return true;
    }
}
