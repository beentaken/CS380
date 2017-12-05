using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TileGeneration : MonoBehaviour
{
    private FileReader fm;
    private List<Vector3> unvisited_rooms;
    private List<Vector3> visited_rooms;
    private int totalRooms = 0;
    private int desiredRooms = 25;

    private List<GameObject> allDoors;
    private List<GameObject> allTiles;

    public GameObject tile;
    public GameObject door;
    public GameObject chalice;

    private bool remake;
    private bool step;

    // Use this for initialization
    void Start()
    {
        remake = false;
        step = false;
        fm = this.GetComponent<FileReader>();
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
    void Update()
    {
        if (remake)
        {
            unvisited_rooms = new List<Vector3>();
            visited_rooms = new List<Vector3>();
            allDoors = new List<GameObject>();
            allTiles = new List<GameObject>();
            totalRooms = 0;

            Vector3 tile_pos = new Vector3(0.0f, 0.0f, 0.0f);

            unvisited_rooms.Add(tile_pos);

            //Loop through the unvisited rooms
            while (unvisited_rooms.Count > 0)
            {
                int num_doors = int.Parse(fm.RollDice("Doors.txt"));

                tile_pos = unvisited_rooms[0];

                //Debug.Log(num_doors);

                //Check if we already visted this room
                if (visited_rooms.Contains(tile_pos))
                {
                    unvisited_rooms.RemoveAt(0);
                    continue;
                }

                allTiles.Add(Instantiate(tile, tile_pos, new Quaternion()));
                totalRooms++;
                visited_rooms.Add(unvisited_rooms[0]);
                unvisited_rooms.RemoveAt(0);

                if (totalRooms == desiredRooms)
                    break;

                for (int i = 0; i < num_doors; i++)
                {
                    //StartCoroutine(Wait());

                    //positive y-axis door
                    if (i == 0)
                    {
                        allDoors.Add(Instantiate(door, new Vector3(tile_pos.x, tile_pos.y + 1.0f, 0.0f), new Quaternion(0, 0, 90, 90)));
                        unvisited_rooms.Add(new Vector3(tile_pos.x, tile_pos.y + 2.0f, 0.0f));
                    }
                    //positive x-axis door
                    if (i == 1)
                    {
                        allDoors.Add(Instantiate(door, new Vector3(tile_pos.x + 1.0f, tile_pos.y, 0.0f), new Quaternion(0, 0, 0, 0)));
                        unvisited_rooms.Add(new Vector3(tile_pos.x + 2.0f, tile_pos.y, 0.0f));
                    }
                    //negative y-axis door
                    if (i == 2)
                    {
                        allDoors.Add(Instantiate(door, new Vector3(tile_pos.x, tile_pos.y - 1.0f, 0.0f), new Quaternion(0, 0, 90, 90)));
                        unvisited_rooms.Add(new Vector3(tile_pos.x, tile_pos.y - 2.0f, 0.0f));
                    }
                    //negative x-axis door
                    if (i == 3)
                    {
                        allDoors.Add(Instantiate(door, new Vector3(tile_pos.x - 1.0f, tile_pos.y, 0.0f), new Quaternion(0, 0, 0, 0)));
                        unvisited_rooms.Add(new Vector3(tile_pos.x - 2.0f, tile_pos.y, 0.0f));
                    }
                }
            }

            int doorsCount = allDoors.Count;

            //Destroys unused doors
            for (int i = 0; i < doorsCount; i++)
            {
                ContactFilter2D filter = new ContactFilter2D();
                Collider2D[] colliderArray = new Collider2D[100];
                int collisions = 0;

                filter.SetDepth(-10, 10);

                collisions = allDoors[i].GetComponent<Collider2D>().OverlapCollider(filter, colliderArray);

                if (collisions != 2)
                {
                    DestroyObject(allDoors[i]);
                    //doorsCount--;
                }
            }


            //Sets rooms with 1 door to special rooms
            for (int i = 0; i < allTiles.Count; i++)
            {
                ContactFilter2D filter = new ContactFilter2D();
                Collider2D[] colliderArray = new Collider2D[100];
                int collisions = 0, doorCollisions = 0;

                filter.SetDepth(-1, 1);

                collisions = allTiles[i].GetComponent<Collider2D>().OverlapCollider(filter, colliderArray);

                for (int j = 0; j < collisions; j++)
                {
                    if (colliderArray[j].gameObject.name == "Door(Clone)")
                        doorCollisions++;
                }

                if (doorCollisions == 1)
                {
                    string file_name = "1.txt";

                    while (fm.FindFile(file_name))
                    {
                        string content = fm.RollDice(file_name);

                        if (content == "Trap")                                                          //ROOM TYPES
                            allTiles[i].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                        else if (content == "Item")
                            allTiles[i].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                        else if (content == "Shop")
                            allTiles[i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
                        else if (content == "Boss")
                            allTiles[i].GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
                        else                                                                            //ITEMS
                            Instantiate(Resources.Load(content), allTiles[i].transform);
                        
                        file_name = content + ".txt";
                    }
                }
                /*
                else if(doorCollisions == 2)
                {
                    string file_name = "2.txt";
                }
                else if (doorCollisions == 3)
                {

                    string file_name = "3.txt";
                }
                else if (doorCollisions == 4)
                {

                    string file_name = "4.txt";
                }*/
            }

            remake = false;
        }

    }

    public void Size()
    {
        if (desiredRooms == 25)
            desiredRooms = 10;
        else if (desiredRooms == 10)
            desiredRooms = 40;
        else if (desiredRooms == 40)
            desiredRooms = 25;
    }

    public void Remake()
    {
        foreach(GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            if (o.name == "Door(Clone)")
                Destroy(o);
            else if (o.name == "Tile(Clone)")
                Destroy(o);
        }
        remake = true;
    }

    public void Step()
    {
        if (step == false)
            step = true;
        else
            step = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
    }
}
