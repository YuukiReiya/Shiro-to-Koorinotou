using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンの名前の定義
/// 製作者：冨田 大悟
/// </summary>
public static class SceneNames  {

    public static string StageSelectName = "StageSelect";
    public static string TitleName = "Title";
    public static string StageLoadName = "LoadScene";
    public static string LoadName = "LoadStageSelectScene";
    public static string OpeningName = "OPScene";

    public static int GetStageNam(string name)
    {
        return int.Parse(name.Substring(0, 1));

    }

    public static int GetFloorNam(string name)
    {
        return int.Parse(name.Substring(2));

    }
}
