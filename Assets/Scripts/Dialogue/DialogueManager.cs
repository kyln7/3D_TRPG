using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DialogueManager:MonoBehaviour
{
    //public static DialogueManager instance;
    //speed指1秒显示多少字
    public bool StartDialog(string TextName, float textspeed)
    {
        //加载对话文件
        Dialogue dialogue = new Dialogue();
        if (!dialogue.LoadDialogue(TextName)) return false;

        //加载对话框
        GameObject DialogBox = Resources.Load("Dialogue/DialogueSet") as GameObject;
        DialogBox = Instantiate(DialogBox);

        TypeEffect typeEffect = DialogBox.GetComponent<TypeEffect>();
        StartCoroutine(typeEffect.StartDia(DialogBox, dialogue,textspeed));

        return true;
    }

    public bool StartDialog(Dialogue dialogue, float textspeed)
    {
        //加载对话框
        GameObject DialogBox = Resources.Load("Dialogue/DialogueSet") as GameObject;
        DialogBox = Instantiate(DialogBox);

        TypeEffect typeEffect = DialogBox.GetComponent<TypeEffect>();
        StartCoroutine(typeEffect.StartDia(DialogBox, dialogue, textspeed));

        return true;
    }
}
