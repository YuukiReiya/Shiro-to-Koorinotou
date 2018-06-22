using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 画面にかけるシャドウの制御クラス
/// 制作者:番場宥輝
/// </summary>
public class TitleBackShadows : MonoBehaviour {

    private Image backShadow;       //シャドウ
    [SerializeField]
    private float maxAlpha;         //表示するα値のMAX
    [SerializeField]
    private float minAlpha;         //表示するα値のMIN
    [SerializeField]
    private float value;            //１フレームあたりに変化する変位
    private float alpha;            //α値

	void Start () {
        backShadow = GetComponent<Image>();
        maxAlpha = backShadow.color.a;
        backShadow.color = new Color(backShadow.color.r, backShadow.color.g, backShadow.color.b, minAlpha);
	}
    /// <summary>
    /// フェードさせながらα値を上げていくコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartFadeIn()
    {
        while (alpha < maxAlpha) 
        {
            alpha += value;
            backShadow.color = new Color(backShadow.color.r, backShadow.color.g, backShadow.color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
        alpha = maxAlpha;
        backShadow.color = new Color(backShadow.color.r, backShadow.color.g, backShadow.color.b, alpha);
    }
    /// <summary>
    /// フェードさせながらα値を下げていくコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartFadeOut()
    {
        while (minAlpha < alpha) 
        {
            alpha -= value;
            backShadow.color = new Color(backShadow.color.r, backShadow.color.g, backShadow.color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
        alpha = minAlpha;
        backShadow.color = new Color(backShadow.color.r, backShadow.color.g, backShadow.color.b, alpha);
    }
}
