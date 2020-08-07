using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogBox;

    Dialogue dialogue;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = new Dialogue();
        dialogue.LoadDialogue("01");
        Debug.Log(dialogue.sentences.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (i > dialogue.sentences.Count - 1) i = 0;

            Debug.Log(dialogue.sentences[i].getContent());
            DialogBox.transform.GetChild(0).GetComponentInChildren<Text>().text = dialogue.sentences[i].getName();
            DialogBox.transform.GetChild(1).GetComponentInChildren<Text>().text = dialogue.sentences[i].getContent();
            i += 1;
        }
    }

}
