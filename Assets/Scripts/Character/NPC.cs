using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Property p;
    public PSkill pskill;
    public int favor;

    // Start is called before the first frame update

    void Start()
    {
        p.name = "NPC";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            p.SetProperty(180);
            pskill.SetPSkill();
        }
    }
}