using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;
using Grid = TRpgMap.Grid;

public class testmove : MonoBehaviour
{
    public GameObject moveObj;
    Vector2 startPos, endPos;

    bool ifTouched = false;

    private GridArray mapData;
    //int i = 0;
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
        //Debug.Log("waiting for press");
        if (Input.GetMouseButtonDown(0))
        {
            ifTouched = true;
            GameObject obj = GameSystem.GetGameObjectByMouse("Ground");
            if (obj == null) return;
            endPos = new Vector2(mapData.GetGridPos(obj)[0], mapData.GetGridPos(obj)[1]);

            Debug.Log("start:" + startPos.ToString());
            Debug.Log("end:" + endPos.ToString());
        }

        if (ifTouched)
        {
            if (AStarMove.GetInstance().Move(moveObj, mapData.gridArray, startPos, endPos, out Vector2 stopPos))
            {
                ifTouched = false;
                startPos = stopPos;
            }
        }
    }
}
