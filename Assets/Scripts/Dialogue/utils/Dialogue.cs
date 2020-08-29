//读取并保存对话信息
using System;
using System.Collections.Generic;
using UnityEngine;

using Excel;
using System.IO;
using System.Data;

[Serializable]
public class Dialogue
{
    public List<Sentence> sentences = new List<Sentence>();//每一行对话
    public Dictionary<string, Sprite> LoadedPics = new Dictionary<string, Sprite>();//预加载的图片资源

    public Dialogue(){ sentences = new List<Sentence>(); }

    public bool LoadDialogue(string textname)
    {
        string path = Path.Combine("Dialogue",textname);

        DataSet result;
        //读取xlsx文件
        try
        {
            FileStream stream = File.Open(Application.dataPath + "/Resources/Dialogue/" + textname + ".xlsx", FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            result = excelReader.AsDataSet();
        }
        catch (FileNotFoundException)
        {
            Debug.Log("找不到xlsx文件：" + textname);
            return false;
        }

        //预加载图片
        string PicToBeLoad = result.Tables[0].Rows[0][1].ToString();
        //Debug.Log(result.Tables[0].Rows[0][1]);
        string[] pnames = PicToBeLoad.Split(',');
        for (int i = 0; i < pnames.Length; i++)
        {
            Sprite tmp = Resources.Load("Character/" + pnames[i], typeof(Sprite)) as Sprite;
            if (tmp==null) { Debug.Log("找不到图片：" + pnames[i].ToString()); return false; }
            LoadedPics.Add(pnames[i], tmp);
        }

        for (int i = 2; i < result.Tables[0].Rows.Count; i++)
        {
            DataRow sentenceText = result.Tables[0].Rows[i];
            Sentence sentence = new Sentence(sentenceText[0].ToString(), sentenceText[1].ToString(),
                                             sentenceText[2].ToString(), sentenceText[3].ToString());
            sentences.Add(sentence);
        }

        Debug.Log("对话文件加载完毕。");
        return true;
    }
}

[Serializable]
public class Sentence
{
    string name;
    string speaker;
    string content;
    List<string> picnames;

    public Sentence() { }
    public Sentence(string n, string s, string c, string picset)
    {
        name = n;
        speaker = s;
        content = c;

        picnames = new List<string>();
        string[] tmp = picset.Split(',');
        for (int i = 0; i < tmp.Length; i++)
        {
            picnames.Add(tmp[i]);
        }
    }

    public string GetName() { return name; }
    public string GetSpeaker() { return speaker; }
    public string GetContent() { return content; }
    public List<string> GetPicnames() { return picnames; }
}

