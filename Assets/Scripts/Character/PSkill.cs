using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//代表各个技能的等级:
//力量系：斗殴，投掷，搬运，防御
//敏捷系：闪避，跳跃，攀爬
//智力系：枪械使用，侦察，聆听，查阅，急救，心理学，精密操作
//魅力系：恐吓，说服，话术，魅惑
//体质系：抵抗
[System.Serializable]
public class PSkill
{
    public int Brawl;
    public int Throw;
    public int Carry;
    public int Defend;

    public int Evade;
    public int Jump;
    public int Climb;

    public int GunUse;
    public int Spy;
    public int Listen;
    public int LookUp;
    public int Rescue;
    public int Psychology;
    public int AccuracyOperate;

    public int Threaten;
    public int Persuade;
    public int Speech;
    public int Charm;

    public int Resist;

    public void SetPSkill(int point=200)
    {
        //这里设置属性的最大值和最小值
        int min = 1;
        int max = 40;

        int[] points = new int[19];
        for (int i = 0; i < 19; i++) points[i] = min;
        point -= 19 * min;
        int maxnum = min;//代表所有属性中的最大值

        while (point > 0)
        {
            int num = Random.Range(0, 19);
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

        Brawl = points[0];
        Throw = points[1];
        Carry = points[2];
        Defend = points[3];

        Evade = points[4];
        Jump = points[5];
        Climb = points[6];

        GunUse = points[7];
        Spy = points[8];
        Listen = points[9];
        LookUp = points[10];
        Rescue = points[11];
        Psychology = points[12];
        AccuracyOperate = points[13];

        Threaten = points[14];
        Persuade = points[15];
        Speech = points[16];
        Charm = points[17];

        Resist = points[18];

        int sum = 0;
        for (int i = 0; i < 19; i++) sum += points[i];
        Debug.Log(sum);
    }
}
