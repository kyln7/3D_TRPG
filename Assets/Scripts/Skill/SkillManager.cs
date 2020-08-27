using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgSkill;

public class SkillManager : MonoBehaviour
{
    private List<Skill> skills;
    private List<Skill> activeSkills;

    [Label("殴打")]
    public bool Hit;
    [Label("投掷")]
    public bool Throw;
    // Start is called before the first frame update
    void Start()
    {
        skills = new List<Skill>();
        activeSkills = new List<Skill>();
        if (Hit) skills.Add(new Hit(gameObject));
        if (Throw) skills.Add(new Throw(gameObject));
        skills.Add(new Insight(gameObject));
    }

    // Update is called once per frame
    void Update()
    {

    }

    //获取可用技能列表
    public List<Skill> GetActiveSkills()
    {
        activeSkills.Clear();
        foreach (Skill skill in skills)
        {
            if (skill.IsActive()) activeSkills.Add(skill);
        }
        return activeSkills;
    }
}
