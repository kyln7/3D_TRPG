using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;
using TRpgSkill;

public class GameControl : MonoBehaviour
{
    public static GridArray Map;
    public GameObject player;
    void Start()
    {
        Map = SaveSystem.LoadMapData();
        SkillManager skillManager = player.GetComponent<SkillManager>();
        if (skillManager.isActiveAndEnabled)
        {
            List<Skill> skills = skillManager.GetActiveSkills();
            foreach (Skill skill in skills)
            {
                Debug.Log(skill.SkillName + " : " + skill.SkillInfo);
                if (skill is Hit) Debug.Log("Skill is Hit");
                if (skill is Throw) Debug.Log("Skill is Throw");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
