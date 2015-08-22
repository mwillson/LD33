﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.

public class BoardManager : MonoBehaviour {

        public GameObject[] groundTiles;          //Array of floor prefabs.

        private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.

        private int columns = 10;
        private int rows = 10;
		private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
	
      /*  public void InitialiseList ()
        {
            //Clear our list gridPositions.
            gridPositions.Clear ();

            //Loop through x axis (columns).
            for(int x = 0; x < columns; x++)
            {
                //Within each column, loop through y axis (rows).
                for(int y = 0; y < rows; y++)
                {
                    //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                    gridPositions.Add (new Vector3(x, y, 0f));
                }
            }
        } */

//Sets up the outer walls and floor (background) of the game board.
        public void BoardSetup ()
        {
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject ("Board").transform;

            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for(int x = 0; x < columns; x++)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for(int y = 0; y < rows; y++)
                {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    GameObject toInstantiate = groundTiles[0];

                    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                    GameObject instance =
                        Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent (boardHolder);
                }
            }

        }
}