using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemClick : MonoBehaviour
{
    public static Item item;
    public static GameObject itemObj;
    void Start()
    {
        //Button btn = this.GetComponent<Button>();
        //btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GameObject Menu = GameObject.Find("Menu");
        GameObject Skills = GameObject.Find("Skills");
        GameObject Items = GameObject.Find("ItemsMenu");
        GameObject StatusDetail = GameObject.Find("StatusDetail");
        StatusDetail.transform.localPosition = new Vector3(-1300, 0, 0);
        Items.transform.localPosition = new Vector3(0, 0, 0);
        Skills.transform.localPosition = new Vector3(0, -900, 0);
        Menu.transform.localPosition = new Vector3(1000, 0, 0);
    }

    public static void SetItemUI(GameObject itemObj)
    {
        ItemClick.itemObj = itemObj;
        ItemClick.item = itemObj.GetComponent<Item>();
        Text itemName = GameObject.Find("ItemCheckName").GetComponent<Text>();
        Text itemContent = GameObject.Find("ItemCheckContent").GetComponent<Text>();
        Image itemImage = GameObject.Find("ItemCheckImage").GetComponent<Image>();
        itemName.text = item.ItemName;
        itemContent.text = item.ItemIntro;
        //todo item image
    }

    public void GetItem()
    {
        itemObj.SetActive(false);
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.itemList.Add(item);
        GameObject Skills = GameObject.Find("Skills");
        GameObject Menu = GameObject.Find("Menu");
        GameObject Item = GameObject.Find("ItemCheckMenu");
        GameObject Items = GameObject.Find("ItemsMenu");
        GameObject StatusDetail = GameObject.Find("StatusDetail");
        Items.transform.localPosition = new Vector3(0, 918, 0);
        StatusDetail.transform.localPosition = new Vector3(-1300, 0, 0);
        Item.transform.localPosition = new Vector3(0, 918, 0);
        Skills.transform.localPosition = new Vector3(0, -900, 0);
        Menu.transform.localPosition = new Vector3(1000, 0, 0);
        GameControl.inputMode = GameControl.InputMode.Game;
    }
}
