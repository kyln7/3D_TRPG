﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;
using Grid = TRpgMap.Grid;

public class testmove : MonoBehaviour
{
    public GameObject moveObj;
    Vector2 startPos, endPos;

    private GridArray mapData;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        mapData = SaveSystem.LoadMapData();
        startPos = new Vector2(mapData.GetGridPos(moveObj)[0], mapData.GetGridPos(moveObj)[1]);
        Debug.Log(startPos.x + " , " + startPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = GameSystem.GetGameObjectByMouse("Ground");
            if (obj == null) return;
            endPos = new Vector2(mapData.GetGridPos(obj)[0], mapData.GetGridPos(obj)[1]);

            AStarClass aStar = new AStarClass(mapData.gridArray, startPos, endPos, false, false);
            List<Vector2> path = aStar.getPath();
            Debug.Log("start:" + aStar.startPos.ToString());
            Debug.Log("end:" + aStar.endPos.ToString());
            
            StartCoroutine(timer(path));
        }
    }

    IEnumerator timer(List<Vector2> path)
    {
        while (i < path.Count)
        {
            yield return new WaitForSeconds(0.1f);

            Grid pos = mapData.gridArray[(int)path[i].x, (int)path[i].y];
            moveObj.transform.position = new Vector3(pos.x, 3, pos.z);
            i += 1;
        }
        startPos = endPos;
        i = 0;
        Debug.Log("success moving");
    }
}
