using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public BoardManager boardScript;
	public static GameManager instance = null;

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
	
	}
}
