using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージセレクトのポーズ画面の後ろのシャドウ制御クラス
/// 制作者:番場宥輝
/// </summary>
public class StageSelectBackShadows : MonoBehaviour {

    private SpriteRenderer backShadow;  //スプライトレンダラーの宣言
    [SerializeField]
    private float maxAlpha;             //α値の最大値
    [SerializeField]
    private float minAlpha;             //α値の最小値
    [SerializeField]
    private float value;                //変化量
    private float alpha;                //α値


    void Start () {
        backShadow = GetComponent<SpriteRenderer>();
        maxAlpha = backShadow.color.a;
        backShadow.color = new Color(backShadow.color.r, backShadow.color.g, backShadow.color.b, minAlpha);
    }
    /// <summary>
    /// α値を上げていくコルーチン
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
    /// α値を下げていくコルーチン
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
