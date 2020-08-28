using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DialogueManager:MonoBehaviour
{
    public static DialogueManager instance;

    public bool StartDialog(string TextName)
    {
        //加载对话文件
        Dialogue dialogue = new Dialogue();
        if (!dialogue.LoadDialogue(TextName)) return false;

        //加载对话框
        GameObject DialogBox = Resources.Load("Dialogue/DialogueSet") as GameObject;

        TypeEffect typeEffect = DialogBox.GetComponent<TypeEffect>();
        StartCoroutine(typeEffect.StartDia(DialogBox, dialogue,0.5f));

        return true;
    }

    public bool StartDialog(Dialogue dialogue)
    {
        return true;
    }
}
