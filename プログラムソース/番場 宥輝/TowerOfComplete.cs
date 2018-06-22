using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 塔のコンプリートマークの制御クラス
/// 制作者:番場宥輝
/// </summary>
public class TowerOfComplete : MonoBehaviour {

    [SerializeField]
    private Vector3[] CompPos;          //コンプリートマークの表示位置
    private SpriteRenderer CompMark;    //レンダラーの宣言
    [SerializeField]
    private int updateFrame = 30;       //スプライトの切り替えフレーム
    [SerializeField]
    private Sprite[] changeSprite;      //切り替えるスプライトのリスト

    void Start () {
        CompMark = GetComponent<SpriteRenderer>();
    }
	
    /// <summary>
    /// コンプリートマークのセットを行う関数
    /// </summary>
	public void SetMarker()
    {
        int comp = SaveData.GetInt("Stage" + (StageSelect.stage + 1) + "ClearFlag");
        this.transform.localPosition = CompPos[StageSelect.stage];
        if (comp == 63) 
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// コンプリートマークのスプライトの更新を行うコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator UpdateSprite()
    {
        for (int i = 0; i < changeSprite.Length;)
        {
            for (int j = 0; j < updateFrame; j++)
            {
                yield return new WaitForEndOfFrame();
            }
            i++;
            if (i == changeSprite.Length)
            {
                i = 0;
            }
            if (this.gameObject.activeSelf)
            {
                CompMark.sprite = changeSprite[i];
            }
        }
    }
}
