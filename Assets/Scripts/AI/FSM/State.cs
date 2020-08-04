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
        Idel, Walk, Talk, Attack
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
                c.GetAction();
                if (c.curAction is MoveAction)
                {
                    c.canMove = true;
                    return m_StateMachine.Transition((int)EStateType.Walk);
                }
            }
            c.Idel(5);
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
            if (c.walkPos == c.GetPos()) return m_StateMachine.Transition((int)EStateType.Idel);
            else
            {
                c.curAction.DoAction(c);
            }
            return this;
        }
    }
}