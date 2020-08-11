using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgAction;

namespace TrpgAI
{
    public class State
    {
        public int StateType
        {
            get; protected set;
        }
        public IAgent Agent
        {
            get; set;
        }
        protected StateMachine m_StateMachine;
        public void SetStateMachine(StateMachine m)
        {
            m_StateMachine = m;
        }
        public virtual void Enter() { }
        public virtual State Execute() { return null; }
        public virtual void Exit() { }

    }
    enum EStateType
    {
        Idel, Walk, Talk,
    }
    
    class IdelState : State
    {
        public IdelState()
        {
            StateType = (int)EStateType.Idel;
        }
        public override State Execute()
        {
            Character c = (Character)Agent;
            if (c.Actions.Count > 0)
            {
                //todo
                if (c.curAction is IdelAction)
                {
                    c.curAction.FinishAction(c);
                }
                c.GetAction();
                if (c.curAction is MoveAction)
                {
                    c.canMove = true;
                    return m_StateMachine.Transition((int)EStateType.Walk);
                }
                if (c.curAction is TalkAction)
                {
                    c.canMove = false;
                    return m_StateMachine.Transition((int)EStateType.Talk);
                }
            }
            else
            {
                if (c.curAction == null)
                {
                    c.curAction = new IdelAction();
                } 
                if (c.curAction.isDone)
                {
                    c.AddMoveAction();
                }
                else
                {
                    c.curAction.DoAction(c);
                }
            }
            return this;
        }
    }
    class WalkState : State
    {
        public WalkState()
        {
            StateType = (int)EStateType.Walk;
        }
        public override State Execute()
        {
            Character c = (Character)Agent;
            if (c.Actions.Count > 0)
            {
                //todo
                if (c.Actions.Peek() is TalkAction)
                {
                    c.curAction.FinishAction(c);
                    c.canMove = false;
                    c.GetAction();
                    return m_StateMachine.Transition((int)EStateType.Talk);
                }
            }
            if (c.curAction.isDone)
            {
                c.AddIdelAction();
                return m_StateMachine.Transition((int)EStateType.Idel);
            }
            else
            {
                c.curAction.DoAction(c);
            }
            return this;
        }
    }

    class TalkState : State
    {
        public TalkState()
        {
            StateType = (int)EStateType.Talk;
        }
        public override State Execute()
        {
            Character c = (Character)Agent;
            if (c.curAction.isDone)
            {
                c.AddIdelAction();
                return m_StateMachine.Transition((int)EStateType.Idel);
            }
            else
            {
                c.curAction.DoAction(c);
            }
            return this;
        }
    }
}