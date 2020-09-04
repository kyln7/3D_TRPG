using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillClick : MonoBehaviour
{
    Vector3 position = new Vector3(0, 40, 0);
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        
    }

    public void OnClick()
    {
        GameObject Skills = GameObject.Find("Skills");
        GameObject Menu = GameObject.Find("Menu");
        GameObject Items = GameObject.Find("ItemsMenu");
        Items.transform.localPosition = new Vector3(0, 918, 0);
        Skills.transform.localPosition = position;
        Menu.transform.localPosition = new Vector3(1000, 0, 0);
    }

}
