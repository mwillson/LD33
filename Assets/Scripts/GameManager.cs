using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public BoardManager boardScript;
	public static GameManager instance = null;

	private string currentState;

	private static Queue notifications = new Queue();

    //send a event notification to this game manager
    //an event is currently 
	public static void Notify(System.Func<string, bool> methodName, string theParam){
        
        notifications.Enqueue(methodName);
        notifications.Enqueue(theParam);

    }


    void Awake() {
		currentState = Config.STARTED;
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
            System.Func<string, bool> theMethod = (System.Func<string, bool>)notifications.Dequeue();
			string s = (string)notifications.Dequeue ();

            bool retval = theMethod(s);

			/* WON THE GAME*/
			if (s == Config.WIN_NOTIFICATION && currentState != Config.WON){
				//Debug.Log ("FEKKING WON -- not sure what to do lol");
				currentState = Config.WON;

				Application.LoadLevel("death1");

			}else if(s == Config.LOSE_NOTIFICATION){
				Debug.Log ("You Dead");
				Application.LoadLevel("mainmenu");
			}
		}
	}
}
