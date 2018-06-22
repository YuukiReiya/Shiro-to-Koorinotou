using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// イカの敵
/// 製作者：冨田 大悟
/// </summary>
public class Squid : Enemy {

    enum MoveDir
    {
        Up,Right,Down,Left
    }

    [SerializeField,Header("Class Squid")]
    MoveDir moveDir;

    [SerializeField]
    Vector3 leftRotate;
    [SerializeField]
    Vector3 rightRotate;

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    int moveTime;
    int moveCount;

    [SerializeField]
    bool turnRight;

    [SerializeField]
    Vector3 boxSize;

    [SerializeField]
    float boxRaySize;

    Vector3 aiceBlockPopAddPosW;
    Vector3 aiceBlockPopRotateW;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        moveCount = moveTime;
        aiceBlockPopAddPosW = aiceBlockPopAddPos;
        aiceBlockPopRotateW = aiceBlockObjPopRotation;
        CheckDir();
    }

    protected override void EnemyUpdate()
    {
        Vector3 moveVDir = new Vector3(0, 0, 0);
        switch (moveDir)
        {
            case MoveDir.Up:
                moveVDir.Set(0, 1, 0);
                break;
            case MoveDir.Right:
                moveVDir.Set(1, 0, 0);

                break;
            case MoveDir.Down:
                moveVDir.Set(0, -1, 0);

                break;
            case MoveDir.Left:
                moveVDir.Set(-1, 0, 0);
                break;
        }

        if (moveCount > 0)
        {
            moveCount--;
            int layer = LayerName.GetMaltMoveHitLayer();
            if (Physics.BoxCast(enemyCenterPos, boxSize, moveVDir, new Quaternion(0, 0, 0, 0), boxRaySize, layer,QueryTriggerInteraction.Ignore))
            {
                turn();
            }
        }
        else
        {
            turn();
        }

        transform.position += (moveVDir * moveSpeed);

    }

    /// <summary>
    /// 回転
    /// </summary>
    void turn()
    {
        moveCount = moveTime;
        int dirnam = (int)moveDir;
        dirnam += turnRight ? 1 : -1;
        if (dirnam < 0)
        {
            dirnam = 3;
        }
        else if (dirnam > 3)
        {
            dirnam = 0;
        }

        moveDir = (MoveDir)dirnam;

        CheckDir();
    }

    /// <summary>
    /// 進行方向による回転の設定
    /// </summary>
    void CheckDir()
    {
        switch (moveDir)
        {
            case MoveDir.Up:
            case MoveDir.Left:
                transform.eulerAngles = leftRotate;
                aiceBlockObjPopRotation = new Vector3(aiceBlockPopRotateW.x, -aiceBlockPopRotateW.y, aiceBlockPopRotateW.z) ;
                aiceBlockPopAddPos = new Vector3(-aiceBlockPopAddPosW.x, aiceBlockPopAddPosW.y, aiceBlockPopAddPosW.z);

                break;
            case MoveDir.Right:
            case MoveDir.Down:
                transform.eulerAngles = rightRotate;
                aiceBlockObjPopRotation = aiceBlockPopRotateW;
                aiceBlockPopAddPos = new Vector3(aiceBlockPopAddPosW.x, aiceBlockPopAddPosW.y, aiceBlockPopAddPosW.z);

                break;
        }
    }

    public override void RotateEnemy()
    {
        base.RotateEnemy();
        if(moveDir == MoveDir.Right)
        {
            moveDir = MoveDir.Left;
        }else if(moveDir == MoveDir.Left) {
            moveDir = MoveDir.Right;

        }
        turnRight = !turnRight;
        CheckDir();
    }
}
