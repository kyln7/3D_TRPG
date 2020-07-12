using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TRpgMap;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
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
}
