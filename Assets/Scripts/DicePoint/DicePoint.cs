using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePoint
{
    private static DicePoint instance;
    public static DicePoint Instance
    {
        get
        {
            if (instance == null)
                instance = new DicePoint();
            return instance;
        }
    }

    //模糊检定-单独检定
    public DiceResult BlurCheckOne(GameObject obj, int num, ExtraEffect extra = ExtraEffect.Nothing)
    {
        int dif;
        return DiceCheck(obj, num, out dif, extra);
    }

    //模糊检定-对抗检定
    //返回值：成功的obj序号+成功结果
    public (int,DiceResult) BlurCheckTwo(GameObject obj1, GameObject obj2, int num, 
                                         ExtraEffect extra = ExtraEffect.Nothing)
    {
        int dif1, dif2;
        DiceResult diceResult1 = DiceCheck(obj1, num, out dif1, extra);
        DiceResult diceResult2 = DiceCheck(obj2, num, out dif2, extra);

        return dif1 >= dif2 ? (0, DiceResult.Success) : (1, DiceResult.Success);
    }



    //量化检定-单独检定
    public DiceResult QuantifyCheckOne(GameObject obj, int num, out int dif,
                                       ExtraEffect extra = ExtraEffect.Nothing)
    {
        return DiceCheck(obj, num, out dif, extra);
    }

    //量化检定-对抗检定
    //返回值：成功的obj序号+成功结果
    //out：双方dif值（阈值-检定值）之差
    public (int,DiceResult) QuantifyCheckTwo(GameObject obj1, GameObject obj2, int num, out int dif,
                                             ExtraEffect extra = ExtraEffect.Nothing)
    {
        int dif1, dif2;
        DiceResult diceResult1 = DiceCheck(obj1, num, out dif1, extra);
        DiceResult diceResult2 = DiceCheck(obj2, num, out dif2, extra);

        dif = dif1 - dif2;
        return dif1 >= dif2 ? (0, DiceResult.Success) : (1, DiceResult.Success);
    }




    #region Utils
    //计算阈值
    public int CalcThresHold(Property p, PSkill pskill, int num)
    {
        int result = 0;
        switch (num)
        {
            case 0: result += p.strength; result += pskill.Brawl; break;
            case 1: result += p.strength; result += pskill.Throw; break;
            case 2: result += p.strength; result += pskill.Carry; break;
            case 3: result += p.strength; result += pskill.Defend; break;

            case 4: result += p.dexterity; result += pskill.Evade; break;
            case 5: result += p.dexterity; result += pskill.Jump; break;
            case 6: result += p.dexterity; result += pskill.Climb; break;

            case 7: result += p.intelligence; result += pskill.GunUse; break;
            case 8: result += p.intelligence; result += pskill.Spy; break;
            case 9: result += p.intelligence; result += pskill.Listen; break;
            case 10: result += p.intelligence; result += pskill.LookUp; break;
            case 11: result += p.intelligence; result += pskill.Rescue; break;
            case 12: result += p.intelligence; result += pskill.Psychology; break;
            case 13: result += p.intelligence; result += pskill.AccuracyOperate; break;

            case 14: result += p.charm; result += pskill.Threaten; break;
            case 15: result += p.charm; result += pskill.Persuade; break;
            case 16: result += p.charm; result += pskill.Speech; break;
            case 17: result += p.charm; result += pskill.Charm; break;

            case 18: result += p.constitution; result += pskill.Resist; break;
            default: Debug.Log("输入不正确！范围为(0-18)."); break;
        }

        return result;
    }

    //计算检定结果
    DiceResult DiceCheck(GameObject obj, int num, out int dif, ExtraEffect extra = ExtraEffect.Nothing)
    {
        Property p = new Property();
        PSkill pskill = new PSkill();
        if (obj.GetComponent<NPC>())
        {
            NPC npc = obj.GetComponent<NPC>();
            p = npc.p;
            pskill = npc.pskill;
        }
        else
        {
            Player player = obj.GetComponent<Player>();
            /*
            p = player.p;
            pskill = player.pskill;*/
        }

        DiceResult d = DiceResult.Nothing;
        int threshold = CalcThresHold(p, pskill, num);
        int checkPoint = Random.Range(1, 101);

        switch (extra)
        {
            case ExtraEffect.Add: checkPoint += 0;break;
            case ExtraEffect.Sub: checkPoint -= 0; break;
            case ExtraEffect.Mul: checkPoint *= 1; break;
            case ExtraEffect.Div: checkPoint /= 1; break;
            case ExtraEffect.Bonus:
                int checkPoint1 = Random.Range(1, 101);
                checkPoint = checkPoint > checkPoint1 ? checkPoint : checkPoint1;
                break;
            case ExtraEffect.Punishment:
                int checkPoint2 = Random.Range(1, 101);
                checkPoint = checkPoint < checkPoint2 ? checkPoint : checkPoint2;
                break;
            default:break;
        }

        dif = threshold - checkPoint;

        if (checkPoint >= 1 && checkPoint <= 5) d =  DiceResult.BigSuccess;
        else if (checkPoint < threshold / 5) d =  DiceResult.ExtremeSuccess;
        else if (checkPoint < threshold / 2) d =  DiceResult.HardSuccess;
        else if (checkPoint <= threshold) d =  DiceResult.Success;
        else if (checkPoint > threshold) d =  DiceResult.Failure;
        else if (checkPoint >= 96 && checkPoint <= 100) d =  DiceResult.BigFailure;

        if (extra==ExtraEffect.Desperation && (d==DiceResult.Failure || d == DiceResult.BigFailure))
        {
            //跳出选择，确定是否再进行一次
            //
            //Undo

            //如果再次失败，则全为大失败
            d = DiceCheck(obj, num, out dif);
            if (d == DiceResult.Failure || d == DiceResult.BigFailure) d = DiceResult.BigFailure;
        }

        Debug.Log("阈值为：" + threshold);
        Debug.Log("检定值为：" + checkPoint);

        return d;
    }
    #endregion
}

public enum DiceResult
{
    BigSuccess,
    ExtremeSuccess,
    HardSuccess,
    Success,
    Failure,
    BigFailure,
    Nothing
}

public enum ExtraEffect
{
    Add,
    Sub,
    Mul,
    Div,
    Bonus,
    Punishment,
    Desperation,
    Nothing
}

