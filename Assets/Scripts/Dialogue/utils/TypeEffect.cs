using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public Text text;
    public float speed;

    public IEnumerator StartDialog(Dialogue dialogue)
    {
        GameObject DialogBox = GameObject.Find("Canvas").transform.Find("DialogBox").gameObject;
        DialogBox.SetActive(true);

        int i = 0;
        while (true)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("start Dialog!");
                if (i > dialogue.sentences.Count - 1) break;
 
                if (dialogue.sentences[i].GetSType() == SType.self)
                {
                    DialogBox.transform.GetChild(0).gameObject.SetActive(true);
                    DialogBox.transform.GetChild(1).gameObject.SetActive(false);
                    DialogBox.transform.GetChild(0).GetComponentInChildren<Text>().text = dialogue.sentences[i].getName();
                }
                else
                {
                    DialogBox.transform.GetChild(1).gameObject.SetActive(true);
                    DialogBox.transform.GetChild(0).gameObject.SetActive(false);
                    DialogBox.transform.GetChild(1).GetComponentInChildren<Text>().text = dialogue.sentences[i].getName();
                }

                StartCoroutine(TypeAsWrite(DialogBox.transform.GetChild(2).GetComponentInChildren<Text>(),
                                                                  dialogue.sentences[i].getContent()));
                i += 1;
            }
        }
        DialogBox.SetActive(false);

    }
    /*
    public void TypeAsWrite(Text textBox, string content)
    {
        StartCoroutine(Typing(textBox, content));
    }*/

    IEnumerator TypeAsWrite(Text textBox, string content)
    {
        int i = 0;
        float timer = 0;
        while (true)
        {
            if (Input.GetMouseButtonDown(1)) { /*Debug.Log("switch sen");*/ i = content.Length; }
            textBox.text = content.Substring(0, i);
            yield return null;
            timer += Time.deltaTime;
            if (timer > speed)
            {
                i += 1;
                timer = 0;
            }

            if (i > content.Length) break;
        }
    }
}
