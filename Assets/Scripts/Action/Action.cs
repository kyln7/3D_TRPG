using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TRpgAction
{
    public class Action
    {
        public string ActionName = "action";
        public bool isDone = true;
        public virtual void DoAction(Character character)
        {
            Debug.Log("nullAction");
        }
    }

    public class MoveAction : Action
    {
        public Vector2Int endPos;
        public Vector2Int startPos;
        public override void DoAction(Character character)
        {
            TRpgMap.Grid startGrid = GameControl.Map.gridArray[character.curPos.x,character.curPos.y];
            TRpgMap.Grid endGrid = GameControl.Map.gridArray[character.walkPos.x,character.walkPos.y];
            startPos = new Vector2Int(startGrid.x,startGrid.z);
            endPos = new Vector2Int(endGrid.x,endGrid.z);
            //Debug.Log("Doing");
            if ((endPos - startPos).magnitude > 1f)
            {
                //temp code
                Vector3 s = new Vector3(startPos.x,1,startPos.y);
                Vector3 e = new Vector3(endPos.x,1,endPos.y);
                //TODO
                //Debug.Log("Mowing");
                character.transform.Translate((e - s).normalized * Time.deltaTime * 10f);
            }
            else isDone = true;
        }

        public MoveAction(Vector2Int pos)
        {
            isDone = false;
            ActionName = "MoveAction";
            endPos = pos;
        }
    }
}