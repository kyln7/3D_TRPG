using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using TRpgMap;
using TRpgAction;

public class Testing : MonoBehaviour
{
    private void Start() {
        if(GameSystem.HasObjectOnGrid(new Vector2Int((int)transform.position.x,(int)transform.position.z),"Wall"))
        {
            Debug.Log("Has Wall");
        }
    }
}
