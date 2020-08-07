using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid = TRpgMap.Grid;

///<summary>
///此类为A*算法的实现
///
///输入：
///input_map：寻路的地图
///start:起点
///end:终点
///can_walk_slope:是否允许斜着走
///can_cross_terrain:是否允许跨越地形
///
///调用方法：
///1.创建类的时候输入参数
///2.使用getPath（）获得路径数组
///
/// 输出：
/// result：list<Vector2>数组，包含起点和终点
/// </summary>
public class AStarClass
{
    public Grid[,] map;
    bool canWalkSlope;
    bool canCrossTerrain;

    public Vector2 startPos;
    public Vector2 endPos;
    
    public AStarPoint[,] mPointGrid;
    AStarPoint mStartPoint;
    AStarPoint mEndPoint;

    public AStarClass() { }
    public AStarClass(Grid[,] input_map, Vector2 start, Vector2 end, bool can_walk_slope, bool can_cross_terrain)
    {
        map = input_map;
        startPos = start;
        endPos = end;
        canWalkSlope = can_walk_slope;
        canCrossTerrain = can_cross_terrain;

        InitGrid();
    }

    //初始化网格
    void InitGrid()
    {
        mPointGrid = new AStarPoint[map.GetLength(0), map.GetLength(1)];

        for (int i = 0; i < mPointGrid.GetLength(0); i++)
        {
            for (int j = 0; j < mPointGrid.GetLength(1); j++)
            {
                mPointGrid[i, j] = new AStarPoint(i, j);
                if (startPos.x == i && startPos.y == j)
                {
                    mStartPoint = mPointGrid[i, j];
                }
                if (endPos.x == i && endPos.y == j)
                {
                    mEndPoint = mPointGrid[i, j];
                }
                //1代表障碍物
                if (!canCrossTerrain && map[i, j].canMove == false)
                {
                    mPointGrid[i, j].mIsObstacle = true;
                }
            }
        }
    }

    void ClearGrid()
    {
        for (int i = 0; i < mPointGrid.GetLength(0); i++)
        {
            for (int j = 0; j < mPointGrid.GetLength(1); j++)
            {
                mPointGrid[i, j] = null;
            }
        }
        mPointGrid = null;

    }

    //得到规范的输出
    public List<Vector2> getPath()
    {
        FindPath();

        List<Vector2> result = new List<Vector2>();
        List<AStarPoint> tmp = new List<AStarPoint>();

        AStarPoint tmp_p = mEndPoint;
        while (tmp_p.mParentPoint != null)
        {
            tmp.Add(tmp_p);
            tmp_p = tmp_p.mParentPoint;
        }
        tmp.Add(tmp_p);

        Debug.Log(tmp.Count);
        for (int i = tmp.Count - 1; i > -1; i--)
        {
            result.Add(new Vector2(tmp[i].mPosition.x, tmp[i].mPosition.y));
        }

        return result;
    }

    #region  寻路功能函数
    //寻路核心方法   
    public AStarPoint FindPath()
    {
        if (mEndPoint.mIsObstacle || mStartPoint.mPosition == mEndPoint.mPosition)
        {
            return null;
        }

        //开启列表     
        List<AStarPoint> openPointList = new List<AStarPoint>();
        //关闭列表       
        List<AStarPoint> closePointList = new List<AStarPoint>();
        openPointList.Add(mStartPoint);
        while (openPointList.Count > 0)
        {
            //寻找开启列表中最小预算值的表格        
            AStarPoint minFPoint = FindPointWithMinF(openPointList);
            //将当前表格从开启列表移除 在关闭列表添加      
            openPointList.Remove(minFPoint);
            closePointList.Add(minFPoint);
            //找到当前点周围的全部点      
            List<AStarPoint> surroundPoints = FindSurroundPoint(minFPoint);
            //在周围的点中，将关闭列表里的点移除掉     
            SurroundPointsFilter(surroundPoints, closePointList);
            //寻路逻辑          
            foreach (var surroundPoint in surroundPoints)
            {
                if (openPointList.Contains(surroundPoint))
                {
                    //计算下新路径下的G值（H值不变的，比较G相当于比较F值）      
                    float newPathG = CalcG(surroundPoint, minFPoint);
                    if (newPathG < surroundPoint.mG)
                    {
                        surroundPoint.mG = newPathG;
                        surroundPoint.mF = surroundPoint.mG + surroundPoint.mH;
                        surroundPoint.mParentPoint = minFPoint;
                    }
                }
                else
                {
                    //将点之间的       
                    surroundPoint.mParentPoint = minFPoint;
                    CalcF(surroundPoint, mEndPoint);
                    openPointList.Add(surroundPoint);
                }
            }
            //如果开始列表中包含了终点，说明找到路径      
            if (openPointList.IndexOf(mEndPoint) > -1)
            {
                break;
            }
        }
        return mEndPoint;
    }

    //寻找预计值最小的格子  
    private AStarPoint FindPointWithMinF(List<AStarPoint> openPointList)
    {
        float f = float.MaxValue;
        AStarPoint temp = null;
        foreach (AStarPoint p in openPointList)
        {
            if (p.mF < f)
            {
                temp = p;
                f = p.mF;
            }
        }
        return temp;
    }

    //寻找周围的全部点  
    private List<AStarPoint> FindSurroundPoint(AStarPoint point)
    {
        List<AStarPoint> list = new List<AStarPoint>();
        int x = (int)point.mPosition.x;
        int y = (int)point.mPosition.y;
        //Debug.Log(x.ToString() + ',' +y.ToString() );

        ////////////判断周围的八个点是否在网格内/////////////    
        AStarPoint up = null, down = null, left = null, right = null;
        AStarPoint lu = null, ru = null, ld = null, rd = null;

        if (y < mPointGrid.GetLength(1) - 1)
        {
            up = mPointGrid[x, y + 1];
        }
        if (y > 0)
        {
            down = mPointGrid[x, y - 1];
        }
        if (x > 0)
        {
            left = mPointGrid[x - 1, y];
        }
        if (x < mPointGrid.GetLength(0) - 1)
        {
            right = mPointGrid[x + 1, y];
        }

        if (canWalkSlope)
        {
            if (up != null && left != null)
            {
                lu = mPointGrid[x - 1, y + 1];
            }
            if (up != null && right != null)
            {
                ru = mPointGrid[x + 1, y + 1];
            }
            if (down != null && left != null)
            {
                ld = mPointGrid[x - 1, y - 1];
            }
            if (down != null && right != null)
            {
                rd = mPointGrid[x + 1, y - 1];
            }
        }

        /////////////将可以经过的表格添加到开启列表中/////////////       
        if (down != null && down.mIsObstacle == false)
        {
            list.Add(down);
        }
        if (up != null && up.mIsObstacle == false)
        {
            list.Add(up);
        }
        if (left != null && left.mIsObstacle == false)
        {
            list.Add(left);
        }
        if (right != null && right.mIsObstacle == false)
        {
            list.Add(right);
        }

        if (canWalkSlope)
        {
            if (lu != null && lu.mIsObstacle == false && left.mIsObstacle == false && up.mIsObstacle == false)
            {
                list.Add(lu);
            }
            if (ld != null && ld.mIsObstacle == false && left.mIsObstacle == false && down.mIsObstacle == false)
            {
                list.Add(ld);
            }
            if (ru != null && ru.mIsObstacle == false && right.mIsObstacle == false && up.mIsObstacle == false)
            {
                list.Add(ru);
            }
            if (rd != null && rd.mIsObstacle == false && right.mIsObstacle == false && down.mIsObstacle == false)
            {
                list.Add(rd);
            }
        }
        return list;
    }

    //将关闭带你从周围点列表中关闭   
    private void SurroundPointsFilter(List<AStarPoint> surroundPoints, List<AStarPoint> closePoints)
    {
        foreach (var closePoint in closePoints)
        {
            if (surroundPoints.Contains(closePoint))
            {
                //Debug.Log("将关闭列表的点移除");
                surroundPoints.Remove(closePoint);
            }
        }
    }

    //计算最小预算值点G值  
    private float CalcG(AStarPoint surround, AStarPoint minFPoint)
    {
        return Vector3.Distance(surround.mPosition, minFPoint.mPosition) + minFPoint.mG;
    }

    //计算该点到终点的F值   
    private void CalcF(AStarPoint now, AStarPoint end)
    {
        //F = G + H      
        float h = Mathf.Abs(end.mPosition.x - now.mPosition.x) + Mathf.Abs(end.mPosition.y - now.mPosition.y);
        float g = 0;
        if (now.mParentPoint == null)
        {
            g = 0;
        }
        else
        {
            g = Vector2.Distance(new Vector2(now.mPosition.x, now.mPosition.y), new Vector2(now.mParentPoint.mPosition.x, now.mParentPoint.mPosition.y)) + now.mParentPoint.mG;
        }

        float f = g + h;
        now.mF = f;
        now.mG = g;
        now.mH = h;
    }
    #endregion
}


/// <summary>/// 存储寻路点信息/// </summary>/// 
public class AStarPoint
{    //父“格子”    
    public AStarPoint mParentPoint { get; set; }

    public float mF { get; set; }
    public float mG { get; set; }
    public float mH { get; set; }

    //点的位置    
    public Vector2 mPosition { get; set; }

    //该点是否处于障碍物   
    public bool mIsObstacle { get; set; }
    public AStarPoint(int positionX, int positionY)
    {
        mPosition = new Vector2(positionX, positionY);
        mParentPoint = null;
        mIsObstacle = false;
    }
}
