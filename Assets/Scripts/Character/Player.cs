﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Property p;

    public PSkill pskill;

    private void Start()
    {
        p.name = "主角";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            p.SetProperty(180);
            pskill.SetPSkill();
        }
    }
}
