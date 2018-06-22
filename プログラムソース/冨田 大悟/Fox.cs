using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 狐の敵
/// 製作者：冨田 大悟
/// </summary>
public class Fox : Enemy {

    [SerializeField,Header("Class Fox")]
    float moveDir=1;

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    Vector3 boxSize;
    [SerializeField]
    float boxRaySize;
    [SerializeField]
    Vector3 popPos;

    [SerializeField]
    GameObject go;
    [SerializeField]
    int createTime;
    int count;

    [SerializeField]
    GameObject rotateObj;
    [SerializeField]
    Vector3 leftRotate, rightRotate;

    [SerializeField]
    AudioClip shotSound;

    protected override void Start()
    {
        base.Start();

        ChackMoveDir();
    }

    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();
        if (Physics.BoxCast(enemyCenterPos, boxSize, Vector3.right * moveDir, new Quaternion(0, 0, 0, 0), boxRaySize, LayerName.GetXMoveHitLayer(), QueryTriggerInteraction.Ignore))
        {
            moveDir = -moveDir;
            ChackMoveDir();
        }
        else
        {
            transform.position += new Vector3(moveSpeed * moveDir, 0, 0);

        }
        if (count == createTime)
        {
            count = 0;
            CreateGameobj();
        }
        count++;
    }
    /// <summary>
    /// 球の生成
    /// </summary>
    private void CreateGameobj()
    {
        SEMaster.Play(shotSound);
        Instantiate(go, transform.position + new Vector3(popPos.x*moveDir,popPos.y,popPos.z), new Quaternion(0, 0, 0, 0));
    }
    /// <summary>
    /// 進行方向による向きの回転
    /// </summary>
    void ChackMoveDir()
    {
        if (moveDir == 1)
        {
            rotateObj.transform.eulerAngles = rightRotate;
        }
        else
        {
            rotateObj.transform.eulerAngles = leftRotate;

        }
    }
}
