using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ステージセレクトのポーズUIのアウトライン制御クラス
/// 制作者:番場宥輝
/// </summary>
public class StageSelectPauseUIOutline : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer[] child;                     //レンダラーの宣言
    private Color SelectCr = new Color(1, 1, 1, 1);     //選択カラー
    private Color noneSelectCr = new Color(1, 1, 1, 0); //非選択カラー

    //定数宣言
    private const int SELECT_MENU = 0;          
    private const int SELECT_SETTING1 = 1;
    private const int SELECT_SETTING2 = 2;

    /// <summary>
    /// スプライトをセットする関数
    /// </summary>
    /// <param name="menu">選択中のメニュー</param>
    /// <param name="high">メニュー深度</param>
    public void SetSprite(int menu,int high)
    {
        for (int i = 0; i < child.Length; i++)
        {
            if (high == SELECT_MENU && menu == i) 
            {
                child[i].color = SelectCr;
            }
            else
            {
                child[i].color = noneSelectCr;
            }
        }
    }
}
