using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フロアの後ろの背景を現在選択中のステージに合わせ変更するクラス
/// 制作者:番場 宥輝
/// </summary>
public class FloorBackSprite : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer[] backRenderer;//フロアの背景画像
    /// <summary>
    /// ステージ毎のフロアの背景画像を多次元配列を用いて格納するためのクラス
    /// </summary>
    [System.SerializableAttribute]
    public class BackImageList
    {
        //各フロアをstageに合わせ1~6まで格納
        public List<Sprite> Stage = new List<Sprite>();

        public BackImageList(List<Sprite> Floor)
        {
            Stage = Floor;
        }
    }
    [SerializeField]
    private List<BackImageList> floorBackImage = new List<BackImageList>();//各ステージの各フロアのスプライトを格納するリスト
	/// <summary>
    /// 選択中のステージに合わせた背景をセットする
    /// </summary>
	public void SetImage()
    {
        //spriteのimageを現在のステージに合わせ変更
        //※StageSelect.stageは参照のみ
        for (int i = 0; i < Menu.FLOOR_NUM; i++)
        {
            backRenderer[i].sprite = floorBackImage[StageSelect.stage].Stage[i];
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
                backRenderer[i].color = FloorSpriteColor.SELECT;
            }
            //未選択のフロア
            else if (i <= StageSelect.ClearFloor[StageSelect.stage])
            {
                
                backRenderer[i].color = FloorSpriteColor.NONE_SELECT;
            }
            //未開放のフロア
            else //if (i >= StageSelect.ClearFloor[StageSelect.stage])
            {
                backRenderer[i].color = FloorSpriteColor.NONE_OPEN;
            }
        }
    }
}
