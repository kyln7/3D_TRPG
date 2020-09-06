using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrpgAI;
using TRpgAction;
using TRpgMap;
using TMPro;

public class Character : MonoBehaviour, IAgent
{
    public TextMeshPro hptext;
    public Transform[] randomMovePos;
    //public GridArray Map;
    public int Strenth = 10;
    public Animator animator;
    public bool attacking;
    public bool ReceivingDmg;
    public bool canMove;
    public bool isTalking;
    public Vector2Int curPos;
    public Vector2Int walkPos;
    public Action curAction;
    public Queue<Action> Actions;
    public StateMachine m_FSM;

    // Start is called before the first frame update
    void Start()
    {
        //Map = GameControl.Map;
        canMove = true;
        isTalking = false;
        ReceivingDmg = false;
        attacking = false;
        Actions = new Queue<Action>();
        curAction = null;
        walkPos = GetPos();
        m_FSM = new StateMachine(this);
        m_FSM.AddState(new WalkState());
        m_FSM.AddState(new IdelState());
        m_FSM.AddState(new TalkState());
        m_FSM.SetDefaultState((int)EStateType.Idel);
    }

    // Update is called once per frame
    void Update()
    {
        curPos = GetPos();
        if (GameControl.inputMode == GameControl.InputMode.Game)
        {
            m_FSM.Update();
        }
        hptext.text = "HP: " + gameObject.GetComponent<NPC>().p.HP;
    }

    //获取当前对象在地图数组中的位置
    public Vector2Int GetPos()
    {
        return new Vector2Int(GameControl.Map.GetGridPos(gameObject)[0], GameControl.Map.GetGridPos(gameObject)[1]);
    }
    //返回行为队列里的行为
    public void GetAction()
    {
        curAction = Actions.Dequeue();
    }
    //添加一个移动行为
    public void AddMoveAction()
    {
        GameObject v3 = randomMovePos[Random.Range(0, randomMovePos.Length)].gameObject;
        walkPos = new Vector2Int(GameControl.Map.GetGridPos(v3)[0], GameControl.Map.GetGridPos(v3)[1]);
        Actions.Enqueue(new MoveAction(walkPos, GameControl.Map));
    }
    public void AddMoveAction(Vector2Int pos)
    {
        walkPos = pos;
        Actions.Enqueue(new MoveAction(walkPos, GameControl.Map));
    }
    //添加一个待机行为
    public void AddIdelAction()
    {
        Actions.Enqueue(new IdelAction());
    }
    //todo
    public void AddTalkAction()
    {
        Actions.Enqueue(new TalkAction());
    }

    public void ReceiveDmg(Character character)
    {

    }
    public void CulDmg()
    {
        //判断闪避or防御
        //计算伤害
        //播放动画
    }

    public void Idel(float time)
    {
        if (canMove)
        {
            StartCoroutine(WaitIdel(time));
        }
    }

    //todo
    IEnumerator WaitIdel(float time)
    {
        //Debug.Log("Waiting..");
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
