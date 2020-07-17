﻿using System.Collections;
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
            Debug.Log(grid.gridName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = GameSystem.GetGameObjectByMouse("Interactable");
        }
        if(Input.GetAxis("Horizontal") == 1)
        {
            Camera.main.transform.position += Vector3.up;
        }
        if(Input.GetAxis("Horizontal") == -1)
        {
            Camera.main.transform.position += Vector3.down;
        }
        if(Input.GetAxis("Vertical") == 1)
        {
            Camera.main.transform.position += Vector3.right;
        }
        if(Input.GetAxis("Vertical") == -1)
        {
            Camera.main.transform.position += Vector3.left;
        }
    }
}
