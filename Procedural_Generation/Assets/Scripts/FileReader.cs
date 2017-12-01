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

    public string GetName()
    {
        return name;
    }

    public List<string> GetContent()
    {
        return content;
    }
}

public class FileReader : MonoBehaviour {

    public List<Table> tables;

    void Awake()
    {
        string path = Application.dataPath + "/Tables/";

        string[] all_files = Directory.GetFiles(path, "*.txt");
        tables = new List<Table>();

        foreach (string file in all_files)
        {
            List<string> content = new List<string>();
            StreamReader reader = new StreamReader(file);
            string name = Path.GetFileName(file);
            string line;
            

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

    //Checks if the File exist
    public bool FindFile(string name)
    {
        for(int i = 0; i < tables.Count; i++)
        {
            if (name == tables[i].GetName())
                return true;
        }

        return false;
    }

    //Returns a line from the rolldice thing
    public string RollDice(string name)
    {
        int ind = -1;

        //Loop to get index of the file
        for (int i = 0; i < tables.Count; i++)
        {
            if (name == tables[i].GetName())
            {
                ind = i;
                break;
            }
        }

        if (ind == -1)
            return "";

        int rand_num = Random.Range(0, tables[ind].GetContent().Count - 1);

        return tables[ind].GetContent()[rand_num];
    }
}
