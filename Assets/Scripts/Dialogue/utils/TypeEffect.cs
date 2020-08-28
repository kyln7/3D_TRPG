using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    Image LC, RC;
    Text NameText, MainText;

    public IEnumerator StartDia(GameObject DialogBox, Dialogue dialogue, float textspeed)
    {
        //GameObject DialogBox = GameObject.Find("Canvas").transform.Find("DialogBox").gameObject;
        //生成对话框
        DialogBox = Instantiate(DialogBox);
        DialogBox.transform.SetParent(GameObject.Find("Canvas").transform);
        DialogBox.GetComponent<RectTransform>().localPosition = new Vector3(0, -385, 0);

        LC = DialogBox.transform.GetChild(0).gameObject.GetComponent<Image>();
        RC = DialogBox.transform.GetChild(1).gameObject.GetComponent<Image>();
        NameText = DialogBox.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        MainText = DialogBox.transform.GetChild(2).GetChild(1).GetComponent<Text>();

        int i = 0;
        while (true)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("start Dialog!");
                if (i > dialogue.sentences.Count - 1) break;

                NameText.text = dialogue.sentences[i].GetName();
                LC.sprite = dialogue.LoadedPics[dialogue.sentences[i].GetPicnames()[0]];
                RC.sprite = dialogue.LoadedPics[dialogue.sentences[i].GetPicnames()[1]];

                //控制角色明暗
                int s = 0;
                foreach (var item in dialogue.LoadedPics)
                {
                    if (dialogue.sentences[i].GetSpeaker() == item.Key)
                    {
                        break;
                    }
                    s += 1;
                }
                if (s == 0)
                {
                    LC.color = Color.white;
                    RC.color = Color.gray;
                }
                else
                {
                    RC.color = Color.white;
                    LC.color = Color.gray;
                }

                Debug.Log(DialogBox.activeSelf);
                //显示对话
                yield return StartCoroutine(test());//TypeAsWrite(MainText,dialogue.sentences[i].GetContent(),textspeed));
                i += 1;
            }
        }
        //DialogBox.SetActive(false);
        Destroy(DialogBox);

    }

    //打字机效果
    IEnumerator TypeAsWrite(Text textBox, string content, float speed)
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

    IEnumerator test()
    {
        yield return null;
        Debug.Log("test");
    }
}
