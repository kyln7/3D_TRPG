using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid = TRpgMap.Grid;
using TRpgMap;

//A*算法移动类
//直接调用Move（）可实现物体的寻路+移动
//SetPoints（）可以改变寻路的起始坐标
public class AStarMove
{
    private static AStarMove instance;

    public bool endMove = false;
    private bool flag2 = false;
    Vector2 end2;

    AStarClass aStar;
    List<Vector2> path;

    //是否已搜索到路径
    public bool ifGetPath = false;
    //目前移动到路径的第几段
    int num = 0;
    Vector3 moveVec = Vector3.zero;

    float speed = 10;

    public AStarMove() { }
    public AStarMove(Grid[,] map) { aStar = new AStarClass(map, Vector2.zero, Vector2.zero, false, false); }
    public AStarMove(Grid[,] map, Vector2 start, Vector2 end) { aStar = new AStarClass(map, start, end, false, false); }

    public static AStarMove GetInstance()
    {
        if (instance == null)
        {
            instance = new AStarMove();
        }
        return instance;
    }

    public void SetPoints(Grid[,] map, Vector2 start, Vector2 end)
    {
        aStar = new AStarClass(map, start, end, false, false);
    }

    public bool Move(GameObject gameObject,GridArray maparray, Vector2 start, Vector2 end, out Vector2 endPos, User user=User.Player)
    {

        
        Grid[,] map = maparray.gridArray;
        if (!ifGetPath)
        {
            SetPoints(map, start, end);
            path = aStar.getPath();
            endMove = false;
            ifGetPath = true;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                flag2 = true;
                GameObject obj = GameSystem.GetGameObjectByMouse("Ground");
                if (obj == null) Debug.Log("NONE");
                end2 = new Vector2(maparray.GetGridPos(obj)[0], maparray.GetGridPos(obj)[1]);
                Debug.Log("now end" + end);
            }

            if (num == path.Count)
            {
                endPos = path[num - 1];
                //初始化配置
                ifGetPath = false;
                num = 0;
                path = null;
                endMove = true;
                return true;
            }

            Status flag = MoveStep(gameObject, user);
            if (flag == Status.Moved)
            {
                num += 1;

                if (flag2)
                {
                    flag2 = false;
                    start = new Vector2(maparray.GetGridPos(gameObject)[0], maparray.GetGridPos(gameObject)[1]);

                    SetPoints(map, start, end2);
                    path = aStar.getPath();
                    //Debug.Log(path.Count);
                    //Debug.Log(start + "   " + end);
                    num = 0;
                    endPos = start;
                }

            }
            else if (flag == Status.Interrupt)
            {
                endPos = path[num - 1];
                //初始化配置
                ifGetPath = false;
                num = 0;
                path = null;
                endMove = true;
                return true;
            }
            Debug.Log(flag);
        }

        endPos = Vector2.zero;
        return false;
    }

    private Status MoveStep(GameObject gameObject, User user=User.Player)
    {
        Debug.Log(path[0] + "   " + path[path.Count-1]);
        Grid targetGrid = aStar.map[(int)path[num].x, (int)path[num].y];
        Vector3 s = gameObject.transform.position;
        Vector3 e = new Vector3(targetGrid.x, s.y, targetGrid.z);

        string BlockLayer = "";
        if (user == User.Player) BlockLayer = "Npc";
        else if (user == User.NPC) BlockLayer = "Interactable";

        if (GameSystem.HasObjectOnGrid(new Vector2Int((int)e.x, (int)e.z), BlockLayer))
        {
            return Status.Interrupt;
        }

        moveVec = e - s;

        //0,0,-2  转90°
        //-2,0,0  转180°
        if (Mathf.Abs(moveVec.x - 2) < 0.05f && moveVec.z < 0.05f) gameObject.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
        if (moveVec.x < 0.05f && Mathf.Abs(moveVec.z + 2) < 0.05f) gameObject.transform.GetChild(0).rotation = Quaternion.Euler(0, 90, 0);
        if (Mathf.Abs(moveVec.x + 2) < 0.05f && moveVec.z < 0.05f) gameObject.transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
        if (moveVec.x < 0.05f && Mathf.Abs(moveVec.z - 2) < 0.05f) gameObject.transform.GetChild(0).rotation = Quaternion.Euler(0, 270, 0);

        if (moveVec.magnitude < 0.05f)
        {
            gameObject.transform.position = e;
            return Status.Moved;
        }
        //Debug.Log("Length: " + moveVec.magnitude.ToString("0.0000"));
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

    public enum User
    {
        Player,
        NPC
    }
}
