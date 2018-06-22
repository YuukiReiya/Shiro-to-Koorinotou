using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトルのコンフィグでメニューのUIのアウトラインを制御するクラス
/// 制作者:番場宥輝
/// </summary>
public class TitleConfigUIOutline : MonoBehaviour {

    [SerializeField]
    private Image[] child;                              //UIのアウトラインを格納する変数
    private Color SelectCr = new Color(1, 1, 1, 1);     //選択中のカラー
    private Color noneSelectCr = new Color(1, 1, 1, 0); //非選択カラー

    /// <summary>
    /// 選択されているメニューに対応したアウトラインを表示する関数
    /// </summary>
    /// <param name="image">選択中のメニュー</param>
    public void SetImage(int image)
    {
        for (int i = 0; i < child.Length; i++)
        {
            if (image == i)
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
