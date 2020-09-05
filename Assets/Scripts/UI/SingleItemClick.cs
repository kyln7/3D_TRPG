using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SingleItemClick : MonoBehaviour
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
            if(name == item.name+"1")
            {
                Text itemName = GameObject.Find("ItemName").GetComponent<Text>();
                Text itemContent = GameObject.Find("ItemContent").GetComponent<Text>();
                Image itemImage = GameObject.Find("ItemImage").GetComponent<Image>();
                itemName.text = item.ItemName;
                itemContent.text = item.ItemIntro;
            }
        }
    }
}
