using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrpgAI;
using TRpgAction;
using TRpgMap;

public class Character : MonoBehaviour, IAgent
{
    public GridArray Map;
    public int Strenth = 10;
    public Animator animator;
    public bool attacking;
    public bool ReceivingDmg;
    public bool canMove = true;

    public Vector2Int curPos;
    public Vector2Int walkPos;
    public Action curAction;
    public Queue<Action> Actions;

    public StateMachine m_FSM;

    // Start is called before the first frame update
    void Start()
    {
        Map = GameControl.Map;
        Actions = new Queue<Action>();
        curAction = null;
        walkPos = GetPos();
        m_FSM = new StateMachine(this);
        m_FSM.AddState(new WalkState());
        m_FSM.AddState(new IdelState());
        m_FSM.SetDefaultState((int)EStateType.Idel);
    }

    // Update is called once per frame
    void Update()
    {
        curPos = GetPos();
        m_FSM.Update();
    }

    public Vector2Int GetPos()
    {
        if (Map != null)
            return new Vector2Int(GameControl.Map.GetGridPos(gameObject)[0], GameControl.Map.GetGridPos(gameObject)[1]);
        else return Vector2Int.zero;
    }
    public void GetAction()
    {
        curAction = Actions.Dequeue();
    }

    public void AddMoveAction(Vector2Int pos)
    {
        walkPos = pos;
        Actions.Enqueue(new MoveAction(pos));
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
        Debug.Log("Waiting..");
        Wait(time);
    }

    IEnumerator Wait(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
