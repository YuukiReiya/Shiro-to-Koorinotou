using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// セーブのテスト
/// 製作者：冨田 大悟
/// </summary>
public class SaveTest : MonoBehaviour {

    public TextAsset textAsset;

    public bool deleteFlag = false;

    public int[] nam = new int[1];
    public string[] key = new string[1];

    [ContextMenu("t")]

    void t()
    {
        Debug.Log("実行");

        SaveData.LoadTextFile(textAsset);

        for(int i = 0; i < key.Length; i++)
        {
            SaveData.SaveInt(key[i], nam[i]);
        }
        SaveData.AllSave();
        
    }

    [ContextMenu("DeleteText")]
    void DeleteText()
    {
        if (deleteFlag)
        {
            deleteFlag = false;
            SaveData.deleteSave();
        }
    }

    [ContextMenu("stateSaveTest")]
    void StartSave()
    {
        SaveData.LoadTextFile(textAsset);

        for (int i = 1; i < 6; i++)
        {
            for(int j = 1; j < 7; j++)
            {
                
                string key = "Stage" + i +"-"+ j + "ClearTime";
                SaveData.SaveFloat(key,99.99f);
            }
        }
        SaveData.AllSave();
    }
}
