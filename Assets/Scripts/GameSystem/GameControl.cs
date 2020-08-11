using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;
using TRpgSkill;
using TRpgAction;
public class GameControl : MonoBehaviour
{
    public static GridArray Map;
    public GameObject player;
    
    void Start()
    {
        Map = SaveSystem.LoadMapData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.GetComponent<Character>().AddTalkAction();
        }
    }
}
