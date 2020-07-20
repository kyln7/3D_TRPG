using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using TRpgMap;

public class Testing : MonoBehaviour
{
    private string mapDataPath;
    private GridArray mapData;
    public Text T;
    // Start is called before the first frame update
    void Start()
    {
        mapData = SaveSystem.LoadMapData();
        if (mapData == null)
        {
            Debug.Log("mapData Not Found");
        }
        foreach (var grid in mapData.gridArray)
        {
            Debug.Log(grid.ToString());
        }
        Dialogue dialogue = SaveSystem.LoadDialogue("Test","test");
        foreach(string text in dialogue.sentences)
        {
            Debug.Log(text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        T.text = TimeSystem.GetTime();
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = GameSystem.GetGameObjectByMouse("Interactable");
        }
    }
}
