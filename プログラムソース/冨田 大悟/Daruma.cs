using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 達磨の敵
/// 製作者：冨田 大悟
/// </summary>
public class Daruma : JumpEnemy
{
    /// <summary>
    /// ジャンプ時に生成されるブロック
    /// </summary>
    [SerializeField,Header("Class Daruma")]
    BlockObj jumpBlock;
    /// <summary>
    /// 生成される座標調整
    /// </summary>
    [SerializeField]
    float popPos;

    /// <summary>
    /// ジャンプしているか
    /// </summary>
    bool jump;

    /// <summary>
    /// 地面にいるかの判定用ボックスサイズ
    /// </summary>
    [SerializeField]
    Vector3 grandChackBoxSize;
    /// <summary>
    /// 地面にいるかの判定のRayの距離
    /// </summary>
    [SerializeField]
    float grandChackRaySize;

    /// <summary>
    /// 生成ブロックの最大数
    /// </summary>
    [SerializeField]
    int blockObjMaxSize;
    /// <summary>
    /// 生成したブロックのリスト
    /// </summary>
    BlockObj[] blockObjs;
    /// <summary>
    /// 生成した数
    /// </summary>
    int nnam;

    protected override void Start()
    {
        base.Start();
        blockObjs = new BlockObj[blockObjMaxSize];
    }

    /// <summary>
    /// 達磨のFixedUpdate
    /// </summary>
    protected override void EnemyUpdate()
    {
        

        base.EnemyUpdate();
        if (jump && GetComponent<Rigidbody>().velocity.y < 0)
        {
        
            if(Physics.BoxCast(enemyCenterPos,grandChackBoxSize,Vector3.down,new Quaternion(0,0,0,0), grandChackRaySize, LayerName.GetObjectHitLayer(), QueryTriggerInteraction.Ignore))
            {
                blockObjs[nnam] = Instantiate(jumpBlock, transform.position + Vector3.down * popPos, new Quaternion(0, 0, 0, 0), this.transform.parent);
             
                jump = false;
            }
        }
        for(int i = 0; i < blockObjs.Length; i++)
        {
            if(blockObjs[i] == null)
            {
                nnam = i;
                break;
            }else if(i == blockObjs.Length-1)
            {
                jumpCount = jumpTime;
                nnam = blockObjs.Length;
            }
        }

    }

    protected override void EnemyJump()
    {
        base.EnemyJump();
        if( nnam != blockObjs.Length)
        {
            jump = true;
        }
    }
}
