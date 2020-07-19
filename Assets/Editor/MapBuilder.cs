using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TRpgMap;

public class MapBuilder : EditorWindow
{
    //mapDataList path
    string mapDataListPath;
    //gridMapData Path
    string GridMapDataPath;
    //The ref of the dataList
    MapDataList mapDataList;
    //The ref of the GridMapData
    GridArray gridArray;
    //GridMap StartPos obj
    GameObject startPos;
    //GridMap EndPos obj
    GameObject endPos;
    //Map in the Editor Hierarchy
    GameObject map;
    //Choosed MapNumber
    int numOfTheMap = 0;
    //MapCounts
    int[] mapCounts;
    //Name of each map
    string[] mapNames;
    //The debug Info of Buttons
    string debugText = "";
    MessageType messageType = MessageType.None;

    [MenuItem("TRPG/MapBuilder")]
    static void Init()
    {
        //Create new Window
        MapBuilder window = (MapBuilder)GetWindow(typeof(MapBuilder));
    }
    private void Awake()
    {
        mapDataList = new MapDataList();
        try
        {
            map = GameObject.Find("Map");
            startPos = GameObject.Find("MapStartPos");
            endPos = GameObject.Find("MapEndPos");
            gridArray = new GridArray(startPos.transform.position, endPos.transform.position);
        }
        catch (Exception e)
        {
            messageType = MessageType.Warning;
            debugText = "Map or startPos OR endPos not Found!\n" + e.Message;
        }
        try
        {
            GridMapDataPath = Application.dataPath + "\\Data\\Json\\" + SceneManager.GetActiveScene().name + ".txt";
            mapDataListPath = Application.dataPath + "\\Data\\Json\\MapData.txt";
            string mdl = File.ReadAllText(mapDataListPath);
            mapDataList = JsonUtility.FromJson<MapDataList>(mdl);
            string gmd = File.ReadAllText(mapDataListPath);
            gridArray = JsonUtility.FromJson<GridArray>(gmd);
        }
        catch (Exception e)
        {
            messageType = MessageType.Error;
            debugText = "Prefab not Found!\n" + e.Message;
        }
    }
    private void Update()
    {
        //Update mapDataList info in the window
        if (mapDataList != null)
        {
            mapCounts = new int[mapDataList.mapDataList.Count];
            mapNames = new string[mapDataList.mapDataList.Count];
            for (int i = 0; i < mapDataList.mapDataList.Count; i++)
            {
                mapNames[i] = mapDataList.mapDataList[i].name;
                mapCounts[i] = i;
            }
            //numOfTheMap = 0;
        }
        else
        {
            mapCounts = new int[0];
            mapNames = new string[0];
            numOfTheMap = -1;
        }
    }

    private void OnGUI()
    {
        //Select Map
        Rect selectRect = (Rect)EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Select Map");
        numOfTheMap = EditorGUILayout.IntPopup(numOfTheMap, mapNames, mapCounts);
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(10f);
        //Get map in editor
        map = EditorGUILayout.ObjectField("Map In Editor Hierarchy", map, typeof(GameObject), true) as GameObject;
        GUILayout.Space(10f);
        //Select StartPos and EndPos
        Rect startRect = (Rect)EditorGUILayout.BeginHorizontal();
        startPos = EditorGUILayout.ObjectField("Start Position", startPos, typeof(GameObject), true) as GameObject;
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(10f);
        Rect endRect = (Rect)EditorGUILayout.BeginHorizontal();
        endPos = EditorGUILayout.ObjectField("End Position", endPos, typeof(GameObject), true) as GameObject;
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(10f);
        //Main Area
        Rect mainRect = (Rect)EditorGUILayout.BeginVertical();
        //Button Rect Area
        Rect buttonRect = (Rect)EditorGUILayout.BeginHorizontal();
        //Add Map Button
        Rect addButtonRect = (Rect)EditorGUILayout.BeginHorizontal("Button", GUILayout.Height(30f));
        if (GUI.Button(addButtonRect, GUIContent.none))
        {
            AddMap();
        }
        GUILayout.Label("Add Map");
        EditorGUILayout.EndHorizontal();
        //End Add Map
        //Save Button
        Rect saveButtonRect = (Rect)EditorGUILayout.BeginHorizontal("Button", GUILayout.Height(30f));
        if (GUI.Button(saveButtonRect, GUIContent.none))
        {
            //TODO
            SaveData();
        }
        GUILayout.Label("Save Map");
        EditorGUILayout.EndHorizontal();
        //End Save
        //Load Button
        Rect loadButtonRect = (Rect)EditorGUILayout.BeginHorizontal("Button", GUILayout.Height(30f));
        if (GUI.Button(loadButtonRect, GUIContent.none))
        {
            //TODO
            LoadData();
        }
        GUILayout.Label("Load Map");
        EditorGUILayout.EndHorizontal();
        //End Load
        EditorGUILayout.EndHorizontal();
        //End Button Rect Area
        //Debug Info Area
        Rect debugInfoRect = (Rect)EditorGUILayout.BeginHorizontal("Label", GUILayout.Height(60f));
        EditorGUILayout.HelpBox(debugText, messageType, true);
        EditorGUILayout.EndHorizontal();
        //End Debug Info Area
        EditorGUILayout.EndVertical();
        //End Main Area
    }

    //save map data
    public void SaveData()
    {
        GridMapDataPath = Application.dataPath + "\\Data\\Json\\" + SceneManager.GetActiveScene().name + ".txt";
        mapDataListPath = Application.dataPath + "\\Data\\Json\\MapData.txt";
        if (!File.Exists(mapDataListPath))
        {
            FileStream fs = File.Create(mapDataListPath);
            fs.Close();
        }
        if (!File.Exists(GridMapDataPath))
        {
            FileStream fs = File.Create(GridMapDataPath);
            fs.Close();
        }
        if (map != null && mapDataList != null)
        {
            if (map.name != "Map")
            {
                messageType = MessageType.Warning;
                debugText = "Map not set correctly";
                return;
            }
            if (mapDataList.mapDataList.Count == 0)
            {
                messageType = MessageType.Warning;
                debugText = "No map Data,Please Add new one";
                return;
            }
            mapDataList.mapDataList[numOfTheMap].gridDataList.Clear();
            if (mapDataList.mapDataList[numOfTheMap] == null)
            {
                AddMap();
            }
            if (mapDataList.mapDataList[numOfTheMap].name != SceneManager.GetActiveScene().name)
            {
                messageType = MessageType.Warning;
                debugText = "Choose The Wrong Scene";
                return;
            }
            gridArray = new GridArray(startPos.transform.position, endPos.transform.position);
            foreach (Transform gridTransform in map.transform)
            {
                mapDataList.mapDataList[numOfTheMap].gridDataList.Add(
                new TRpgMap.Grid(GetGridX(gridTransform), GetGridZ(gridTransform), GetGridY(gridTransform), true, gridTransform.name.Replace("(Clone)", "")));
                int x = GetGridX(gridTransform) - (int)startPos.transform.position.x / TRpgMap.Grid.cellSizeXZ;
                int z = GetGridZ(gridTransform) - (int)startPos.transform.position.z / TRpgMap.Grid.cellSizeXZ;
                int height = GetGridY(gridTransform);
                string gridName = gridTransform.name;
                Debug.Log("Name : " + gridName + ",Pos : X = " + x + " , Z = " + z);
                gridArray.gridArray[x, z] = new TRpgMap.Grid(x, z, height, true, gridName);
            }
            //Debug.Log(gridArray.Width + " , " + gridArray.Height);
            string mdl = JsonUtility.ToJson(mapDataList);
            SaveSystem.SaveMap(mapDataListPath, mdl);
            SaveSystem.SaveMapData(gridArray);
            messageType = MessageType.None;
            debugText = "Map Saved";
        }
        else
        {
            messageType = MessageType.Warning;
            debugText = "Map or mapDataList Asset not Set correctly";
            return;
        }
    }

    //load map data
    public void LoadData()
    {
        mapDataListPath = Application.dataPath + "\\Data\\Json\\MapData.txt";
        string json = File.ReadAllText(mapDataListPath);
        mapDataList = JsonUtility.FromJson<MapDataList>(json);
        if (map != null && mapDataList != null)
        {
            if (map.name != "Map")
            {
                messageType = MessageType.Warning;
                debugText = "Map not set correctly";
                return;
            }
            if (mapDataList.mapDataList.Count == 0)
            {
                messageType = MessageType.Warning;
                debugText = "No map Data,Please Add new one";
                return;
            }
            if (mapDataList.mapDataList[numOfTheMap].name != SceneManager.GetActiveScene().name)
            {
                messageType = MessageType.Warning;
                debugText = "Choose The Wrong Scene";
                return;
            }
            for (int i = map.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(map.transform.GetChild(i).gameObject);
            }
            foreach (var data in mapDataList.mapDataList[numOfTheMap].gridDataList)
            {
                try
                {
                    //GameObject obj = Resources.Load("Prefabs/Map/Cube") as GameObject;
                    GameObject clone = (GameObject)Instantiate(Resources.Load("Map/" + data.gridName, typeof(GameObject)), data.GetPos(), new Quaternion(), map.transform);
                    clone.name = data.gridName;
                }
                catch (Exception e)
                {
                    messageType = MessageType.Error;
                    debugText = "Prefab not Found!\n" + e.Message;
                    break;
                }
                //TODO
            }
            messageType = MessageType.None;
            debugText = "Map Loaded";
        }
        else
        {
            messageType = MessageType.Warning;
            debugText = "Map or mapDataList Asset not Set correctly";
            return;
        }
    }

    //add a new map to the mapDataList
    public void AddMap()
    {
        foreach (var map in mapDataList.mapDataList)
        {
            if (map.name == SceneManager.GetActiveScene().name)
            {
                messageType = MessageType.Error;
                debugText = "Map Already in mapDataList";
                return;
            }
        }
        mapDataList.mapDataList.Add(new TRpgMap.MapData(SceneManager.GetActiveScene().name));
        numOfTheMap = mapDataList.mapDataList.Count - 1;
        messageType = MessageType.None;
        debugText = "Map Added";
    }

    //return the normailized x value of the gameobject
    public int GetGridX(Transform obj)
    {
        return (int)obj.position.x / TRpgMap.Grid.cellSizeXZ;
    }
    //return the normailized z value of the gameobject
    public int GetGridZ(Transform obj)
    {
        return (int)obj.position.z / TRpgMap.Grid.cellSizeXZ;
    }
    //return the normailized y value of the gameobject
    public int GetGridY(Transform obj)
    {
        return (int)obj.position.y / TRpgMap.Grid.cellSizeY;
    }

    [MenuItem("TRPG/CreateGround")]
    static void CreateGround()
    {
        Transform startPos, endPos, map;
        startPos = GameObject.Find("MapStartPos").transform;
        endPos = GameObject.Find("MapEndPos").transform;
        map = GameObject.Find("Map").transform;
        if (startPos == null || endPos == null || map == null)
        {
            Debug.Log("StartPos OR EndPos OR Map Not Found");
            return;
        }
        for (int i = map.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(map.transform.GetChild(i).gameObject);
        }
        for (int x = (int)startPos.position.x; x <= (int)endPos.position.x; x += 2)
        {
            for (int z = (int)startPos.position.z; z <= (int)endPos.position.z; z += 2)
            {
                Instantiate(Resources.Load("Map/Ground1", typeof(GameObject)), new Vector3(x, 0, z), new Quaternion(), map);
            }
        }
    }
}
