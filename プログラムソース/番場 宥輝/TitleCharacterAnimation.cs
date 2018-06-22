using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// タイトルのキャラクターアニメーション制御クラス
/// 製作者：番場宥輝
/// </summary>
public class TitleCharacterAnimation : MonoBehaviour {

    private PlayerAnimation playAnim;       //プレイヤーアニメーション宣言

	void Start () {
        playAnim = GetComponent<PlayerAnimation>();
        //アイドルモーションを行う
        playAnim.IdleAnimation(true);
	}
}
