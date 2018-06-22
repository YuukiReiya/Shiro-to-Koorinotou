using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 目的地を指定して移動する敵
/// 製作者：冨田 大悟
/// </summary>
public class TargetMove : Enemy {

    [SerializeField,Header("Class TargetMove")]
    protected int moveDir;

    [SerializeField]
    float movePower;

    [SerializeField]
    GameObject tergetListObj;
    [SerializeField]
    protected List< Vector3> targetPos;

    [SerializeField]
    protected int moveNam;

    [SerializeField]
    bool hitReturn;
    [SerializeField]
    float hitReturnRaySize = 0.1f;

    [SerializeField]
    Vector3 hitBoxSize;

    protected override void Start()
    {
        base.Start();

        SetTargetPos();
    }

    /// <summary>
    /// 座標の設定
    /// </summary>
    [ContextMenu("SetTargetPos")]
    virtual protected void SetTargetPos()
    {
        targetPos.Clear();
        targetPos.Add(this.transform.position);


        foreach (Transform tr in tergetListObj.GetComponentsInChildren<Transform>())
        {
            if(tr.gameObject != this.tergetListObj)
            {
                targetPos.Add(tr.position);
            }
        }
    }

    protected override void EnemyUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos[moveNam], movePower);
        if (transform.position == targetPos[moveNam])
        {
            reachingPos();
        }
        if (hitReturn)
        {
            int layer = LayerName.GetMaltMoveHitLayer();
            if (Physics.BoxCast(transform.position, hitBoxSize, targetPos[moveNam] - transform.position, new Quaternion(0, 0, 0, 0), hitReturnRaySize, layer, QueryTriggerInteraction.Ignore))
            {

                moveDir *= -1;
                addMoveNam(moveDir);
            }
        }
    }

    /// <summary>
    /// 目標到着時の処理
    /// </summary>
    virtual protected void reachingPos()
    {
        addMoveNam(moveDir);
    }

    /// <summary>
    /// 次の目標を設定
    /// </summary>
    /// <param name="adds"></param>
    virtual protected void addMoveNam(int adds)
    {
        moveNam += adds;

        if(moveNam == -1)
        {
            moveNam = targetPos.Count - 1;
        }else if(moveNam == targetPos.Count)
        {
            moveNam = 0;
        }
    }

    public override void RotateEnemy()
    {
        base.RotateEnemy();
        tergetListObj.transform.eulerAngles += new Vector3(0, 180, 0);
    }
}
