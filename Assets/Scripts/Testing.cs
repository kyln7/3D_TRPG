using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TRpgMap;

public class Testing : MonoBehaviour
{
    private string mapDataPath;
    private GridArray mapData;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = GameSystem.GetGameObjectByMouse("Interactable");
        }
    }
}
