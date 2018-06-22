using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// サウンドのボリュームの表示の制御クラス
/// 制作者:番場宥輝
/// </summary>
public class VolumeUI : MonoBehaviour {

    [SerializeField]
    private Image[] imageVol;                               //音量1～10
    private Color selectCr = new Color(1, 1, 1, 1);         //選択カラー
    private Color NoneSelectCr = new Color(1, 1, 1, 0);     //非選択カラー
	
    /// <summary>
    /// 描画関数
    /// </summary>
    /// <param name="vol">現在の音量</param>
	public void Draw(int vol)
    {
        for (int i = 0; i < imageVol.Length; i++) 
        {
            if (i <= vol) 
            {
                imageVol[i].color = selectCr;
            }
            else
            {
                imageVol[i].color = NoneSelectCr;
            }
        }
    }

}
