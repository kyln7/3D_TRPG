using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgAction;

public class ActionManager : MonoBehaviour
{
    private Queue<Action> actionQueue;
    private Action curAction;
    private void Start()
    {
        actionQueue = new Queue<Action>();
        curAction = new Action();
    }

    private void Update()
    {
        if (curAction.isDone)
        {
            if (actionQueue.Count > 0)
            {
                curAction = actionQueue.Dequeue();
            }
            // else 
            //     Debug.Log("Waiting for new Action");
                
        }
        else{
            curAction.DoAction(gameObject);
        }
    }

    public void AddAction(Action action)
    {
        actionQueue.Enqueue(action);
    }
}
