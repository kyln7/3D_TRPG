using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TRpgSkill;

public class SkillUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Player player;
    public Skill skill;
    public string skillName;
    private void Start()
    {
        if (skillName == "Insight")
        {
            skill = player.GetInsight();
        }
        if (skillName == "Hit")
        {
            skill = player.GetHit();
        }
        if (skillName == "Check")
        {
            skill = player.GetCheck();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Player.isMoving)
            player.ShowScope(skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Player.isMoving)
            player.FinishShowScope(skill);
    }

    public void InsightShowItems()
    {
        if(!Player.isMoving)
            player.ShowItems((Insight)skill);
    }

    public void GetObj()
    {
        if (!Player.isMoving)
        {
            GameObject Skills = GameObject.Find("Skills");
            GameObject Menu = GameObject.Find("Menu");
            GameObject Items = GameObject.Find("ItemsMenu");
            GameObject ItemInfo = GameObject.Find("ItemCheckMenu");
            GameObject StatusDetail = GameObject.Find("StatusDetail");
            StatusDetail.transform.localPosition = new Vector3(-1300, 0, 0);
            Items.transform.localPosition = new Vector3(0, 918, 0);
            ItemInfo.transform.localPosition = new Vector3(0, 918, 0);
            Skills.transform.localPosition = new Vector3(0, -900, 0);
            Menu.transform.localPosition = new Vector3(1000, 0, 0);
            GameControl.inputMode = GameControl.InputMode.SelectObj;
            player.SetSkillScope(skill);
            player.usingSkill = skill;
        }
    }
}
