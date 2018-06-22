using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キーコンフィグの際、ジャンプと魔法の横に出る矢印の制御クラス
/// 制作者:番場宥輝
/// </summary>
public class KeyCursolUI : MonoBehaviour {

    [SerializeField]
    private Image left;     //左矢印
    [SerializeField]
    private Image right;    //右矢印
    private Color selectCr = new Color(1, 1, 1, 1);     //表示
    private Color NoneSelectCr = new Color(1, 1, 1, 0); //非表示
    /// <summary>
    /// 表示・非表示を行う関数
    /// </summary>
    /// <param name="flags">true:表示,false:非表示</param>
    public void SetCursol(bool flags)
    {
        if (flags)
        {
            left.color = selectCr;
            right.color = selectCr;
        }
        else
        {
            left.color = NoneSelectCr;
            right.color = NoneSelectCr;
        }

    }
	
}
