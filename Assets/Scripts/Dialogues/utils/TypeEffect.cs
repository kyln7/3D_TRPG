using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    Image LC, RC;
    Text NameText, MainText;

    int i = 0;

    bool flag = true;
    bool ifSelect = false;
    int selectItemId = 0;
    GameObject ItemsMenu;

    bool flag2 = false;
    bool ifItemUseSelect = false;
    int whichWaySelect = 0;

    public IEnumerator StartDia(GameObject DialogBox, Dialogue dialogue, float textspeed)
    {
        //GameObject DialogBox = GameObject.Find("Canvas").transform.Find("DialogBox").gameObject;
        //生成对话框
        DialogBox.transform.SetParent(GameObject.Find("Canvas").transform);
        DialogBox.GetComponent<RectTransform>().localPosition = new Vector3(0, -385, 0);

        ItemsMenu = GameObject.Find("ItemsMenu");
        int ItemsMenuNum = ItemsMenu.transform.GetSiblingIndex();
        if (ItemsMenuNum == 0) DialogBox.transform.SetAsFirstSibling();
        else DialogBox.transform.SetSiblingIndex(ItemsMenuNum);

        LC = DialogBox.transform.GetChild(0).gameObject.GetComponent<Image>();
        RC = DialogBox.transform.GetChild(1).gameObject.GetComponent<Image>();
        NameText = DialogBox.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        MainText = DialogBox.transform.GetChild(2).GetChild(1).GetComponent<Text>();

        while (true)
        {
            yield return null;
            if (flag || Input.GetMouseButtonDown(0))
            {
                flag = false;
                if (i > dialogue.sentences.Count - 1) break;

                //Debug.Log("Decide Next: " + i);
                if (dialogue.sentences[i].GetSenType() == SenType.normal)
                {
                    yield return DisplaySentence(dialogue, i, textspeed);

                    if (dialogue.sentences[i].GetSkipTo()[0] == -1) i += 1;
                    else i = dialogue.sentences[i].GetSkipTo()[0];

                    continue;
                }

                if (dialogue.sentences[i].GetSenType() == SenType.branch)
                {
                    GameObject canvas = GameObject.Find("Canvas");
                    GameObject branch = Resources.Load("Dialogue/Branch") as GameObject;
                    branch = Instantiate(branch);
                    branch.transform.SetParent(canvas.transform);
                    branch.transform.localPosition = Vector3.zero;


                    string[] bs = dialogue.sentences[i].GetContent().Split('@');

                    int k0 = dialogue.sentences[i].GetSkipTo()[0];
                    branch.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(
                                                 delegate () { i = k0; Debug.Log("i1 = " + i); ifSelect = true; Destroy(branch); });
                    branch.transform.GetChild(0).GetComponentInChildren<Text>().text = bs[0];

                    int k1 = dialogue.sentences[i].GetSkipTo()[1];
                    branch.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(
                                                 delegate () { i = k1; Debug.Log("i2 = " + i); ifSelect = true; Destroy(branch); });
                    branch.transform.GetChild(1).GetComponentInChildren<Text>().text = bs[1];


                    yield return WaitForBranch();
                    //yield return DisplaySentence(dialogue, i, textspeed);
                    continue;
                }

                if (dialogue.sentences[i].GetSenType() == SenType.UseItem)
                {
                    yield return DisplaySentence(dialogue, i, textspeed);
                    GameObject canvas = GameObject.Find("Canvas");
                    GameObject branch = Resources.Load("Dialogue/Branch") as GameObject;
                    branch = Instantiate(branch);
                    branch.transform.SetParent(canvas.transform);
                    branch.transform.localPosition = Vector3.zero;

                    branch.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(
                                                 delegate () { whichWaySelect = 0; ifItemUseSelect = true; Destroy(branch); });
                    branch.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(
                                                 delegate () { whichWaySelect = 1; ifItemUseSelect = true; Destroy(branch); });

                    yield return WaitForItemBranch(dialogue.sentences[i], dialogue.sentences.Count);
                    continue;
                }
            }
        }
        Destroy(DialogBox);
        flag = true;
        i = 0;
        //Debug.Log("end dialog");
        GameControl.inputMode = GameControl.InputMode.Game;
    }

    IEnumerator DisplaySentence(Dialogue dialogue, int i, float textspeed)
    {
        //Debug.Log("start Dialog!");
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

        yield return TypeAsWrite(MainText, dialogue.sentences[i].GetContent(), textspeed);
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
            if (Input.GetMouseButtonDown(1)) { i = length; }
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

    IEnumerator WaitForBranch()
    {
        while (!ifSelect)
        {
            yield return null;
        }
        ifSelect = false;
        flag = true;
    }

    IEnumerator WaitForItemBranch(Sentence sentence, int maxNum)
    {
        while (!ifItemUseSelect)
        {
            yield return null;
        }
        ifItemUseSelect = false;

        if (GameObject.FindWithTag("Player").GetComponent<Player>().itemList.Count == 0)
        {
            i = maxNum - 1;
            flag = true;
        }
        else
        {
            ItemsMenu.transform.localPosition = Vector3.zero;

            int TargetItemNum = sentence.GetItemId().Count;
            if (whichWaySelect == 0)
            {
                List<Item> items = GameObject.FindWithTag("Player").GetComponent<Player>().itemList;
                //ShowItemList
                ItemClick tmp = gameObject.AddComponent<ItemClick>();
                tmp.newObj = Resources.Load("Button") as GameObject;
                tmp.ShowItemUI();
                for (int s = 5; s < ItemsMenu.transform.childCount; s++)
                {
                    int k = s;
                    ItemsMenu.transform.GetChild(s).GetComponent<Button>().onClick.AddListener(delegate { selectItemId = k - 5; });
                }
                ItemsMenu.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate { flag2 = true; });


                //等待选择物品
                while (!flag2)
                {
                    yield return null;
                }
                flag2 = false;
                ItemsMenu.transform.localPosition = new Vector3(2000, 2000, 0);
                foreach (Item item in items)
                {
                    GameObject newItemD = GameObject.Find(item.ItemName + "1");
                    if (newItemD != null)
                    {
                        Destroy(newItemD);
                    }
                }


                int m;
                for (m = 0; m < TargetItemNum; m++)
                {
                    if (items[selectItemId].id == sentence.GetItemId()[m])
                    {

                        i = sentence.GetSkipTo()[m];
                        break;
                    }
                }
                if (m == TargetItemNum) i = sentence.GetSkipTo()[TargetItemNum];
            }
            if (whichWaySelect == 1)
            {
                List<Item> items = GameObject.FindWithTag("Player").GetComponent<Player>().itemList;
                //ShowItemList
                ItemClick tmp = gameObject.AddComponent<ItemClick>();
                tmp.newObj = Resources.Load("Button") as GameObject;
                tmp.ShowItemUI();
                for (int s = 5; s < ItemsMenu.transform.childCount; s++)
                {
                    int k = s;
                    ItemsMenu.transform.GetChild(s).GetComponent<Button>().onClick.AddListener(delegate { selectItemId = k - 5; });
                }
                ItemsMenu.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate { flag2 = true; });


                //等待选择物品
                while (!flag2)
                {
                    yield return null;
                }
                flag2 = false;
                ItemsMenu.transform.localPosition = new Vector3(2000, 2000, 0);
                foreach (Item item in items)
                {
                    GameObject newItemD = GameObject.Find(item.ItemName + "1");
                    if (newItemD != null)
                    {
                        Destroy(newItemD);
                    }
                }

                //瞎写的，后续改正
                if (items[selectItemId].GetComponent<Item>().Power > 10)
                {
                    i = sentence.GetSkipTo()[TargetItemNum + 1];
                }
                else i = sentence.GetSkipTo()[TargetItemNum + 2];
            }
            flag = true;
        }
        
    }
}
