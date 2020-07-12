using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string name;
    private float height;
    private bool interactable;
    public Item(string name)
    {
        this.Name = name;
    }
    public string Name { get => name; set => name = value; }
}
