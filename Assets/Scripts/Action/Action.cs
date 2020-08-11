using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;

namespace TRpgAction
{
    public class Action
    {
        public string ActionName = "action";
        public bool isDone = true;
        public bool doing = false;
        public virtual void DoAction(Character character)
        {

        }
        public virtual void FinishAction(Character character)
        {

        }
    }

    public class MoveAction : Action
    {
        public Vector2 endPos;
        public Vector2 startPos;
        public GridArray mapData;
        public Vector2 stopPos;
        public AStarMove aStarMove;
        public override void DoAction(Character character)
        {
            if (!aStarMove.ifGetPath)
            {
                startPos = new Vector2Int(character.curPos.x, character.curPos.y);
            }
            if (aStarMove.Move(character.gameObject, mapData.gridArray, startPos, endPos, out Vector2 stopPos)) isDone = true;
        }
        public override void FinishAction(Character character)
        {
            Debug.Log("Finish Walking");
            isDone = true;
            aStarMove.ifGetPath = false;
            doing = false;
            character.canMove = true;
        }
        public MoveAction(Vector2Int pos, GridArray mapData)
        {
            this.mapData = mapData;
            isDone = false;
            doing = false;
            ActionName = "MoveAction";
            endPos = pos;
            aStarMove = AStarMove.GetInstance();
        }
    }

    public class IdelAction : Action
    {
        public override void DoAction(Character character)
        {
            if (doing == false)
            {
                doing = true;
                character.Idel(4);
            }
            if (character.canMove) isDone = true;
        }

        public override void FinishAction(Character character)
        {
            isDone = true;
            doing = false;
            character.canMove = true;
        }
        public IdelAction()
        {
            isDone = false;
            doing = false;
            ActionName = "IdelAction";
        }
    }

    public class TalkAction : Action
    {
        public override void DoAction(Character character)
        {
            if (doing == false)
            {
                doing = true;
                character.Talk(4);
            }
            if (character.canMove) isDone = true;
        }

        public override void FinishAction(Character character)
        {
            isDone = true;
            doing = false;
            character.canMove = true;
        }
        public TalkAction()
        {
            isDone = false;
            doing = false;
            ActionName = "TalkAction";
        }
    }
}