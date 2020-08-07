using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TRpgAction
{
    public class Action
    {
        public string ActionName = "action";
        public bool isDone = true;
        public virtual void DoAction(GameObject obj)
        {
            Debug.Log("nullAction");
        }
    }

    public class MoveAction : Action
    {
        public Vector3 endPos;

        public override void DoAction(GameObject obj)
        {
            //Debug.Log("Doing");
            if ((endPos - obj.transform.position).magnitude > 1f)
            {
                //TODO
                //Debug.Log("Mowing");
                obj.transform.Translate((endPos - obj.transform.position).normalized * Time.deltaTime * 10f);
            }
            else isDone = true;
        }

        public MoveAction(Vector3 pos)
        {
            isDone = false;
            ActionName = "MoveAction";
            endPos = pos;
        }
    }
}