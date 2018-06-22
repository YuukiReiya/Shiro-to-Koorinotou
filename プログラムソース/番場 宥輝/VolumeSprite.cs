using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボリュームのスプライトの制御クラス
/// 制作者:番場宥輝
/// </summary>
public class VolumeSprite : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer[] srVol;                     //スプライトレンダラー
    private Color SelectCr = new Color(1, 1, 1, 1);     //選択カラー
    private Color noneSelectCr = new Color(1, 1, 1, 0); //非選択カラー

    /// <summary>
    /// アクティブの切り替えを行う関数
    /// </summary>
    /// <param name="active">true:アクティブ,false:非アクティブ</param>
	public void Set(bool active)
    {
        this.gameObject.SetActive(active);
    }
    /// <summary>
    /// 描画関数
    /// </summary>
    /// <param name="vol">現在の音量</param>
    public void Draw(int vol)
    {
        //音量
        for (int i = 0; i < srVol.Length; i++)
        {
            if (i <= vol)
            {
                srVol[i].color = SelectCr;
            }
            else
            {
                srVol[i].color = noneSelectCr;
            }
        }
    }
}
