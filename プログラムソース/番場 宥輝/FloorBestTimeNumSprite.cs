using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フロア選択時に映るベストタイムの制御クラス
/// 制作者:番場 宥輝
/// </summary>
public class FloorBestTimeNumSprite : MonoBehaviour
{

    /// <summary>
    /// 配列の添字はfloorの数だけあり、添え字にフロアが対応してる
    /// </summary>
    [SerializeField]
    private SpriteRenderer[] placeTen;      //10の位
    [SerializeField]
    private SpriteRenderer[] placeOne;      //1の位
    [SerializeField]
    private SpriteRenderer[] dot;           //小数点
    [SerializeField]
    private SpriteRenderer[] decimalOne;    //小数第一位
    [SerializeField]
    private SpriteRenderer[] decimalTwo;    //小数第二位
    [SerializeField]
    private Sprite[] number;                //数字のスプライトを格納する

    /// <summary>
    /// ベストタイムをセーブデータから読み込み設定する
    /// </summary>
    public void SetBestTime()
    {
        float temp;//読み込んだベストタイムを一時的に格納する変数
        for (int i = Menu.FLOOR1; i <= Menu.FLOOR6; i++)
        {
            temp = SaveData.GetFloat("Stage" + (StageSelect.stage + 1) + "-" + (i + 1) + "ClearTime");
            placeTen[i].sprite = number[(int)(temp / 10) % 10]; //10の位
            placeOne[i].sprite = number[(int)temp % 10];        //1の位
            temp *= 10;                                         
            decimalOne[i].sprite = number[(int)temp % 10];
            temp *= 10;
            decimalTwo[i].sprite = number[(int)temp % 10];
        }
    }
    /// <summary>
    /// 選択中フロア、未選択フロア、未開放フロアで色を変える
    /// </summary>
    /// <param name="floor">選択中のフロア</param>
    public void Draw(int floor)
    {
        for (int i = 0; i < Menu.FLOOR_NUM; i++)
        {
            //選択中のフロア
            if (i == floor)
            {
                placeTen[i].color = FloorSpriteColor.SELECT;
                placeOne[i].color = FloorSpriteColor.SELECT;
                dot[i].color = FloorSpriteColor.SELECT;
                decimalOne[i].color = FloorSpriteColor.SELECT;
                decimalTwo[i].color = FloorSpriteColor.SELECT;
            }
            //未選択のフロア
            else if (i <= StageSelect.ClearFloor[StageSelect.stage])
            {
                placeTen[i].color = FloorSpriteColor.NONE_SELECT;
                placeOne[i].color = FloorSpriteColor.NONE_SELECT;
                dot[i].color = FloorSpriteColor.NONE_SELECT;
                decimalOne[i].color = FloorSpriteColor.NONE_SELECT;
                decimalTwo[i].color = FloorSpriteColor.NONE_SELECT;
            }
            //未開放のフロア
            else// if (i >= StageSelect.openfloor[StageSelect.stage])
            {
                placeTen[i].color = FloorSpriteColor.NONE_OPEN;
                placeOne[i].color = FloorSpriteColor.NONE_OPEN;
                dot[i].color = FloorSpriteColor.NONE_OPEN;
                decimalOne[i].color = FloorSpriteColor.NONE_OPEN;
                decimalTwo[i].color = FloorSpriteColor.NONE_OPEN;
            }
        }
    }
}
