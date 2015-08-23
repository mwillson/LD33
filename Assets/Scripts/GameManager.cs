using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public BoardManager boardScript;
	public static GameManager instance = null;

	private static Queue notifications = new Queue();

	public static void Notify(string str){
		notifications.Enqueue(str);
	}


	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		boardScript = GetComponent<BoardManager> ();
	}
	// Use this for initialization
	void Start () {
		boardScript.BoardSetup ();
		//boardScript.InitialiseList ();
	}
	
	// Update is called once per frame
	void Update () {
		if (notifications.Count != 0) {
			string s = (string)notifications.Dequeue ();

			/* WON THE GAME*/
			if (s == Config.WINS){
				Debug.Log ("FEKKING WON");

			}
		}
	}
}
