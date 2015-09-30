using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists.

public class BoardManager : MonoBehaviour {

    public GameObject[] groundTiles;          //Array of floor prefabs.
	public GameObject[] wallTiles;
    private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.
	//a private list of lists of arrays representing possible sections of a map
	private List<List<int[]>> possibles;
    //private int columns = 10;
    private int rows;

	List<int[]> topt = new List<int[]>() {
		new int[]{ 1, 1, 1, 1, 1},
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 0, 0, 0, 0, 0}
	};

	List<int[]> bottomt = new List<int[]>() {
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 1, 1, 1, 1, 1}
	};

	List<int[]> right = new List<int[]>() {
		new int[]{ 0, 0, 0, 0, 1},
		new int[]{ 0, 0, 0, 0, 1},
		new int[]{ 0, 0, 0, 0, 1},
		new int[]{ 0, 0, 0, 0, 1},
		new int[]{ 0, 0, 0, 0, 1}
	};

	List<int[]> left = new List<int[]>() {
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 0, 0, 0, 0}
	};

	List<int[]> bleft = new List<int[]>() {
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 1, 1, 1, 1}
	};
	
	List<int[]> bright = new List<int[]>() {
		new int[]{ 0, 0, 0, 0, 1},
		new int[]{ 0, 0, 0, 0, 1},
		new int[]{ 0, 0, 0, 0, 1},
		new int[]{ 0, 0, 0, 0, 1},
		new int[]{ 1, 1, 1, 1, 1}
	};
	
	List<int[]> tleft = new List<int[]>() {
		new int[]{ 1, 1, 1, 1, 1},
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 0, 0, 0, 0}
	};

	List<int[]> tright = new List<int[]>() {
		new int[]{ 1, 1, 1, 1, 1},
		new int[]{ 0, 0, 0, 0, 1},
		new int[]{ 0, 0, 0, 0, 1},
		new int[]{ 0, 0, 0, 0, 1},
		new int[]{ 0, 0, 0, 0, 1}
	};
	
	List<int[]> rand1 = new List<int[]>() {
		new int[]{ 0, 1, 0, 0, 0},
		new int[]{ 0, 1, 0, 1, 1},
		new int[]{ 0, 1, 0, 0, 1},
		new int[]{ 0, 1, 0, 0, 1},
		new int[]{ 0, 0, 0, 0, 0}
	};
	
	List<int[]> rand4 = new List<int[]>() {
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 1, 1, 1, 0, 0},
		new int[]{ 0, 0, 1, 0, 0},
		new int[]{ 0, 0, 1, 1, 0},
		new int[]{ 0, 0, 0, 0, 0}
	};

	List<int[]> rand5 = new List<int[]>() {
		new int[]{ 1, 1, 1, 0, 0},
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 0, 0, 1, 1, 0},
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 0, 1, 1, 1, 1}
	};
	
	List<int[]> rand2 = new List<int[]>() {
		new int[]{ 1, 0, 0, 0, 0},
		new int[]{ 1, 0, 0, 0, 1},
		new int[]{ 1, 0, 0, 1, 1},
		new int[]{ 0, 0, 1, 1, 0},
		new int[]{ 0, 0, 0, 0, 0}
	};
	
	List<int[]> rand3 = new List<int[]>() {
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 0, 1, 1, 1, 0},
		new int[]{ 0, 0, 0, 0, 0},
		new int[]{ 1, 1, 1, 1, 0},
		new int[]{ 0, 0, 0, 0, 0}
	};

	List<int[]> data = new List<int[]>(){
		new int[]{	1,	1,	1,	1,	1,	1,	1,	1,	1,	1,	1,	1, 	1,	1,	1,	1,	1,	1,	1,	1,	1,	1,	1, 1, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	1,	0,	0,	0, 	0,	0,	0,	0, 	0,	0,	0,	0,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	1,	0,	1,	0,	1,	1,	0,	0,	0,	0, 	0,	0,	0,	0,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	1,	0,	0,	0,	0,	0,	0, 	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	1,	1,	1,	1,	1,	1,	1,	0,	0,	0, 	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	1,	1,	1,	0,	0,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	1,	0,	1,	1,	1,	1,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	0,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	1,	0,	1,	0,	0,	1,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	0,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	1,	0,	1,	1,	0,	1,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	0,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	0,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	1,	1,	1,	1,	1,	1,	1,	1,	1,	1,	1, 	1,	1,	0,	0,	0,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	0,	0,	0,	0,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	1,	0,	1,	0,	0,	1,	1,	1,	1, 	0,	1,	1,	1,	1,	1,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	1,	1,	1,	0,	0,	0,	1,	0,	0, 	0,	1,	0,	0,	0,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	1,	0,	1,	1,	1,	1,	1,	0,	1, 	0,	1,	0,	0,	0,	1,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	1,	0,	0,	0,	0,	0,	0,	0,	1, 	0,	1,	0,	0,	1,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	1, 	0,	1,	0,	0,	0,	0,	0,	1,	0,	1, 0, 1},
		new int[]{	1,	0,	1,	1,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	0,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	1,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	1,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	1,	1,	1,	1,	1,	0,	0,	0,	0,	0, 	0,	1,	0,	1,	1,	1,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	1,	1,	1,	0,	0,	1,	0,	0,	0,	0,	0, 	0,	1,	0,	1,	1,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	1,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	0,	1,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	0,	1,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	1,	1,	0,	0,	0,	1,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	1,	0,	0,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	0,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	1,	0,	0,	1,	1,	1,	0,	0, 	0,	1,	0,	0,	0,	0,	1,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	0,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	1,	0,	0,	0,	0,	0,	0,	0,	1, 	0,	0,	0,	0,	0,	0,	1,	1,	1,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	1,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0,	0, 	0,	1,	0,	0,	1,	0,	0,	0,	0,	0, 0, 1},
		new int[]{	1,	1,	1,	1,	1,	1,	1,	1,	1,	1,	1,	1, 	1,	1,	1,	1,	1,	1,	1,	1,	1,	1,	1, 1, 1},


	};

		private int columns;

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

		public void Start(){
			possibles = new List<List<int[]>>();
			possibles.Add (rand1);
			possibles.Add (rand2);
			possibles.Add (rand3);
			possibles.Add (rand4);
			possibles.Add (rand5);
	}



//Sets up the outer walls and floor (background) of the game board.
        public void BoardSetup ()
        {
			RandomizeData ();
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject ("Board").transform;
			columns = data[0].Length;
			rows =  data.Count; 
            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for(int x = columns-1; x >= 0; x--)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for(int y = 0; y < rows; y++)
                {
				GameObject toInstantiate;
					//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    if (data[y][x] == 0){
						toInstantiate = groundTiles[0];


					} else { // if ( data[y][x] == 1){
						toInstantiate = wallTiles[0];

					} 
				//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
				GameObject instance =
					Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
				
				//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
				instance.transform.SetParent (boardHolder);
                }
            }

        }

		//Randomize the board data given some potential "areas" or "rooms"

		//IMPORTANT QUESTION
		//How can we know if a randomly generated map is navigable to the 'end'?
		void RandomizeData(){
			//x and y represent spaces on our board
			//x is columns, y is rows
			for (int x = 0; x < 5; x++) {
				for (int y = 0; y < 7; y++) {
					//each area is a 5x5 matrix
					//pick a random one
					int num = Random.Range(0, 5);
					List<int[]> whichone = possibles.Find(anarea => possibles.IndexOf(anarea) == num);
					//or if we are on an edge space, use appropriate edge piece
					if(x == 0 && y == 0) whichone = tleft;
					else if (x == 4 && y == 0) whichone = tright;
					else if (x == 0 && y == 6) whichone = bleft;
					else if (x == 4 && y == 6) whichone = bright;
					else if (x == 0) whichone = left;
					else if (x == 4) whichone = right;
					else if (y == 0) whichone = topt;
					else if (y == 6) whichone = bottomt;

					//place values for each space in that particular area
					ValuesForArea(whichone, x, y);		
				}
			}
		}

		void ValuesForArea(List<int[]> list, int x, int y){
			int[] currline;
			for (int i = 0; i < 5; i++) {
				currline = list.Find(someline => list.IndexOf(someline) == i);
				for (int j = 0; j < 5; j++) {
					//currline[j] is the piece of data we are currently placing
					
				//it needs to be placed on the main 'data' array on line 5y + i
					int[] whicharray = data.Find(somearray => data.IndexOf(somearray) == ((5*y)+i) );
				//and column 5x + j
					whicharray[(5*x)+j] = currline[j];
				}
			}
			//all data should be placed, go back up the call stack and continue boardsetup
		}


}
