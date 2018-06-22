using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトルのコンフィグで文字列の選択中にだすアウトラインの制御クラス
/// 制作者:番場宥輝
/// </summary>
public class TitleConfigStringOutline : MonoBehaviour
{

    [SerializeField]
    private Image close;        //"閉じる"
    [SerializeField]
    private Image bgm;          //"BGM"
    [SerializeField]
    private Image se;           //"SE"
    [SerializeField]
    private Image jump;         //"ジャンプ"
    [SerializeField]
    private Image magic;        //"魔法"
    [SerializeField]
    private Image data;         //"データ"

    //定数宣言
    const int SELECT_SETTING1 = 1;
    const int SELECT_SETTING2 = 2;

    /// <summary>
    /// 選択中のアウトラインを表示する関数
    /// </summary>
    /// <param name="menu">選択中のメニュー</param>
    /// <param name="high">選択中の深度</param>
    public void SetOutline(int menu, int high)
    {
        close.gameObject.SetActive(menu == Menu.CLOSE && high == SELECT_SETTING1);
        bgm.gameObject.SetActive(menu == Menu.SOUND && high == SELECT_SETTING1);
        se.gameObject.SetActive(menu == Menu.SOUND && high == SELECT_SETTING2);
        jump.gameObject.SetActive(menu == Menu.BUTTON && high == SELECT_SETTING1);
        magic.gameObject.SetActive(menu == Menu.BUTTON && high == SELECT_SETTING2);
        data.gameObject.SetActive(menu == Menu.DATA && high == SELECT_SETTING1);
    }

}

