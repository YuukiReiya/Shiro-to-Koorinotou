using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "しろちゃん"のアニメーション制御クラス
/// 制作者：番場宥輝
/// </summary>
public class PlayerAnimation : MonoBehaviour {

    private Animator animator;      //アニメーター

    void Awake () {
        animator = GetComponent<Animator>();
	}
    /// <summary>
    ///  走るアニメーション
    /// </summary>
    public void RunAnimation(bool result)
    {
        animator.SetBool("isMove", result);
    }
/// <summary>
/// ジャンプアニメーション
/// </summary>
/// <param name="animflags"></param>
    public void JumpAnimation(bool animflags)
    {
        animator.SetBool("isJump", animflags);

    }

    /// <summary>
    /// クリアアニメーション
    /// </summary>
    /// <param name="animflags"></param>
    public void ClearAnimation(bool animflags)
    {
        animator.SetBool("isClear", animflags);
    }


    /// <summary>
    /// 横向き魔法アニメーション
    /// </summary>
    public void CrossMagicAnimation()
    {
        animator.SetTrigger("useCrossMagic");
    }

    /// <summary>
    /// 上向き魔法アニメーション
    /// </summary>
    public void UpMagicAnimation()
    {
        animator.SetTrigger("useUpMagic");
    }
    /// <summary>
    /// 下向き魔法アニメーション
    /// </summary>
    public void UnderMagicAnimation()
    {
        animator.SetTrigger("useUnderMagic");
    }
    /// <summary>
    /// ダメージアニメーション
    /// </summary>
    public void DamageAnimation()
    {
        animator.SetTrigger("isStanded");
    }
    /// <summary>
    /// 待機（アイドル）アニメーション
    /// </summary>
    /// <param name="animflags"></param>
    public void IdleAnimation(bool animflags)
    {
        animator.SetBool("isIdleMotion", animflags);
    }
}
