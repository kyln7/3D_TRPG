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
    public Transform cursorSelect;
    public Vector3 cursorPos;

    public Vector2 playersStartPos, playersEndPos;

    bool ifTouched = false;

    public static InputMode inputMode;
    public enum InputMode { UI, Game }

    void Start()
    {
        Map = SaveSystem.LoadMapData();
        inputMode = InputMode.Game;
        playersStartPos = new Vector2(Map.GetGridPos(player)[0], Map.GetGridPos(player)[1]);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputMode == InputMode.Game)
        {
            //game logic
            cursorSelect.gameObject.SetActive(true);
            GetMousePos();
            cursorSelect.position = cursorPos;
            if (Input.GetMouseButtonDown(0))
            {
                ifTouched = true;
                GameObject obj = GameSystem.GetGameObjectByMouse("Ground");
                if (obj == null) return;
                playersEndPos = new Vector2(Map.GetGridPos(obj)[0], Map.GetGridPos(obj)[1]);
            }
            if (ifTouched)
            {
                if (AStarMove.GetInstance().Move(player, Map.gridArray, playersStartPos, playersEndPos, out Vector2 stopPos))
                {
                    ifTouched = false;
                    playersStartPos = stopPos;
                }
            }
        }
        else
        {
            //ui logic
        }
    }

    //return cursorPos if mousepos is valid
    public void GetMousePos()
    {
        cursorPos = GameSystem.GetGameObjectByMouse("Ground") != null ?
        (GameSystem.GetGameObjectByMouse("Ground").CompareTag("canMove") ?
        GameSystem.GetGameObjectByMouse("Ground").transform.position : cursorPos) : cursorPos;
        cursorPos.y = 0.52f;
    }
}
