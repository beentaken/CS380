using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Table
{
    string name;
    List<string> content;

    public Table()
    {
        name = "";
        content = new List<string>();
    }

    public Table(string _name, List<string> _content)
    {
        name = _name;

        content = new List<string>();

        for (int i = 0; i < _content.Count; i++)
            content.Add(_content[i]);
    }
}

public class FileReader : MonoBehaviour {

    private List<Table> tables;

	// Use this for initialization
	void Start () {
        string path = Application.dataPath + "/Tables/";

        string[] all_files = Directory.GetFiles(path, "*.txt");
        tables = new List<Table>();

        foreach(string file in all_files)
        {
            List<string> content = new List<string>();
            StreamReader reader = new StreamReader(file);
            string name = Path.GetFileName(file);
            string line;

            Debug.Log(name);

            do
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    content.Add(line);
                }
            } while (line != null);
            
            tables.Add(new Table(name, content));
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
