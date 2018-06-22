using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトル画面のメニューの制御クラス
/// 制作者:番場宥輝
/// </summary>
public class MenuUI : MonoBehaviour {

    [SerializeField]
    private Image[] UI;     //各メニューUI

    private Color selectCr = new Color(1, 1, 1, 1);             //選択中カラー
    private Color NoneSelectCr = new Color(0.5f, 0.5f, 0.5f,1); //非選択カラー

    void Start()
    {
        Draw(Menu.START);
    }
    /// <summary>
    /// メニューUIの描画関数
    /// </summary>
    /// <param name="menu"></param>
    public void Draw(int menu)
    {
        for (int i = 0; i <UI.Length; i++)
        {
            if (menu == i) 
            {
                UI[i].color = selectCr;
            }
            else
            {
                UI[i].color = NoneSelectCr;
            }
        }
    }
}
