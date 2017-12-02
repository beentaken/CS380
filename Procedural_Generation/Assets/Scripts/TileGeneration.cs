using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour {

    private FileReader fm;
    private List<Vector3> unvisited_rooms;
    private List<Vector3> visited_rooms;

    public GameObject tile;
    public GameObject door;

	// Use this for initialization
	void Start () {
        fm = this.GetComponent<FileReader>();
        unvisited_rooms = new List<Vector3>();
        visited_rooms = new List<Vector3>();
        Vector3 tile_pos = new Vector3(0.0f, 0.0f, 0.0f);

        unvisited_rooms.Add(tile_pos);
        
        //Debug.Log(fm.RollDice("Doors.txt"));

        //Loop through the unvisited rooms
        while(unvisited_rooms.Count > 0)
        {
            int num_doors = int.Parse(fm.RollDice("Doors.txt"));
            tile_pos = unvisited_rooms[0];

            Debug.Log(num_doors);

            //Check if we already visted this room
            if (visited_rooms.Contains(tile_pos))
            {
                unvisited_rooms.RemoveAt(0);
                continue;
            }

            Instantiate(tile, tile_pos, new Quaternion());
            visited_rooms.Add(unvisited_rooms[0]);
            unvisited_rooms.RemoveAt(0);

            for (int i = 0; i < num_doors; i++)
            {
                //positive y-axis door
                if (i == 0)
                {
                    Instantiate(door, new Vector3(tile_pos.x, tile_pos.y + 1.0f, 0.0f), new Quaternion(0, 0, 90, 90));
                    unvisited_rooms.Add(new Vector3(tile_pos.x, tile_pos.y + 2.0f, 0.0f));
                }
                //positive x-axis door
                if (i == 1)
                {
                    Instantiate(door, new Vector3(tile_pos.x + 1.0f, tile_pos.y, 0.0f), new Quaternion(0, 0, 0, 0));
                    unvisited_rooms.Add(new Vector3(tile_pos.x + 2.0f, tile_pos.y, 0.0f));
                }
                //negative y-axis door
                if (i == 2)
                {
                    Instantiate(door, new Vector3(tile_pos.x, tile_pos.y - 1.0f, 0.0f), new Quaternion(0, 0, 90, 90));
                    unvisited_rooms.Add(new Vector3(tile_pos.x, tile_pos.y - 2.0f, 0.0f));
                }
                //negative x-axis door
                if (i == 3)
                {
                    Instantiate(door, new Vector3(tile_pos.x - 1.0f, tile_pos.y, 0.0f), new Quaternion(0, 0, 0, 0));
                    unvisited_rooms.Add(new Vector3(tile_pos.x - 2.0f, tile_pos.y, 0.0f));
                }
            }

            if (num_doors == 4)
                break;

            /*
            int num_doors = int.Parse(fm.RollDice("Doors.txt"));

            Debug.Log(num_doors);

            
            

            //If the file exist, roll dice
            
            while (fm.FindFile(file_name))
            {

                
            }*/
        }

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
