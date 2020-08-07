using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid = TRpgMap.Grid;

//A*算法移动类
//直接调用Move（）可实现物体的寻路+移动
//SetPoints（）可以改变寻路的起始坐标
public class AStarMove
{
    private static AStarMove instance;

    AStarClass aStar;
    List<Vector2> path;

    //是否已搜索到路径
    private bool ifGetPath = false;
    //目前移动到路径的第几段
    int num = 0;
    Vector3 moveVec = Vector3.zero;

    float speed = 10;

    AStarMove() { }
    AStarMove(Grid[,] map) { aStar = new AStarClass(map, Vector2.zero, Vector2.zero, false, false); }
    AStarMove(Grid[,] map,Vector2 start,Vector2 end) { aStar = new AStarClass(map, start, end, false, false); }

    public static AStarMove GetInstance()
    {
        if (instance == null)
        {
            instance = new AStarMove();
        }
        return instance;
    }

    public void SetPoints(Grid[,]map, Vector2 start, Vector2 end)
    {
        if (aStar == null) { aStar = new AStarClass(map, start, end, false, false); }

        aStar.map = map;
        aStar.startPos = start;
        aStar.endPos = end;
    }

    public bool Move(GameObject gameObject, Grid[,] map, Vector2 start, Vector2 end)
    {
        if (!ifGetPath)
        {
            SetPoints(map, start, end);
            path = aStar.getPath();

            ifGetPath = true;
        }
        else
        {
            if (num == path.Count - 1)
            {
                //初始化配置
                ifGetPath = false;
                num = 0;
                path = null;

                return true;
            }


            if (MoveStep(gameObject) == Status.Moved)
            {
                num += 1;
            }
        }

        return false;
    }

    private Status MoveStep(GameObject gameObject)
    {
        Grid targetGrid = aStar.map[(int)path[num].x, (int)path[num].y];
        Vector3 s = gameObject.transform.position;
        Vector3 e = new Vector3(targetGrid.x, s.y, targetGrid.z);

        if (GameSystem. new Vector2(e.x,e.z))

        moveVec = e - s;
        if (moveVec.magnitude < 0.05f) return Status.Moved;
        Debug.Log("Length: " + moveVec.magnitude.ToString("0.0000"));
        moveVec = moveVec.normalized;

        //Debug.Log("Moving: " + s.ToString("0.0000") + " to " + e.ToString("0.0000"));
        gameObject.transform.Translate(moveVec * Time.deltaTime * speed);

        return Status.Moving;
    }

    enum Status
    {
        Moved,
        Moving,
        Interrupt
    }
}
