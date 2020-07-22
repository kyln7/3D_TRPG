using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //Name of the Item
    private string itemName;
    //Durability of the Item
    private float durability;
    //Priority of the Item When InteractIng
    private int priority;
    //Is this Item Active or passive
    private bool isActive;
    //Power of the Item
    private int power;
    //Can this Item be set as trap
    private int canBeSetTrap;
    //Type of this Item (weapon or Armor)
    enum Type{Weapon,Armor}
    private Type type;
    //Threshold（力量阈值）
    private int threshold;

    public string ItemName { get => ItemName; set => ItemName = value; }
    public float Durability { get => durability; set => durability = value; }
    public int Priority { get => priority; set => priority = value; }
    public bool IsActive { get => isActive; set => isActive = value; }
    public int Power { get => power; set => power = value; }
    public int CanBeSetTrap { get => canBeSetTrap; set => canBeSetTrap = value; }
    private Type Type1 { get => type; set => type = value; }
    public int Threshold { get => threshold; set => threshold = value; }

    public Item(string name)
    {
        this.ItemName = name;
    }
}
