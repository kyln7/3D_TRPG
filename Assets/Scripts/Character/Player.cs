using System.Collections;
using System.Collections.Generic;
using TRpgSkill;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SkillManager skillManager;
    public List<Skill> skills;

    public List<Vector2Int> skillScope;
    public int Strenth;
    //todo
    // Start is called before the first frame update
    void Start()
    {
        skillManager = GetComponent<SkillManager>();
        skills = skillManager.GetActiveSkills();
        foreach(Skill skill in skills)
        {
            if(skill is Throw){
                skillScope = skill.GetScope();
            }
        }
        ShowScope();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScope()
    {
        foreach(Vector2Int node in skillScope)
        {
            GameSystem.GetObjectOnGrid(node, "Ground").transform.Find("select").gameObject.SetActive(true);
        }
    }
}
