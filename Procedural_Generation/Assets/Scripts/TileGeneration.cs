using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour {

    private FileReader fm;
    public GameObject tile;

	// Use this for initialization
	void Start () {
        fm = this.GetComponent<FileReader>();

        Instantiate(tile);

        //Debug.Log(fm.RollDice("Doors.txt"));

        /*
         * Make a tile, then do a loop, call fm.FindFile(name), name = name of file, at the start it should be Doors.txt
         *  in loop call fm.RollDice(name)
         *  you will get return of string, which is one of the things in the file
         *  from then we should talk what it should do, I am thinking just a long if statement thing for different words
         *  
         *  you will have to create another loop that says something like, for how many doors create a tile and stuff like that
         */
    }

    // Update is called once per frame
    void Update () {

        
    }
}
