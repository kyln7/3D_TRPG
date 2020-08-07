using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public List<Sentence> sentences;

    public Dialogue(){ sentences = new List<Sentence>(); }

    public void LoadDialogue(string textname)
    {
        string path = System.IO.Path.Combine("Dialogue",textname);
        
        TextAsset textfile = Resources.Load(path, typeof(TextAsset)) as TextAsset;
        string[] lines = textfile.text.Split('\r');

        foreach (string line in lines)
        {
            string[] sentenceText = line.Split(',');
            if (sentenceText[2] == "content") continue;
            Sentence sentence = new Sentence(sentenceText[0], sentenceText[1], sentenceText[2], sentenceText[3]);
            sentences.Add(sentence);
        }
    }
}

[Serializable]
public class Sentence
{

    string name;
    SType type;
    string content;
    Sprite pic;

    public Sentence() { }
    public Sentence(string n, string t, string c, string picname)
    {
        name = n;
        type = t == "0" ? SType.self : SType.NPC;
        content = c;
        pic = Resources.Load("Sprite/" + picname, typeof(Sprite)) as Sprite; 
    }

    public string getName() { return name; }
    public SType GetSType() { return type; }
    public string getContent() { return content; }
}
public enum SType
{
    self,
    NPC
}
