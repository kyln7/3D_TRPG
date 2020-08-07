using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TRpgMap
{
    //地图数据列表:这不是地图数组，而是地图模型的数据
    [Serializable]
    public class MapDataList
    {
        public List<MapData> mapDataList = new List<MapData>();
    }

    [Serializable]
    public class MapData
    {
        public string name;
        public List<Grid> gridDataList = new List<Grid>();
        public MapData(string name)
        {
            this.name = name;
        }
    }

    //地图数据：程序通过获取这个类的实例中的地图数组gridArray来进行一些操作
    [Serializable]
    public class GridArray
    {
        //X Length
        public int Width;
        //Z Length
        public int Height;
        //StartPos
        public int xStart;
        public int zStart;
        //EndPos
        public int xEnd;
        public int zEnd;
        public Grid[,] gridArray;

        public GridArray(Vector3 startPos, Vector3 endPos)
        {
            xStart = (int)startPos.x;
            zStart = (int)startPos.z;
            xEnd = (int)endPos.x;
            zEnd = (int)endPos.z;
            Width = (xEnd - xStart) / Grid.cellSizeXZ + 1;
            Height = (zEnd - zStart) / Grid.cellSizeXZ + 1;
            if (xEnd >= xStart && zEnd >= zStart)
            {
                gridArray = new Grid[Width, Height];
                for (int x = 0; x < Width; x++)
                {
                    for (int z = 0; z < Height; z++)
                    {
                        gridArray[x, z] = new Grid(xStart + x * Grid.cellSizeXZ, zStart + z * Grid.cellSizeXZ, 0, false, "null");
                    }
                }
            }
        }
        public GridArray()
        {
            Width = 0;
            Height = 0;
            xStart = 0;
            xEnd = 0;
            zStart = 0;
            zEnd = 0;
            gridArray = new Grid[0, 0];
        }
        //通过obj获取其在数组内的位置:一维数组pos[x,z] 和 GameSystem的接口GetGameObjectByMouse一起使用
        public int[] GetGridPos(GameObject obj)
        {
            int x, z;
            x = (int)(obj.transform.position.x - xStart) / Grid.cellSizeXZ;
            z = (int)(obj.transform.position.z - zStart) / Grid.cellSizeXZ;
            return new int[2] { x, z };
        }
        //设置item下的grid
        public void SetGridUnderObj(Item obj)
        {
            int x, z;
            x = (int)(obj.transform.position.x - xStart) / Grid.cellSizeXZ;
            z = (int)(obj.transform.position.z - zStart) / Grid.cellSizeXZ;
            gridArray[x, z].canMove = false;
            gridArray[x, z].hasItem = true;
        }
    }

    [Serializable]
    public class Grid
    {
        //在世界坐标中的X位置
        public int x;
        //在世界坐标中的Z位置
        public int z;
        //该格子的高度
        public float height;
        //能否通行
        public bool canMove;
        //有无物品
        public bool hasItem;
        //温度
        public int temperature;
        //阻挡度（在能够通行的条件下）
        public int delay;
        //格子的长宽
        public const int cellSizeXZ = 2;
        //格子的高度
        public const int cellSizeY = 1;
        public string gridName;

        //new a Grid that x,z are the array index
        public Grid(int x, int z, float height, bool canMove, string gridName)
        {
            this.x = x;
            this.z = z;
            this.height = height;
            this.canMove = canMove;
            this.gridName = gridName;
        }
        //return real x,y,z in Unity
        public Vector3 GetPos()
        {
            return new Vector3(x, height, z);
        }
        //override ToString
        public override string ToString()
        {
            return "Name : " + gridName + ",Pos : X = " + x + " , Z = " + z + " , CanMove = " + canMove;
        }
    }
}

