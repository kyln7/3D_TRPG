using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testdialog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DialogueManager t = gameObject.GetComponent<DialogueManager>();
        t.StartDialog("1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
