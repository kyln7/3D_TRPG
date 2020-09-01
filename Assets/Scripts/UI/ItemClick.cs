using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemClick : MonoBehaviour
{
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
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
}
