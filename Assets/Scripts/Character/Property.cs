using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Property
{
    public string name;

    /// <summary>
    /// 基础属性：力量，敏捷，体质，魅力和智力
    /// </summary>
    public int strength;
    public int dexterity;
    public int constitution;
    public int charm;
    public int intelligence;

    /// <summary>
    /// 生存属性：体力，耐力，回复和理智
    /// </summary>
    public int HP;
    int MaxHP;
    public int Stamina;
    int MaxStamina;
    public int Recover;
    public int Mind;
    int MaxMind;

    //随机属性生成函数
    public void SetProperty(int point)
    {
        //这里设置属性的最大值和最小值
        int min = 1;
        int max = 50;

        int[] points = new int[] { min, min, min, min, min };
        point -= 5 * min;
        int maxnum = min;//代表所有属性中的最大值

        while (point > 0)
        {
            int num = Random.Range(0, 5);
            if (points[num] >= max) continue;
            else
            {
                points[num] += 1;
                point -= 1;

                if (maxnum < points[num])
                {
                    maxnum = points[num];
                }
            }
        }

        strength = points[0];
        dexterity = points[1];
        constitution = points[2];
        charm = points[3];
        intelligence = points[4];

        HP = MaxHP = constitution;
        Stamina = MaxStamina = constitution / 2;
        Mind = MaxMind = maxnum;
    }

    //自由分配属性
    public void SetProperty(int a, int b, int c, int d, int e)//, int f, int g, int h, int i)
    {
        strength = a;
        dexterity = b;
        constitution = c;
        charm = d;
        intelligence = e;
    }

    public void SetHP(int Add)
    {
        MaxHP += Add;
    }

    public void SetStamina(int Add)
    {
        MaxStamina += Add;
    }

    public void SetMind(int Add)
    {
        MaxMind += Add;
    }
}