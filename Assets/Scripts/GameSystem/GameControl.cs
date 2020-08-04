using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRpgMap;
using TRpgSkill;
using TRpgAction;
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
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = GameSystem.GetGameObjectByMouse("Ground");
            if (obj == null) return;
            Vector2Int endPos = new Vector2Int(Map.GetGridPos(obj)[0], Map.GetGridPos(obj)[1]);
            Character character = player.GetComponent<Character>();
            character.walkPos = endPos;
            character.Actions.Enqueue(new MoveAction(endPos));
        }
    }
}
