using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TRpgMap
{
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


        public void SetGrid(int x, int z, Grid grid)
        {
            gridArray[x, z] = grid;
        }
        public Grid GetGrid(int x, int z)
        {
            return gridArray[x, z];
        }
    }

    [Serializable]
    public class Grid
    {
        //
        public int x;
        public int z;
        public int height;
        public bool canMove;
        public bool hasItem;
        public int temperature;
        //阻挡度
        public int delay;
        public const int cellSizeXZ = 2;
        public const int cellSizeY = 1;
        public string gridName;

        //new a Grid that x,z are the array index
        public Grid(int x, int z, int height, bool canMove, string gridName)
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

