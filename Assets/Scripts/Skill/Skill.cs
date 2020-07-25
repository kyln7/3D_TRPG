using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TRpgSkill
{

    public class Skill : MonoBehaviour
    {
        private string skillName;
        private string skillInfo;
        private int range;

        public virtual bool IsActive()
        {
            //todo;
            return false;
        }

        public string SkillName { get => skillName; set => skillName = value; }
        public string SkillInfo { get => skillInfo; set => skillInfo = value; }
        public int Range { get => range; set => range = value; }

    }

    public class Hit : Skill
    {
        public Hit()
        {
            SkillName = "殴打";
            SkillInfo = "让他的脸开花！";
            Range = 1;
        }
        public override bool IsActive()
        {
            Vector2Int pos = new Vector2Int((int)transform.position.x, (int)transform.position.z);
            List<Vector2Int> nodes = GameSystem.GetRange(pos, 1);
            if (GameSystem.HasObjectOnGrids(nodes, "Npc")) return true;
            else return false;
        }
    }

}
