﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LabelAttribute:PropertyAttribute
{
    private string name = "";   
    
    public string Name { get { return name; } }

    public LabelAttribute(string name)
    {
        this.name = name;
    }
}
