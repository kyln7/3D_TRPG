using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemUse : MonoBehaviour
{
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
                        //使用木头的情况
                        break;
                    case "石头":
                        //使用石头的情况
                        break;
                    case "散弹枪":
                        //使用散弹枪的情况
                        break;
                }
            }
        }
    }
}
