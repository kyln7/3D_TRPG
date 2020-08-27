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
            if (player.GetComponent<Player>().s_inSight != null)
            {
                //player.GetComponent<Player>().s_inSight.GetScope();
                player.GetComponent<Player>().ShowScope((Insight)player.GetComponent<Player>().s_inSight);
                player.GetComponent<Player>().ShowItems((Insight)player.GetComponent<Player>().s_inSight);
            }
        }
    }
}
