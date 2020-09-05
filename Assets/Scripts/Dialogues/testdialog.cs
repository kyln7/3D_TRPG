using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testdialog : MonoBehaviour
{
    DialogueManager t;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.AddComponent<DialogueManager>();
        //t.StartDialog("2",5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            i += 5;
            t.StartDialog("2",5+i);
        }
    }
}
