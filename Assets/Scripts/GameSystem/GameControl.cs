using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;
using TRpgSkill;

public class GameControl : MonoBehaviour
{
    public static GridArray Map;
    public GameObject player;
    public Transform cursorSelect;
    public Vector3 cursorPos;

    public Vector2 playersStartPos, playersEndPos;
    bool ifTouched = false;

    public static InputMode inputMode;
    public enum InputMode { UI, Game, SelectObj }
    public DialogueManager dialogueManager;

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
                if (GameSystem.IsPointerOverGameObject(Input.mousePosition))
                {
                    inputMode = InputMode.UI;
                }
                else
                {
                    ifTouched = true;
                    GameObject obj = GameSystem.GetGameObjectByMouse("Ground");
                    if (obj == null) return;
                    if (obj.CompareTag("cantMove")) return;
                    playersEndPos = new Vector2(Map.GetGridPos(obj)[0], Map.GetGridPos(obj)[1]);
                }
            }
            if (ifTouched)
            {
                if (AStarMove.GetInstance().Move(player, Map, playersStartPos, playersEndPos, out Vector2 stopPos))
                {
                    ifTouched = false;
                    playersStartPos = stopPos;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                List<Vector2Int> testList = GameSystem.GetRange(player.GetComponent<Player>().GetPos(), 1);
                foreach (Vector2Int node in testList)
                {
                    Debug.DrawRay(new Vector3(node.x, -1, node.y), Vector3.up * 100f, Color.red, 100f);
                    if (GameSystem.HasObjectOnGrid(node, "Npc"))
                    {
                        inputMode = InputMode.UI;
                        dialogueManager = GameSystem.GetObjectOnGrid(node, "Npc").GetComponent<DialogueManager>();
                        dialogueManager.StartDialog("1", 4);
                        GameSystem.GetObjectOnGrid(node, "Npc").GetComponent<Character>().AddTalkAction();
                        return;
                    }
                }
            }
        }
        else if (inputMode == InputMode.UI)
        {
            //ui logic
        }
        else
        {
            player.GetComponent<Player>().ShowScope(player.GetComponent<Player>().usingSkill);
            if (Input.GetKey(KeyCode.Escape))
            {
                player.GetComponent<Player>().FinishShowScope(player.GetComponent<Player>().usingSkill);
                GameControl.inputMode = GameControl.InputMode.Game;
            }
            GetSkillObj(player.GetComponent<Player>().usingSkill);
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
    public void GetSkillObj(Skill skill)
    {
        foreach (Vector2Int node in player.GetComponent<Player>().skillScope)
        {
            if (GameSystem.GetGameObjectByMouse("Ground") != null)
            {
                if (GameSystem.GetGameObjectByMouse("Ground").transform.position.x == node.x &&
                    GameSystem.GetGameObjectByMouse("Ground").transform.position.z == node.y
                )
                {
                    cursorPos = GameSystem.GetGameObjectByMouse("Ground").transform.position;
                    cursorPos.y = 0.52f;
                    cursorSelect.gameObject.SetActive(true);
                    cursorSelect.position = cursorPos;
                    //-----------------------------------------
                    if (skill is Hit)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            GameObject hitObj = GameSystem.GetGameObjectByMouse("Npc");
                            if (hitObj == null)
                            {
                                hitObj = GameSystem.GetGameObjectByMouse("Item");
                            }
                            if (hitObj != null)
                            {
                                var res = DicePoint.Instance.BlurCheckTwo(player, hitObj, 0);
                                if (res.Item1 == 0)
                                {
                                    if (res.Item2 == DiceResult.Success) hitObj.GetComponent<NPC>().p.HP -= 10;
                                    if (res.Item2 == DiceResult.BigSuccess) hitObj.GetComponent<NPC>().p.HP -= 20;
                                    if (res.Item2 == DiceResult.HardSuccess) hitObj.GetComponent<NPC>().p.HP -= 5;
                                }
                                player.GetComponent<Player>().FinishShowScope(skill);
                                inputMode = InputMode.Game;
                            }
                        }
                    }
                    //------------------------------------------------------------------
                    if (skill is Check)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            GameObject hitObj = GameSystem.GetGameObjectByMouse("Item");
                            if (hitObj != null)
                            {
                                GameObject Menu = GameObject.Find("Menu");
                                GameObject Skills = GameObject.Find("Skills");
                                GameObject Items = GameObject.Find("ItemCheckMenu");
                                GameObject StatusDetail = GameObject.Find("StatusDetail");
                                StatusDetail.transform.localPosition = new Vector3(-1300, 0, 0);
                                Items.transform.localPosition = new Vector3(0, 0, 0);
                                Skills.transform.localPosition = new Vector3(0, -900, 0);
                                Menu.transform.localPosition = new Vector3(1000, 0, 0);
                                player.GetComponent<Player>().FinishShowScope(skill);
                                ItemClick.SetItemUI(hitObj);
                                inputMode = InputMode.UI;
                            }
                        }
                    }
                }
            }
        }
    }
}
