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
        public abstract void Use();
        public abstract void Use(Character character);
        public abstract void Use(Item item);

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

        public override void Use()
        {
            throw new System.NotImplementedException();
        }

        public override void Use(Character character)
        {
            //todo
            //计算伤害--投骰子[GameControl.RollDice()]
            //character.ReceiveDmg(character);
            //character.Play("receiveDmg");
        }

        public override void Use(Item item)
        {
            throw new System.NotImplementedException();
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

        public override void Use()
        {
            throw new System.NotImplementedException();
        }

        public override void Use(Character character)
        {
            throw new System.NotImplementedException();
        }

        public override void Use(Item item)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Defence : Skill
    {

        public Defence(GameObject owner)
        {
            Owner = owner;
            SkillName = "防御";
            SkillInfo = "摆出防御姿态";
        }

        public override bool IsActive()
        {
            //todo
            //if(owner.character.receivingDmg)
            //true
            //false
            return false;
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }

        public override void Use(Character character)
        {
            throw new System.NotImplementedException();
        }

        public override void Use(Item item)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Insight : Skill
    {
        public Insight(GameObject owner)
        {
            Owner = owner;
            SkillName = "侦查";
            SkillInfo = "侦查四周";
        }

        public override bool IsActive()
        {
            throw new System.NotImplementedException();
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }

        public override void Use(Character character)
        {
            throw new System.NotImplementedException();
        }

        public override void Use(Item item)
        {
            throw new System.NotImplementedException();
        }
    }

}
