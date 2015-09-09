using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	GameObject currentitem, nextitem;
	LinkedList<GameObject> items;
	bool axisinuse;

	// Use this for initialization
	void Start () {
		items = new LinkedList<GameObject> ();
		foreach (Transform child in GetComponent<RectTransform>()) {
			items.AddLast (child.gameObject);
		}
		currentitem = items.First.Value;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		if (Input.GetAxisRaw ("Vertical") == -1) {
			if(!axisinuse){
				axisinuse = true;
				MoveDown();
			}
		} else if (Input.GetAxisRaw ("Vertical") == 1) {
			if(!axisinuse){
				axisinuse = true;
				MoveUp();
			}
		} else {
			axisinuse = false;
		}

		if (Input.GetButtonDown ("Fire1")) {
			HandleSelection(currentitem.name);
		}
	}

	void MoveDown() {
		DeHighlight (currentitem);
		if(items.Find(currentitem) == items.Last){
			nextitem = items.First.Value;
		}else {
			nextitem = items.Find (currentitem).Next.Value;
		}
		Highlight (nextitem);
		currentitem = nextitem;
	}

	void MoveUp() {
		DeHighlight (currentitem);
		if(items.Find(currentitem) == items.First) {
			nextitem = items.Last.Value;
		}else {
			nextitem = items.Find (currentitem).Previous.Value;
		}
		Highlight (nextitem);
		currentitem = nextitem;
	}


	//do stuff depending on what selection was made
	void HandleSelection(string name) {
		if (name == "Play") {
			Application.LoadLevel ("level1");
		} else {
			Debug.Log ("Unknown Selection");
		}
	}

	void DeHighlight(GameObject obj) {
		obj.GetComponent<Text> ().color = Color.gray;
		Debug.Log ("DeHighlighting " + obj.name);
	}

	void Highlight(GameObject obj) {
		obj.GetComponent<Text> ().color = Color.white;
		Debug.Log (obj.name);
	}

}
