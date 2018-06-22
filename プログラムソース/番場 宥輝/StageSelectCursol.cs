using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージセレクトの左右に表示されている矢印カーソルの制御クラス
/// 制作者:番場宥輝
/// </summary>
public class StageSelectCursol : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer left;        //左
    [SerializeField]
    private SpriteRenderer right;       //右
    [SerializeField]
    private Sprite[] leftSpriteList;    //左矢印の変更テクスチャリスト
    [SerializeField]
    private Sprite[] rightSpriteList;   //右矢印の変更テクスチャリスト
    [SerializeField]
    private int UPDATE_SPRITE = 10;     //テクスチャの更新フレーム

     
    /// <summary>
    /// 矢印の更新処理を行う関数
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpriteUpdate()
    {
        for (int i = 0; i < leftSpriteList.Length;)
        {
            left.sprite = leftSpriteList[i];
            right.sprite = rightSpriteList[i];
            for (int j = 0; j < UPDATE_SPRITE; j++)
            {
                yield return new WaitForEndOfFrame();
            }
            i++;
            if(i==leftSpriteList.Length)
            {
                i = 0;
            }
        }
    }
    /// <summary>
    /// アクティブの切り替えを行う関数
    /// </summary>
    /// <param name="flags"></param>
    public void Set(bool flags)
    {
        left.gameObject.SetActive(flags);
        right.gameObject.SetActive(flags);
    }

}
