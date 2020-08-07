using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TRpgMap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

public class SaveSystem
{
    public static void SaveMap(string path, string json)
    {
        StreamWriter stream = new StreamWriter(path);
        stream.Write(json);
        stream.Close();
    }

    public static void SaveMapData(GridArray gridArray)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "\\Data\\Json\\" + SceneManager.GetActiveScene().name + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, gridArray);
        stream.Close();
    }

    public static GridArray LoadMapData()
    {
        string path = Application.dataPath + "\\Data\\Json\\" + SceneManager.GetActiveScene().name + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GridArray data = formatter.Deserialize(stream) as GridArray;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("No Data Path");
        }
        return null;
    }

    public static Dialogue LoadDialogue(string npcName, string DialogueName)
    {
        string path = Application.dataPath + "\\Data\\Dialogue\\" + npcName + "\\" + DialogueName + ".xml";
        Dialogue res = new Dialogue();
        if (File.Exists(path))
        {
            XmlSerializer reader = new XmlSerializer(typeof(Dialogue));
            FileStream stream = new FileStream(path, FileMode.Open);
            res = reader.Deserialize(stream) as Dialogue;
            stream.Close();
        }
        else{
            XmlSerializer writer = new XmlSerializer(typeof(Dialogue));
            FileStream stream = new FileStream(path, FileMode.Create);
            writer.Serialize(stream,res);
            stream.Close();
        }
        return res;
    }
}
