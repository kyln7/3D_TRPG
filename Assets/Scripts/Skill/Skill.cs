using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TRpgSkill
{

    public abstract class Skill : MonoBehaviour
    {
        private GameObject owner;
        private string skillName;
        private string skillInfo;
        private int range;

        public abstract bool IsActive();

        public string SkillName { get => skillName; set => skillName = value; }
        public string SkillInfo { get => skillInfo; set => skillInfo = value; }
        public int Range { get => range; set => range = value; }
        public GameObject Owner { get => owner; set => owner = value; }
    }

    public class Hit : Skill
    {
        public Hit(GameObject owner)
        {
            Owner = owner;
            SkillName = "殴打";
            SkillInfo = "让他的脸开花！";
            Range = 1;
        }
        public override bool IsActive()
        {
            Vector2Int pos = new Vector2Int((int)Owner.transform.position.x, (int)Owner.transform.position.z);
            List<Vector2Int> nodes = GameSystem.GetRange(pos, Range);
            if (GameSystem.HasObjectOnGrids(nodes, "Npc")) return true;
            else return false;
        }
    }

    public class Throw : Skill
    {
        public Throw(GameObject owner)
        {
            Owner = owner;
            SkillName = "投掷";
            SkillInfo = "射他屁股莫古！";
            Range = GetRange(owner);
        }
        public int GetRange(GameObject owner)
        {
            Character character = owner.GetComponent<Character>();
            if (character && character.isActiveAndEnabled)
            {
                return character.Strenth / 10 + 1;
            }
            else return 1;
        }
        public override bool IsActive()
        {
            Vector2Int pos = new Vector2Int((int)Owner.transform.position.x, (int)Owner.transform.position.z);
            List<Vector2Int> nodes = GameSystem.GetRange(pos, Range);
            if (GameSystem.HasObjectOnGrids(nodes, "Npc")) return true;
            else return false;
        }
    }

}
