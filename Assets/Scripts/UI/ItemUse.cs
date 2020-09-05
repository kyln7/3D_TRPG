using System.Collections;
using System.Collections.Generic;
using TRpgSkill;
using UnityEngine;
using UnityEngine.UI;
public class ItemUse : MonoBehaviour
{
    private GameObject player, hitObj;
    private Skill skill;
    public ItemClick itemClick;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    public void OnClick()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        List<Item> items = player.itemList;
        foreach (Item item in items)
        {
            Text itemName = GameObject.Find("ItemName").GetComponent<Text>();
            if (itemName.text == item.name)
            {
                Debug.Log("Use " + itemName.text);
                switch (item.name)
                {
                    case "木头":
                        //使用木头的情况\
                        UseItemToAttack(item);
                        break;
                    case "石头":
                        //使用石头的情况
                        UseItemToAttack(item);
                        break;
                    case "散弹枪":
                        //使用散弹枪的情况
                        UseItemToAttack(item);
                        break;
                }
            }
        }
    }
    public void SetPara(GameObject p, GameObject h, Skill s)
    {
        player = p;
        hitObj = h;
        skill = s;
        itemClick.ShowItemUI();
    }
    public void UseItemToAttack(Item item)
    {
        var res = DicePoint.Instance.BlurCheckTwo(player, hitObj, 0);
        if (res.Item1 == 0)
        {
            Debug.Log("hit" + hitObj.gameObject.name);
            if (res.Item2 == DiceResult.Success) hitObj.GetComponent<NPC>().p.HP -= (10 + item.Power);
        }
        player.GetComponent<Player>().FinishShowScope(skill);
        GameControl.inputMode = GameControl.InputMode.Game;
        ReturnClick.Return();
    }

    public void ShowSelectItemUI()
    {
        itemClick.ShowItemUI();
    }
}
