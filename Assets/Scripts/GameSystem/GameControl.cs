using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;
using TRpgSkill;

public class GameControl : MonoBehaviour
{
    public static GridArray Map;
    public SkillManager skillManager;
    void Start()
    {
        Map = SaveSystem.LoadMapData();
        List<Skill> skills = skillManager.GetActiveSkills();
        foreach (Skill skill in skills)
        {
            Debug.Log(skill.SkillName + " : " + skill.SkillInfo);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
