using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 選択中フロア、未選択フロア、未開放フロアの色の定数宣言
/// (読み取り専用)
/// 制作者:番場 宥輝
/// </summary>
public static class FloorSpriteColor
{

    //選択中のカラー
    public static readonly Color SELECT = new Color(1, 1, 1, 1);

    //未選択のカラー
    public static readonly Color NONE_SELECT = new Color(0.6f, 0.6f, 0.6f, 1);

    //未開放のカラー
    public static readonly Color NONE_OPEN = new Color(0.1f, 0.1f, 0.1f, 1);
}
