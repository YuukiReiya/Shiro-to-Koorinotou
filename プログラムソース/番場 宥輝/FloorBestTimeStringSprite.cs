using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フロアに表示されるベストタイムの文字
/// 制作者:番場 宥輝
/// </summary>
public class FloorBestTimeStringSprite : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer[] strRenderer;

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
                strRenderer[i].color = FloorSpriteColor.SELECT;
            }
            //未選択のフロア
            else if (i <= StageSelect.ClearFloor[StageSelect.stage])
            {
                strRenderer[i].color = FloorSpriteColor.NONE_SELECT;
            }
            //未開放のフロア
            else 
            {
                strRenderer[i].color = FloorSpriteColor.NONE_OPEN;
            }
        }
    }

}
