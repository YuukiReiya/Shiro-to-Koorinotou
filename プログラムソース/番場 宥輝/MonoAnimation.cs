using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 単一アニメーションを行うクラス
/// 制作者:番場 宥輝
/// </summary>
public class MonoAnimation : MonoBehaviour {

    //  variable
    [SerializeField]
    private AnimationClip animclip;                     //行うアニメーションクリップ
    private Animation anim;                             //アニメーションコンポーネント
    [SerializeField]
    private float ClipMag = 1.0f;                       //再生速度

    private void Awake()
    {
        anim = GetComponent<Animation>();               //アタッチされたAnimationを取得
        AnimationState animState = anim[animclip.name]; //アニメーションクリップのステートを取得
        animState.speed = ClipMag;                      //アニメーションクリップの再生速度を任意の倍率に変化させる
        //開始時に合わせてアニメーションする
        PlayAnim();
    }


    //  Function
    /// <summary>
    /// アニメーションの再生
    /// </summary>
    public void PlayAnim()
    {
        anim.Play();
    }
    /// <summary>
    /// アニメーションの停止
    /// </summary>
    public void StopAnim()
    {
        anim.Stop();
    }

}
