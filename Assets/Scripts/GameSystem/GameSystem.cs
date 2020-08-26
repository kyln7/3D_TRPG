using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;
using Grid = TRpgMap.Grid;

public class GameSystem : MonoBehaviour
{
    //通过鼠标点击获取对象，layerMask决定要获取的对象的layer是哪一个
    public static GameObject GetGameObjectByMouse(string layerMask)
    {
        GameObject res;
        RaycastHit hit;
        int layer = LayerMask.GetMask(layerMask);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red, 10f);
            Debug.Log("Hit Name: " + hit.transform.name + " , Hit Position: " + hit.transform.position.ToString());
            res = hit.transform.gameObject;
            return res;
        }
        return null;
    }

    //设置含有Item的格子
    public static void SetMapItem(GameObject ItemRoot, GridArray gridArray)
    {
        int x, z;
        foreach (Transform item in ItemRoot.transform)
        {
            x = (int)item.position.x;
            z = (int)item.position.z;
            gridArray.SetGridUnderObj(item.GetComponent<Item>());
        }
    }
    //判断格子是否在当前地图内
    public static bool IsGridInMap(Vector2Int pos)
    {
        GridArray map = GameControl.Map;
        return (pos.x <= map.xEnd && pos.x >= map.xStart && pos.y <= map.zEnd && pos.y >= map.zStart) ? true : false;
    }

    //检测当前格子上是否存在指定的对象
    public static bool HasObjectOnGrid(Vector2Int detectPos, string layerMask)
    {
        int layer = LayerMask.GetMask(layerMask);
        RaycastHit hit;
        Debug.DrawRay(new Vector3(detectPos.x, 0, detectPos.y), Vector3.up * 100f, Color.red, 100f);
        if (Physics.Raycast(new Vector3(detectPos.x, 0, detectPos.y), Vector3.up, out hit, Mathf.Infinity, layer))
        {
            return true;
        }
        else return false;
    }

    //获取当前格子上指定的对象
    public static GameObject GetObjectOnGrid(Vector2Int detectPos, string layerMask)
    {
        int layer = LayerMask.GetMask(layerMask);
        RaycastHit raycast;
        Debug.DrawRay(new Vector3(detectPos.x, 0, detectPos.y), Vector3.up * 100f, Color.red, 100f);
        if (Physics.Raycast(new Vector3(detectPos.x, 0, detectPos.y), Vector3.up, out raycast, Mathf.Infinity, layer))
        {
            return raycast.transform.gameObject;
        }
        else return null;
    }

    //得到一个格子的扩张的范围
    public static List<Vector2Int> GetRange(Vector2Int pos, int range)
    {
        List<Vector2Int> res = new List<Vector2Int>();
        int ls = pos.x - range * Grid.cellSizeXZ;
        int re = pos.x + range * Grid.cellSizeXZ;
        int ds = pos.y - range * Grid.cellSizeXZ;
        int ue = pos.y + range * Grid.cellSizeXZ;
        for (int x = ls; x <= re; x += Grid.cellSizeXZ)
        {
            for (int z = ds; z <= ue; z += Grid.cellSizeXZ)
            {
                Vector2Int node = new Vector2Int(x, z);
                if ((int)Mathf.Abs(pos.x - x) + (int)Mathf.Abs(pos.y - z) <= range * Grid.cellSizeXZ && IsGridInMap(node))
                    res.Add(node);
            }
        }
        return res;
    }
    //检测给出的位置序列中，是否有需要查找的对象
    public static bool HasObjectOnGrids(List<Vector2Int> detectPos, string layerMask)
    {
        foreach (Vector2Int pos in detectPos)
        {
            if (HasObjectOnGrid(pos, layerMask))
            {
                return true;
            }
        }
        return false;
    }
}
