using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortcut : MonoBehaviour
{
    bool moving = false;
    bool isDown = false;
    float speed = 6;
    Vector3 start = new Vector3(0, 0, 0);
    Vector3 end = new Vector3(0, -160, 0);
    bool isOrder = false;
    Vector3 position = new Vector3(-200, -384, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = GameObject.Find("MenuSet");
        GameObject Status = GameObject.Find("Status");
        GameObject Order = GameObject.Find("Order");
        if (Input.GetKeyDown(KeyCode.Space) && isDown==false && moving == false)
        {
            start = new Vector3(0, 0, 0);
            end = new Vector3(0, -160, 0);
            target.transform.localPosition = Vector3.MoveTowards(start, end, speed);
            moving = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isDown == true && moving == false)
        {
            start = new Vector3(0, -160, 0);
            end = new Vector3(0, 0, 0);
            target.transform.localPosition = Vector3.MoveTowards(start, end, speed);
            moving = true;
        }
        if(Input.GetKeyDown(KeyCode.Z) && moving == false)
        {
            if(isOrder == false)
            {
                Status.transform.localPosition = new Vector3(-200, -570, 0);
                Order.transform.localPosition = position;
                isOrder = !isOrder;
            }
            else
            {
                Status.transform.localPosition = position;
                Order.transform.localPosition = new Vector3(-200, -570, 0);
                isOrder = !isOrder;
            }
        }
        if (moving==true)
        {
            start = target.transform.localPosition;
            target.transform.localPosition = Vector3.MoveTowards(start, end, speed);
            if(target.transform.localPosition==end)
            {
                moving = false;
                isDown = !isDown;
            }
        }
    }
}
