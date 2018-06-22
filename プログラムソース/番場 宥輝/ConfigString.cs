using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 選択されたメニューに対応した文字列(オブジェクト群)の表示・非表示をするクラス
/// 制作者:番場宥輝
/// </summary>
public class ConfigString : MonoBehaviour {

    [SerializeField]
    private GameObject[] StringObj;       //各メニューのゲームオブジェクト

    /// <summary>
    /// 文字列のセットを行う関数
    /// </summary>
    /// <param name="configMenu">現在選択中のメニュー</param>
    public void SetString(int configMenu)
    {
        for (int i = 0; i < StringObj.Length; i++)
        {
            StringObj[i].gameObject.SetActive(i == configMenu);
        }
    }
}
