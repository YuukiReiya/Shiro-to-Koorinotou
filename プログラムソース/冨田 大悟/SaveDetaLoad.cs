using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// セーブデータのロードの実行
/// 製作者：冨田 大悟
/// </summary>
public class SaveDetaLoad : MonoBehaviour {

    public TextAsset textAsset;

    private void Awake()
    {
        SaveData.LoadTextFile(textAsset);
    }
}
