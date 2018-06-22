using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 骸骨の敵
/// 製作者：冨田 大悟
/// </summary>
public class Bone : Enemy {

    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField,Header("Class Bone")]
    private float moveSpeed;
    /// <summary>
    /// 回転の半径
    /// </summary>
    [SerializeField]
    private float radius;
    /// <summary>
    /// 回転の加算角度
    /// </summary>
    [SerializeField]
    private float addAngle;

    /// <summary>
    /// 回転の中心座標
    /// </summary>
    [SerializeField]
    private Vector3 center;
    
    /// <summary>
    /// 開始時点での目標位置の種類
    /// </summary>
    enum StartPos
    {
        Right,Left,UP,Down
    }

    /// <summary>
    /// 開始時点での目標位置
    /// </summary>
    [SerializeField]
    StartPos stpos;

    /// <summary>
    /// 回転用sin値
    /// </summary>
    private float snam;
    /// <summary>
    /// 回転用cin値
    /// </summary>
    private float cnam;

    /// <summary>
    /// 目標座標
    /// </summary>
    private Vector3 targetCoordinate;

    /// <summary>
    /// 反転サイズ
    /// </summary>
    [SerializeField]
    Vector3 reverseBoxSize;
    /// <summary>
    /// 反転のRayの距離
    /// </summary>
    [SerializeField]
    float reverseRaySize;

    [SerializeField]
    GameObject rotateObj;

    /// <summary>
    /// 左回転のモデル角度
    /// </summary>
    [SerializeField]
    Vector3 leftRotate;
    /// <summary>
    /// 右回転のモデル角度
    /// </summary>
    [SerializeField]
    Vector3 rightRotate;

    /// <summary>
    /// 連続反転の制限回数　超えると動かなくなる
    /// </summary>
    [SerializeField]
    int notMoveCount = 5;
    /// <summary>
    /// 連続反転の制限回数のカウンタ
    /// </summary>
    int notMoveCounter;

    // Use this for initialization
   protected override void Start () {
        base.Start();
        switch (stpos)
        {
            case StartPos.Down:
                snam = 3.2f;
                cnam = 0;
                break;
            case StartPos.UP:
                snam = 0;
                cnam = 0;
                break;
            case StartPos.Right:
                snam = 1.5f;
                cnam = 0;
                break;
            case StartPos.Left:
                snam = 4.7f;
                cnam = 0;
                break;
        }
        targetCoordinate = center + (new Vector3(Mathf.Sin(snam), Mathf.Cos(snam), 0))*radius;
        notMoveCounter = 0;
    }

    /// <summary>
    /// 骸骨のFixedUpdate
    /// </summary>
    protected override void EnemyUpdate()
    {
        if (notMoveCounter <= notMoveCount)
        {
            GetComponent<Rigidbody>().position = Vector3.MoveTowards(transform.position, targetCoordinate, moveSpeed);
            if (GetComponent<Rigidbody>().position == targetCoordinate)
            {
                snam += addAngle;
                cnam += addAngle;
                targetCoordinate = center + (new Vector3(Mathf.Sin(snam), Mathf.Cos(snam), 0)) * radius;
            }
            Debug.DrawRay(transform.position, (targetCoordinate - transform.position).normalized, Color.red, 0.1f);

            int layer = LayerName.GetMaltMoveHitLayer();
            if (Physics.BoxCast(enemyCenterPos, reverseBoxSize, targetCoordinate - transform.position, new Quaternion(0, 0, 0, 0), reverseRaySize, layer, QueryTriggerInteraction.Ignore))
            {
                addAngle = -addAngle;
                snam += addAngle;
                cnam += addAngle;
                targetCoordinate = center + (new Vector3(Mathf.Sin(snam), Mathf.Cos(snam), 0)) * radius;
                if (notMoveCounter++ >= notMoveCount)
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            else
            {
                notMoveCounter = 0;
            }
            SetRotate();
        }
    }

    /// <summary>
    /// 目標座標によるモデルの回転
    /// </summary>
    private void SetRotate()
    {

        if( transform.position.y > center.y)
        {
            if (addAngle > 0)
            {
                rotateObj. transform.rotation = Quaternion.Euler(rightRotate);
            }
            else
            {
                rotateObj.transform.rotation = Quaternion.Euler(leftRotate);
            }
        }
        else
        {
            if (addAngle > 0)
            {
                rotateObj.transform.rotation = Quaternion.Euler(leftRotate);
            }
            else
            {
                rotateObj.transform.rotation = Quaternion.Euler(rightRotate);

            }
        }
    }

    /// <summary>
    /// 骸骨のRotateEnemy
    /// </summary>
    public override void RotateEnemy()
    {
        base.RotateEnemy();
        center = new Vector3(-center.x, center.y, center.z);
        rotateObj.transform.Rotate(0, 180, 0);
    
        if(stpos == StartPos.Left)
        {
            stpos = StartPos.Right;
        }else if (stpos == StartPos.Right)
        {
            stpos = StartPos.Left;
        }
        addAngle = -addAngle;
        SetRotate();
    }
}
