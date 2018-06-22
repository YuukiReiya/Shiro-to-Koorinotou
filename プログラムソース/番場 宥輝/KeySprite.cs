using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 表示するボタンを制御するクラス
/// 制作者:番場 宥輝
/// </summary>
public class KeySprite : MonoBehaviour {

    private SpriteRenderer keyRnderer;  //スプライトのレンダラー
    [SerializeField]
    private Sprite[] KeySpriteList;     //ボタンのスプライト
    void Start () {
        keyRnderer = GetComponent<SpriteRenderer>();
	}
    /// <summary>
    /// 表示するボタンを設定する関数
    /// </summary>
    /// <param name="key">描画するボタン</param>
    public void Set(int key)
    {
        //keyの値が範囲外参照されたら"0"番目のスプライトを表示する
        if (0 <= key && key < KeySpriteList.Length) 
        {
            keyRnderer.sprite = KeySpriteList[key];
        }
        else
        {
            keyRnderer.sprite = KeySpriteList[0];
        }
    }
}
