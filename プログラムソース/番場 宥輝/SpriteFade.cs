using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スプライトをフェードするクラス
/// 制作者:番場 宥輝
/// </summary>
public class SpriteFade : MonoBehaviour
{
    private SpriteRenderer renderer;        //スプライトのレンダラー
    private float initial;                  //α値の初期値
    private float alpha;                    //レンダラーのα値
    private float value = 0.07f;            //α値の変化量
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        initial = renderer.color.a;
        alpha = renderer.color.a;
    }
    /// <summary>
    /// α値を落とす関数
    /// </summary>
    public void Fade()
    {
        if (this.gameObject.activeSelf)
        {
            alpha -= value;
            renderer.color = new Color(
                renderer.color.r,
                renderer.color.g,
                renderer.color.b,
                alpha);
        }
    }
    /// <summary>
    /// 色の初期化を行う関数
    /// </summary>
    public void Reset()
    {
        if (this.gameObject.activeSelf)
        {
            alpha = initial;
            renderer.color = new Color(
                renderer.color.r,
                renderer.color.g,
                renderer.color.b,
                alpha);
        }
    }
}
