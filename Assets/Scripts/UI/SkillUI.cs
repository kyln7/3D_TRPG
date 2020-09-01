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
            Debug.Log("1");
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        player.ShowScope(skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        player.FinishShowScope(skill);
    }

    public void InsightShowItems()
    {
        player.ShowItems((Insight)skill);
    }
}
