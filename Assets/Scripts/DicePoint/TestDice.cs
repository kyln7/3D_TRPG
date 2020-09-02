using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDice : MonoBehaviour
{
    public GameObject obj1, obj2;
    int dif = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //1代表第一个技能。由于一共19个技能，所以取值范围是0-18.
            //可以加入Extra参数表示检定增益/惩罚
            Debug.Log("模糊检定-单独检定");
            Debug.Log(DicePoint.Instance.BlurCheckOne(obj1, 1)); 

            Debug.Log("模糊检定-对抗检定");
            Debug.Log(DicePoint.Instance.BlurCheckTwo(obj1, obj2, 1));

            Debug.Log("量化检定-单独检定");
            Debug.Log(DicePoint.Instance.QuantifyCheckOne(obj1, 1, out dif));
            Debug.Log("阈值-检定值：" + dif);

            Debug.Log("量化检定-对抗检定");
            Debug.Log(DicePoint.Instance.QuantifyCheckTwo(obj1, obj2, 1, out dif));
            Debug.Log("二者（阈值-检定值)的值的差：" + dif);
        }
    }
}
