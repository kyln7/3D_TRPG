using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    //Name of the Item
    private string itemName;
    //Durability of the Item
    [SerializeField]
    private float durability;
    //Priority of the Item When InteractIng
    [SerializeField]
    private int priority;
    //Is this Item Active or passive
    [SerializeField]
    private bool isActive;
    //Power of the Item
    [SerializeField]
    private int power;
    //Can this Item be set as trap
    [SerializeField]
    private int canBeSetTrap;
    //Type of this Item (weapon or Armor)
    [SerializeField]
    enum Type{Weapon,Armor}
    [SerializeField]
    private Type type;
    //Threshold（力量阈值）
    [SerializeField]
    private int threshold;
    [SerializeField]
    private string itemIntro;

    public string ItemName { get => itemName; set => itemName = value; }
    public float Durability { get => durability; set => durability = value; }
    public int Priority { get => priority; set => priority = value; }
    public bool IsActive { get => isActive; set => isActive = value; }
    public int Power { get => power; set => power = value; }
    public int CanBeSetTrap { get => canBeSetTrap; set => canBeSetTrap = value; }
    private Type Type1 { get => type; set => type = value; }
    public int Threshold { get => threshold; set => threshold = value; }
    public string ItemIntro { get => itemIntro; set => itemIntro = value; }

    public Item(string name)
    {
        this.ItemName = name;
    }
}
