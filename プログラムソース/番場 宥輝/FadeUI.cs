using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIをフェードさせるクラス
/// 制作者:番場宥輝
/// </summary>
public class FadeUI : MonoBehaviour {

    private Image UI;               //UI
    [SerializeField]
    private float alphaVal = -0.02f;//1フレーム辺りの変化量
    private float alpha;            //α値


	void Start () {
        UI = GetComponent<Image>();
	}
	/// <summary>
    /// 描画関数
    /// </summary>
    public void Draw()
    {
        alpha += alphaVal;
        UI.color = new Color(1, 1, 1, alpha);
        if (UI.color.a < 0)
        {
            alphaVal *= -1;
        }
        else if (UI.color.a > 1)
        {
            alphaVal *= -1;
        }
    }
	/// <summary>
    /// 初期化関数
    /// </summary>
    public void Reset()
    {
        alpha = 1;
        UI.color = new Color(1, 1, 1, alpha);
    }

}
