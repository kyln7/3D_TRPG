using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
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
}
