﻿using System.Collections;
using System.Collections.Generic;
using TRpgSkill;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SkillManager skillManager;
    public List<Skill> skills;
    public List<Vector2Int> skillScope;
    public int Strenth;
    public Skill s_inSight;
    public Skill s_hit;
    public Skill s_throw;
    //todo
    // Start is called before the first frame update
    void Start()
    {
        skillManager = GetComponent<SkillManager>();
        SetActiveSkills();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //set skills after enter in a new room or whenever need re-detect active skills
    public void SetActiveSkills()
    {
        skills = skillManager.GetActiveSkills();
        foreach(Skill skill in skills)
        {
            if(skill is Insight) s_inSight = skill;
            if(skill is Hit) s_hit = skill;
            if(skill is Throw) s_throw = skill;
        }
    }

    //show blue area of the skill Scope
    public void ShowScope(Skill skill)
    {
        skillScope = skill.GetScope();
        foreach(Vector2Int node in skillScope)
        {
            GameSystem.GetObjectOnGrid(node, "Ground").transform.Find("select").gameObject.SetActive(true);
        }
    }

    //hide blue area of the skill Scope
    public void FinishShowScope(Skill skill)
    {
        skillScope = skill.GetScope();
        foreach(Vector2Int node in skillScope)
        {
            GameSystem.GetObjectOnGrid(node, "Ground").transform.Find("select").gameObject.SetActive(false);
        }
    }

    //show outline items
    public void ShowItems(Insight insight)
    {
        Material outline = Resources.Load<Material>("Materials/Outline");
        List<GameObject> items = new List<GameObject>();
        items = insight.GetItems();
        foreach(GameObject item in items)
        {
            item.GetComponent<MeshRenderer>().material = outline;
        }
    }
    //hide outline 
    public void FinishShowItems(Insight insight)
    {
        Material df = Resources.Load<Material>("Materials/Default");
        List<GameObject> items = new List<GameObject>();
        items = insight.GetItems();
        foreach(GameObject item in items)
        {
            item.GetComponent<MeshRenderer>().material = df;
        }
    }
}
