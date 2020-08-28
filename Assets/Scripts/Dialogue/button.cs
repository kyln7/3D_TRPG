using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    public List<Dialogue> dialogues;
    public Dictionary<int, bool> dialogDic;
    private void Start()
    {
        dialogDic = new Dictionary<int, bool>();
        dialogDic.Add(1, false);
        dialogDic.Add(2, false);
        dialogDic.Add(3, false);
        dialogues = new List<Dialogue>();

        foreach (KeyValuePair <int,bool> dia in dialogDic)
        {
            Dialogue dialogue = new Dialogue();
            Debug.Log(dia.Key);
            dialogue.LoadDialogue(dia.Key.ToString());
            dialogues.Add(dialogue);
        }
    }

    public void OnDialog()
    {
        int num = Random.Range(0, dialogues.Count);
        Debug.Log("加载对话：" + num.ToString());
        //StartCoroutine(gameObject.GetComponent<TypeEffect>().StartDialog(dialogues[num]));
    }
}
