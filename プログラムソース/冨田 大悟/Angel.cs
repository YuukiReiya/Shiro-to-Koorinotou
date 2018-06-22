using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 天使の敵　
/// 製作者：冨田 大悟
/// </summary>
public class Angel : Enemy {

    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField,Header("Angel")]
    float moveSpeed;
    /// <summary>
    /// 移動方向
    /// </summary>
    [SerializeField]
    float moveDir;
    /// <summary>
    /// 下に移動する距離
    /// </summary>
    [SerializeField]
    float downSize;
    /// <summary>
    /// 下移動用のカウント
    /// </summary>
    float downSizeCount;

    /// <summary>
    /// あたり判定の大きさ　壁・床判定用
    /// </summary>
    [SerializeField]
    Vector3 rayBoxSize;
    /// <summary>
    /// あたり判定のRayの距離
    /// </summary>
    [SerializeField]
    float raySize;
    /// <summary>
    /// 下方向のあたり判定のRayの距離
    /// </summary>
    [SerializeField]
    float downRaySize;

    /// <summary>
    /// 左右移動か下に移動しているか　trueなら下移動
    /// </summary>
    bool down;
    

    /// <summary>
    /// 天使のFixedUpdate
    /// </summary>
    protected override void EnemyUpdate()
    {
        if (down)//降下
        {

            if ( Physics.BoxCast(enemyCenterPos, rayBoxSize, Vector3.down, new Quaternion(0, 0, 0, 0), downRaySize, LayerName.GetObjectHitLayer(), QueryTriggerInteraction.Ignore))
            {

                down = false;
                downSizeCount = 0;
            }
            else
            {
                transform.position += Vector3.down * moveSpeed;
                downSizeCount += moveSpeed;
                if (downSizeCount >= downSize)
                {
                    down = false;
                    downSizeCount = 0;
                }
            }
        }
        else//左右移動
        {
            RaycastHit hit;
            if (Physics.BoxCast(enemyCenterPos, rayBoxSize, new Vector3(moveDir, 0, 0), out hit, new Quaternion(0, 0, 0, 0), raySize, LayerName.GetXMoveHitLayer(), QueryTriggerInteraction.Ignore))
            {

                if (hit.transform.CompareTag("enemy"))
                {
                    //反転
                    moveDir = -moveDir;
                }
                else if (hit.transform.CompareTag("wall") || hit.transform.CompareTag("blockObj"))
                {
                    //降下
                    down = true;
                    moveDir = -moveDir;

                }
            }
            else
            {
                transform.position += new Vector3(moveSpeed * moveDir, 0, 0);
            }

        }
    }

    /// <summary>
    /// 天使のRotateEnemy
    /// </summary>
    public override void RotateEnemy()
    {
        base.RotateEnemy();
        moveDir = -moveDir;
    }
}
