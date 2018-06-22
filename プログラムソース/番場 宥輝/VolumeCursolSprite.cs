using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボリュームの横に出る矢印の制御クラス
/// 制作者:番場宥輝
/// </summary>
public class VolumeCursolSprite : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer leftArrow;                   //左
    [SerializeField]                            
    private SpriteRenderer rightArrow;                  //右
    private Color SelectCr = new Color(1, 1, 1, 1);     //選択カラー
    private Color noneSelectCr = new Color(1, 1, 1, 0); //非選択カラー

    /// <summary>
    /// アクティブの切り替えを行う関数
    /// </summary>
    /// <param name="active"></param>
    public void Set(bool active)
    {
        leftArrow.gameObject.SetActive(active);
        rightArrow.gameObject.SetActive(active);
    }
    /// <summary>
    /// 描画関数
    /// </summary>
    /// <param name="vol"></param>
    public void Draw(int vol)
    {
        //カーソル
        if (vol == 0)
        {
            leftArrow.color = noneSelectCr;
            rightArrow.color = SelectCr;
        }
        else if (vol == 10)
        {
            leftArrow.color = SelectCr;
            rightArrow.color = noneSelectCr;
        }
        else
        {
            leftArrow.color = SelectCr;
            rightArrow.color = SelectCr;
        }
    }
}
