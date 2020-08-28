using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnClick : MonoBehaviour
{
    Vector3 position = new Vector3(-200, -384, 0);
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);

    }

    private void OnClick()
    {
        GameObject Status = GameObject.Find("Status");
        GameObject Order = GameObject.Find("Order");
        GameObject Skills = GameObject.Find("Skills");
        Status.transform.localPosition = new Vector3(-200, -570, 0);
        Order.transform.localPosition = position;
        Skills.transform.localPosition = new Vector3(-200, -570, 0);
    }
}
