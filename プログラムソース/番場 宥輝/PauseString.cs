using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズ画面下にでる文字列を制御するクラス
/// 制作者:番場 宥輝
/// </summary>
public class PauseString : MonoBehaviour {

    [SerializeField]
    private GameObject[] StringObj;
    /// <summary>
    /// 指定のメニューの文字列をスプライトに設定する
    /// </summary>
    /// <param name="pauseMenu">選択メニュー</param>
    public void SetString(int pauseMenu)
    {
        for (int i = 0; i < StringObj.Length; i++)
        {
            StringObj[i].gameObject.SetActive(i == pauseMenu);
        }
    }
}
