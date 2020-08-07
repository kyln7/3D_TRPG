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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    int i = 0;
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        mapData = SaveSystem.LoadMapData();
        startPos = new Vector2(mapData.GetGridPos(moveObj)[0], mapData.GetGridPos(moveObj)[1]);

        //moveObj.transform.position = new Vector3(startPos.x, 2, startPos.y);
        Debug.Log(startPos.x + " , " + startPos.y);

        //moveObj.transform.SetParent(GameObject.Find())
    }

    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("waiting for press");
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        if (Input.GetMouseButtonDown(0))
        {
            ifTouched = true;
            GameObject obj = GameSystem.GetGameObjectByMouse("Interactable");
            if (obj == null) return;
            endPos = new Vector2(mapData.GetGridPos(obj)[0], mapData.GetGridPos(obj)[1]);


            

            //AStarClass aStar = new AStarClass(mapData.gridArray, startPos, endPos, false, false);
            //List<Vector2> path = aStar.getPath();
=======
=======
>>>>>>> Stashed changes
        if (!ifTouched && Input.GetMouseButtonDown(0))
        {
            Debug.Log("buttondown");
            ifTouched = true;
            GameObject obj = GameSystem.GetGameObjectByMouse("Ground");
            if (obj == null) return;
            endPos = new Vector2(mapData.GetGridPos(obj)[0], mapData.GetGridPos(obj)[1]);

<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
            Debug.Log("start:" + startPos.ToString());
            Debug.Log("end:" + endPos.ToString());

            //StartCoroutine(timer(path));
        }

        if (ifTouched)
        {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            if (AStarMove.GetInstance().Move(moveObj, mapData.gridArray, startPos, endPos))
            {
                ifTouched = false;
                startPos = endPos;
            }
        }
    }
    /*
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
    }*/


=======
=======
>>>>>>> Stashed changes
            if (AStarMove.GetInstance().Move(moveObj, mapData.gridArray, startPos, endPos, out startPos))
            {
                ifTouched = false;
                //startPos = endPos;
            }
        }
    }
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
}
