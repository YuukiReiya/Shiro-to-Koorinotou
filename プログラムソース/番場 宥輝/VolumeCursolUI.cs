using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// コンフィグの"サウンド"で表示する左右の矢印の制御クラス
/// 制作者:番場宥輝
/// </summary>
public class VolumeCursolUI : MonoBehaviour
{

    [SerializeField]
    private Image left;     //左矢印
    [SerializeField]        
    private Image right;    //右矢印
    private Color select = new Color(1, 1, 1, 1);       //選択カラー
    private Color none = new Color(1, 1, 1, 0);         //非選択カラー

    /// <summary>
    /// アクティブの切り替えを行う関数
    /// </summary>
    /// <param name="flags">true:表示,false:非表示</param>
    public void Set(bool flags)
    {
        left.gameObject.SetActive(flags);
        right.gameObject.SetActive(flags);
    }
    /// <summary>
    /// 描画関数
    /// </summary>
    /// <param name="vol">現在の音量</param>
    public void Draw(int vol)
    {
        if (vol == Menu.MIN_VOL)
        {
            left.color = none;
            right.color = select;
        }
        else if (vol == Menu.MAX_VOL)
        {
            left.color = select;
            right.color = none;
        }
        else
        {
            left.color = select;
            right.color = select;
        }
    }
}
