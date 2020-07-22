using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;
using Grid = TRpgMap.Grid;

public class testmove : MonoBehaviour
{
    public GameObject moveObj;
    Vector2 startPos,endPos;

    private GridArray mapData;
    int i=0;
    // Start is called before the first frame update
    void Start()
    {
        startPos = Vector2.zero;

        moveObj.transform.position = Vector3.zero;
        moveObj.transform.SetParent(GameObject.Find("Map").transform);
        moveObj.transform.position = new Vector3(startPos.x, 3, startPos.y);

        mapData = SaveSystem.LoadMapData();

        for (int k = 0; k < 20; k++)
        {
            mapData.gridArray[20, k].delay = 1;
            GameObject tmp = Instantiate(moveObj);
            tmp.transform.SetParent(GameObject.Find("Map").transform);
            moveObj.transform.position = new Vector3(mapData.gridArray[20, k].x, 3, mapData.gridArray[20, k].z);
        }
        for (int k = 25; k < 40; k++)
        {
            mapData.gridArray[20, k].delay = 1;
            GameObject tmp = Instantiate(moveObj);
            tmp.transform.SetParent(GameObject.Find("Map").transform);
            moveObj.transform.position = new Vector3(mapData.gridArray[20, k].x, 3, mapData.gridArray[20, k].z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("waiting for press");
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = GameSystem.GetGameObjectByMouse("Interactable");
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
        while(i < path.Count)
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
