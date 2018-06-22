using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 各ステージの各フロア名を管理するクラス
/// 制作者:番場宥輝
/// </summary>
public class FloorNameManager : MonoBehaviour {

    [SerializeField]
    TextMeshPro[] floorName;            //フロアの名前
    [SerializeField]
    TextMeshPro[] floorNameUnder;       //フロアの名前下地
   
    //カラーのα値の定数宣言
    readonly float SELECT = 1;
    readonly float NONE_SELECT =0.6f;
    readonly float NONE_OPEN = 0.1f;

    void Start () {
		SetName();
	}
	
    /// <summary>
    /// 名前をセットする関数
    /// </summary>
    public void SetName()
    {
        for(int i=0;i<Menu.FLOOR_NUM;i++)
        {
            floorName[i].text = FloorNameList.FLOOR_NAME[StageSelect.stage][i];
            floorNameUnder[i].text = FloorNameList.FLOOR_NAME[StageSelect.stage][i];
        }
    }
    /// <summary>
    /// 描画関数
    /// </summary>
    /// <param name="floor">選択中のフロア</param>
    public void Draw(int floor)
    {
        for (int i = 0; i < Menu.FLOOR_NUM; i++)
        {
            //選択中のフロア
            if (i == floor)
            {
                floorName[i].color = new Color(floorName[i].color.r, floorName[i].color.g, floorName[i].color.b, SELECT);
                floorNameUnder[i].color = new Color(floorNameUnder[i].color.r, floorNameUnder[i].color.g, floorNameUnder[i].color.b, SELECT);
            }
            //未選択のフロア
            else if (i <= StageSelect.ClearFloor[StageSelect.stage])
            {
                floorName[i].color = new Color(floorName[i].color.r, floorName[i].color.g, floorName[i].color.b, NONE_SELECT);
                floorNameUnder[i].color = new Color(floorNameUnder[i].color.r, floorNameUnder[i].color.g, floorNameUnder[i].color.b, NONE_SELECT);
            }
            //未開放のフロア
            else
            {
                floorName[i].color = new Color(floorName[i].color.r, floorName[i].color.g, floorName[i].color.b, NONE_OPEN); ;
                floorNameUnder[i].color = new Color(floorNameUnder[i].color.r, floorNameUnder[i].color.g, floorNameUnder[i].color.b, NONE_OPEN);
            }
        }
    }

}
