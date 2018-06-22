using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージセレクトのポーズ画面下に表示される文字列のアウトライン制御クラス
/// 制作者:番場宥輝
/// </summary>
public class StageSelectPauseStringOutline : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer close;       //閉じるのアウトライン
    [SerializeField]
    private SpriteRenderer bgm;         //BGMのアウトライン
    [SerializeField]
    private SpriteRenderer se;          //SEのアウトライン
    [SerializeField]
    private SpriteRenderer jump;        //ジャンプのアウトライン
    [SerializeField]
    private SpriteRenderer magic;       //魔法のアウトライン
    [SerializeField]
    private SpriteRenderer title;       //タイトルに戻るのアウトライン

    //メニュー深度の定数宣言
    const int SELECT_SETTING1 = 1;
    const int SELECT_SETTING2 = 2;

    /// <summary>
    /// アウトラインをセットする関数
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="high"></param>
    public void SetOutline(int menu, int high)
    {
        close.gameObject.SetActive(menu == Menu.CLOSE && high == SELECT_SETTING1);
        bgm.gameObject.SetActive(menu == Menu.SOUND && high == SELECT_SETTING1);
        se.gameObject.SetActive(menu == Menu.SOUND && high == SELECT_SETTING2);
        jump.gameObject.SetActive(menu == Menu.BUTTON && high == SELECT_SETTING1);
        magic.gameObject.SetActive(menu == Menu.BUTTON && high == SELECT_SETTING2);
        title.gameObject.SetActive(menu == Menu.DATA && high == SELECT_SETTING1);
    }

}
