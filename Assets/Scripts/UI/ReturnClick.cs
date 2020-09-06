using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnClick : MonoBehaviour
{
    public static bool isHit;
    public ItemUse itemUse;
    void Start()
    {
        isHit = false;
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);

    }

    public void OnClick()
    {
        GameObject Skills = GameObject.Find("Skills");
        GameObject Menu = GameObject.Find("Menu");
        GameObject Items = GameObject.Find("ItemsMenu");
        GameObject StatusDetail = GameObject.Find("StatusDetail");
        GameObject Item = GameObject.Find("ItemCheckMenu");
        Item.transform.localPosition = new Vector3(0, 918, 0);
        StatusDetail.transform.localPosition = new Vector3(-1300, 0, 0);
        Items.transform.localPosition = new Vector3(0, 918, 0);
        Skills.transform.localPosition = new Vector3(0, -900, 0);
        Menu.transform.localPosition = new Vector3(1000, 0, 0);
        GameControl.inputMode = GameControl.InputMode.Game;

        Player player = GameObject.Find("Player").GetComponent<Player>();
        List<Item> items = player.itemList;
        foreach (Item item in items)
        {
            GameObject newItemD = GameObject.Find(item.ItemName + "1");
            if (newItemD != null)
            {
                Destroy(newItemD);
            }
        }
        if (isHit)
        {
            itemUse.NoItemAttack();
        }
    }

    public static void Return()
    {
        GameObject Skills = GameObject.Find("Skills");
        GameObject Menu = GameObject.Find("Menu");
        GameObject Items = GameObject.Find("ItemsMenu");
        GameObject StatusDetail = GameObject.Find("StatusDetail");
        GameObject Item = GameObject.Find("ItemCheckMenu");
        Item.transform.localPosition = new Vector3(0, 918, 0);
        StatusDetail.transform.localPosition = new Vector3(-1300, 0, 0);
        Items.transform.localPosition = new Vector3(0, 918, 0);
        Skills.transform.localPosition = new Vector3(0, -900, 0);
        Menu.transform.localPosition = new Vector3(1000, 0, 0);
        GameControl.inputMode = GameControl.InputMode.Game;

        Player player = GameObject.Find("Player").GetComponent<Player>();
        List<Item> items = player.itemList;
        foreach (Item item in items)
        {
            GameObject newItemD = GameObject.Find(item.ItemName + "1");
            if (newItemD != null)
            {
                Destroy(newItemD);
            }
        }
    }
}
