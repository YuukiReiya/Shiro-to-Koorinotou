using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ドラゴンの敵
/// 製作者：冨田 大悟
/// </summary>
public class Doragon : TargetMove
{
    /// <summary>
    /// 目標座標に到達時のウェイト
    /// </summary>
    [SerializeField,Header("Class Doragon")]
    int[] posWait;
    /// <summary>
    /// ウェイトカウント
    /// </summary>
    int waitCount;

    /// <summary>
    /// 回転するオブジェクト
    /// </summary>
    [SerializeField]
    GameObject rotateObj;

    /// <summary>
    /// 左移動しているときの回転
    /// </summary>
    [SerializeField]
    Vector3 leftMove;
    /// <summary>
    /// 右移動しているときの回転
    /// </summary>
    [SerializeField]
    Vector3 rightMove;

    /// <summary>
    /// 左移動しているときの凍った時の回転
    /// </summary>
    [SerializeField]
    Vector3 aiceBlockRotateLeft;
    /// <summary>
    /// 右移動しているときの凍った時の回転
    /// </summary>
    [SerializeField]
    Vector3 aiceBlockRotateRight;

    protected override void Start()
    {

        int[] w = posWait;
        base.Start();
        posWait = w;

        waitCount = posWait[moveNam];
    }

    /// <summary>
    /// ドラゴンのFixedUpdate
    /// </summary>
    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();
        if(transform.position.x > targetPos[moveNam].x)
        {
            rotateObj.transform.eulerAngles = leftMove;
        }else if (transform.position.x < targetPos[moveNam].x)
        {
            rotateObj.transform.eulerAngles = rightMove;
        }
    }

    /// <summary>
    /// ドラゴンのBlockObjPop
    /// </summary>
    /// <returns></returns>
    protected override BlockObj BlockObjPop()
    {
        BlockObj bo = base.BlockObjPop();
        if(transform.position.x > targetPos[moveNam].x)
        {
            bo.transform.eulerAngles =  aiceBlockRotateLeft;
        }else if (transform.position.x < targetPos[moveNam].x)
        {
            bo.transform.eulerAngles = aiceBlockRotateRight;

        }
        return bo;
    }

    /// <summary>
    /// ドラゴンの目標到達時の処理
    /// </summary>
    protected override void reachingPos()
    {
        if(waitCount-- ==0 )
        {
            addMoveNam(moveDir);
            waitCount = posWait[moveNam];
        }
    }

    /// <summary>
    /// ドラゴンの目標座標の設定
    /// </summary>
    [ContextMenu("SetTargetPos")]
    protected override void SetTargetPos()
    {
        base.SetTargetPos();
        posWait = new int[ targetPos.Count];
    }

    /// <summary>
    /// ドラゴンの目標座標の更新
    /// </summary>
    /// <param name="adds">進行方向</param>
    protected override void addMoveNam(int adds)
    {

        moveNam += adds;

        if (moveNam == -1)
        {
            moveNam = 1;
            moveDir = -moveDir;

        }
        else if (moveNam == targetPos.Count)
        {
            moveNam = targetPos.Count - 2;
            moveDir = -moveDir;
        }

        waitCount = posWait[moveNam];
    }

    public override void FloorRotate()
    {
        base.FloorRotate();
        rotateObj.transform.Rotate(0, 180, 0);
        if (transform.position.x > targetPos[moveNam].x)
        {
            rotateObj.transform.eulerAngles = leftMove;
        }
        else if (transform.position.x < targetPos[moveNam].x)
        {
            rotateObj.transform.eulerAngles = rightMove;
        }
    }
}
