using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;

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
            //Debug.Log("Hit Name: " + hit.transform.name + " , Hit Position: " + hit.transform.position.ToString());
            res = hit.transform.gameObject;
            return res;
        }
        return null;
    }

    public static void SetMapItem(GameObject ItemRoot,GridArray gridArray)
    {
        int x,z;
        foreach(Transform item in ItemRoot.transform)
        {
            x = (int)item.position.x;
            z = (int)item.position.z;
            gridArray.SetGridUnderObj(item.GetComponent<Item>());
        }
    }
}
