using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キーコンフィグの際表示するボタンのUIの制御クラス
/// 制作者:番場宥輝
/// </summary>
public class KeyUI : MonoBehaviour {

    [SerializeField]
    private Image keyRen;   //UI(レンダラー)
    [SerializeField]
    private Sprite[] key;   //キー

    /// <summary>
    /// 選択中のキーに対応したボタンに切り替える関数
    /// </summary>
    /// <param name="index">選択してるキー</param>
    public void Draw(int index)
    {
        if (0 <= index && index < key.Length) 
        {
            keyRen.sprite = key[index];
        }
        //範囲外参照されたら0番目に格納されたイメージに切り替える
        else
        {
            keyRen.sprite = key[0];
        }
    }
	
}
