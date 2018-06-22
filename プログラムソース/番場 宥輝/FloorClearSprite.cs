using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フロア選択中に出るクリアマークの制御クラス
/// 制作者:番場 宥輝
/// </summary>
public class FloorClearSprite : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer[] clearMark;
    
    /// <summary>
    /// フロア選択中のクリアマークを表示する関数
    /// </summary>
    /// <param name="floor">現在選択しているフロア</param>
    public void Draw(int floor)
    {
        for (int i = 0; i < Menu.FLOOR_NUM; i++)
        {
            //フロアを選択中かつそのフロアがクリアされていればクリアマークを"選択中カラー"にする
            if (i == floor && i < StageSelect.ClearFloor[StageSelect.stage]) 
            {
                clearMark[i].color = FloorSpriteColor.SELECT;
            }
            //解放済フロアで選択されてなければ"非選択カラー"にする
            else if (i < StageSelect.ClearFloor[StageSelect.stage])
            {
                clearMark[i].color = FloorSpriteColor.NONE_SELECT;
            }
            //それ以外は表示しない
            else
            {
                clearMark[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
}
