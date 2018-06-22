using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボタン横の矢印2つを制御するクラス
/// 制作者:番場 宥輝
/// </summary>
public class KeyCursolSprite : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer leftArrow;       //左
    [SerializeField]
    SpriteRenderer rightArrow;      //右

    /// <summary>
    /// SetActiveの切り替えを行う関数
    /// </summary>
    /// <param name="active"></param>
    public void Set(bool active)
    {
        leftArrow.gameObject.SetActive(active);
        rightArrow.gameObject.SetActive(active);
    }
}
