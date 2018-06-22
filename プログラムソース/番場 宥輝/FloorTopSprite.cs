using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フロア選択の際の一番上の屋根の表示を制御するクラス
/// 制作者:番場 宥輝
/// </summary>
public class FloorTopSprite : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer top;         //屋根のレンダー
    [SerializeField]
    private Sprite[] spriteList;        //各ステージの屋根のスプライト

    /// <summary>
    /// 現在選択してるステージに合わせた屋根のスプライトをセット
    /// </summary>
    public void SetImage()
    {
        top.sprite = spriteList[StageSelect.stage];
    }
    /// <summary>
    /// フロア選択の一番上の屋根の色を変化させる
    /// </summary>
    /// <param name="floor">現在選択中のフロア</param>
    public void Draw(int floor)
    {
        //フロア6が選択されており、
        if (floor == Menu.FLOOR6)
        {
            top.color = FloorSpriteColor.SELECT;
        }
        //選択したステージのフロア6解放済で非選択状態
        else if (Menu.FLOOR5 <= StageSelect.ClearFloor[StageSelect.stage])
        {
            top.color = FloorSpriteColor.NONE_SELECT;
        }
        //フロア6未開放
        else
        {
            top.color = FloorSpriteColor.NONE_OPEN;
        }
    }
}
