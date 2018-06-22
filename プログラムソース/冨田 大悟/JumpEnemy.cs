using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジャンプする敵
/// 製作者：冨田 大悟
/// </summary>
public class JumpEnemy : Enemy {

    [SerializeField,Header("Class JumpEnemy")]
    protected float jumpPow;

    [SerializeField]
    protected int jumpTime;
    protected int jumpCount;
    [SerializeField]
    protected int stJumpCount;

    [SerializeField]
    AudioClip jumpSound;

    protected override void Start()
    {
        base.Start();
        jumpCount = stJumpCount;
    }

    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();
        if (jumpCount > 0)
        {
            jumpCount--;
        }
        else
        {
            EnemyJump();
        }
    }

    protected virtual void EnemyJump()
    {
        jumpCount = jumpTime;
        GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPow, 0);
        SEMaster.Play(jumpSound);
    }
}
