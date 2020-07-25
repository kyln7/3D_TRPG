using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;

public class GameControl : MonoBehaviour
{
    public static GridArray Map;
    // Start is called before the first frame update
    void Init()
    {
        Map = SaveSystem.LoadMapData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
