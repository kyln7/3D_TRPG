using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public List<string> sentences;

    public Dialogue(){
        sentences = new List<string>();
        sentences.Add("This is a new Sentences");
    }

}
