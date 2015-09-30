using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MDKBuffer : MonoBehaviour {

	List<string> thebuffer;
	List<string> possiblecommands;
	//represents which frame we are on 
	int frames;
	//whether we have successfully inputted a valid command
	bool success;

	// Use this for initialization
	void Start () {
		thebuffer = new List<string>();
		possiblecommands = new List<string>();
		frames = 0;
		success = false;
	}
	
	// Update is called once per frame
	void Update () {
		//check each command in the list of possibles
		foreach (string somecommand in possiblecommands) {
			//if we have inputted a command and we are within the valid timeframe, set our success flag true
			if(CheckCommand (somecommand, thebuffer) && frames < 120){
				Debug.Log ("valid command!!");
				success = true;
			}
		}
		frames++;

		if (success) {

		}
	}

	//check whether the current buffer contains the given command
	bool CheckCommand(string command, List<string> buffer){
		return false;
	}
}
