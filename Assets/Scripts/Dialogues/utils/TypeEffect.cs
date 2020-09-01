using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    Image LC, RC;
    Text NameText, MainText;

    bool flag = true;
    public IEnumerator StartDia(GameObject DialogBox, Dialogue dialogue, float textspeed)
    {
        //GameObject DialogBox = GameObject.Find("Canvas").transform.Find("DialogBox").gameObject;
        //生成对话框
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
            if (flag || Input.GetMouseButtonDown(0))
            {
                flag = false;
                Debug.Log("start Dialog!");
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

                Debug.Log(gameObject.name);
                Debug.Log(gameObject.activeSelf);

                /*
                //打字机效果显示对话
                int k = 0;
                float timer = 0;
                while (true)
                {
                    if (Input.GetMouseButtonDown(1)) {k = dialogue.sentences[i].GetContent().Length; }
                    MainText.text = dialogue.sentences[i].GetContent().Substring(0, k);
                    yield return null;
                    timer += Time.deltaTime;
                    if (timer > textspeed)
                    {
                        k += 1;
                        timer = 0;
                    }

                    if (k > dialogue.sentences[i].GetContent().Length) break;
                }*/
                yield return TypeAsWrite(MainText,dialogue.sentences[i].GetContent(),textspeed);
                i += 1;
            }
        }
        //DialogBox.SetActive(false);
        Destroy(DialogBox);
        flag = true;
        Debug.Log("end dialog");
        GameControl.inputMode = GameControl.InputMode.Game;
    }

    //打字机效果
    //speed指1秒显示多少字
    IEnumerator TypeAsWrite(Text textBox, string content, float speed)
    {
        int i = 0;
        int length = content.Length;
        float timer = 0;
        while (true)
        {
            if (Input.GetMouseButtonDown(1)) { /*Debug.Log("switch sen");*/ i = length; }
            textBox.text = content.Substring(0, i);
            yield return null;
            timer += Time.deltaTime;
            if (timer > 1/speed)
            {
                i += 1;
                timer = 0;
            }

            if (i > length) break;
        }
    }

    IEnumerator test()
    {
        yield return null;
        Debug.Log("test");
    }
}
