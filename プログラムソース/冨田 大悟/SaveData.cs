using System.Collections;
using System.Collections.Generic;

using System.IO; //System.IO.FileInfo, System.IO.StreamReader, System.IO.StreamWriter
using System; //Exception
using System.Text; //Encoding

using UnityEngine;

/// <summary>
/// セーブデータの保存、読み込みの制御
/// 製作者：冨田 大悟
/// </summary>
public static class SaveData{

    static bool loadOne =false;

    static TextAsset textAsset;
    private const string fileName = "Resources/save.txt";

    private static string loadString;


    private static Dictionary<string, float> data = new Dictionary<string, float>();

    /// <summary>
    /// txtにデータを書き込む
    /// </summary>
    public static void AllSave()
    {
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/"+fileName);
        sw = fi.CreateText();
        string text="" ;
        foreach (var item in data)
        {
            text += item.Key;
            text += "\n";
            text += item.Value;
            text += "\n";
        }
        sw.Write(text);
        //閉じる
        sw.Flush();
        sw.Close();
        Debug.Log("save完了");
    }

    /// <summary>
    /// セーブデータのキー値の値を受け取る
    /// </summary>
    /// <param name="str">キー値</param>
    /// <returns>値</returns>
    public static int GetInt(string str)
    {
        float nam;
        if (!data.TryGetValue(str,out nam)){
            nam = 0;
        }
        return (int)nam;
    }

    /// <summary>
    /// セーブデータのキー値の真偽を受け取る
    /// </summary>
    /// <param name="str">キー値</param>
    /// <returns>真偽</returns>
    public static bool GetBool(string str)
    {
        float nam;
        if (!data.TryGetValue(str, out nam))
        {
            nam = 0;
        }
        return nam == 1;
    }

    public static float GetFloat(string str)
    {
        float nam;
        if (!data.TryGetValue(str, out nam))
        {
            nam = 0;
        }
        return nam;
    }
    /// <summary>
    /// キー値の値を変える
    /// </summary>
    /// <param name="str">キー値</param>
    /// <param name="v">値</param>
    public static void SaveInt(string str,int v)
    {
        if (data.ContainsKey(str))
        {
            data[str] = v;
        }
        else
        {
            data.Add(str, v);
        }
    }

    /// <summary>
    /// キー値の値を変える
    /// </summary>
    /// <param name="str">キー値</param>
    /// <param name="v">値</param>
    public static void SaveFloat(string str,float v)
    {
        if (data.ContainsKey(str))
        {
            data[str] = v;
        }
        else
        {
            data.Add(str, v);
        }
    }

    /// <summary>
    /// キー値の値を変える
    /// </summary>
    /// <param name="str">キー値</param>
    /// <param name="v">値</param>
    public static void SaveBool(string str, bool v)
    {
        if (data.ContainsKey(str))
        {
            data[str] = System.Convert.ToInt32(v);
        }
        else
        {
            data.Add(str, System.Convert.ToInt32(v));

        }
    }

    /// <summary>
    /// セーブデータを空にする
    /// </summary>
    public static void deleteSave()
    {
        ////書き込むファイルが既に存在している場合は、上書きする
        //StreamWriter sw = new StreamWriter(
        //    Application.dataPath + "/" + fileName,
        //    false,
        //    Encoding.UTF8);

        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/" + fileName);
        sw = fi.CreateText();

        string text = "";
        sw.Write(text);
        //閉じる
        sw.Flush();
        sw.Close();
    }

    /// <summary>
    /// セーブデータの読み込み
    /// </summary>
    /// <param name="_textAsset">データがなかった時に使う初期値</param>
    public static void LoadTextFile(TextAsset _textAsset)
    {
        if (!loadOne)
        {
            string[] textString = new string[5];

            try
            {
                FileInfo fi = new FileInfo(Application.dataPath + "/" + fileName);
                StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8);
                textString = sr.ReadToEnd().Split(char.Parse("\n"));
            }catch {
                textAsset = _textAsset;
                loadString = textAsset.text;
                textString = loadString.Split(char.Parse("\n"));
            }

            string line = "";
            string key = "";

            for (int i = 0; i < textString.Length; i++)
            {
                line = textString[i];

                if (i % 2 == 0)
                {
                    key = line;

                }
                else
                {
                    data.Add(key, float.Parse(line));
                }
            }
            loadOne = true;
        }


    }
}


