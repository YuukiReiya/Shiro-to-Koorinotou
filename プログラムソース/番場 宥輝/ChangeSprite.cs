using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スプライトの更新を行うクラス
/// 制作者:番場 宥輝
/// </summary>
public class ChangeSprite : MonoBehaviour {

    private SpriteRenderer renderer;       //Renderer
    [SerializeField]        
    private Sprite[] spriteList;           //更新するスプライトを格納する配列

	
	void Awake () {
        renderer = GetComponent<SpriteRenderer>();
	}

    /// <summary>
    /// 別のスプライトに更新する関数
    /// </summary>
    /// <param name="index">スプライトが格納されてる配列の添え字</param>
    public void UpdateSprite(int index)
    {
        //範囲外参照されたら"0"番目のスプライトにする
        if (index >= spriteList.Length || index < 0) 
        {
            renderer.sprite = spriteList[0];
            Debug.Log(this.gameObject.name + "のChangeSpriteクラスにてインデックスの上限参照されました\n" + "値は" +index);
        }
        else
        {
            renderer.sprite = spriteList[index];
        }
    }
}
