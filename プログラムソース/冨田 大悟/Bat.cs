using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 蝙蝠の敵
/// 製作者：冨田 大悟
/// </summary>
public class Bat : Enemy {

    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField,Header("Class Bat")]
    float moveSpeed = 0.01f;
    /// <summary>
    /// X座標の移動量
    /// </summary>
    [SerializeField]
    float moveXSize = 0.1f;

    /// <summary>
    /// 移動方向
    /// </summary>
    [SerializeField]
    int moveDir = 1;

    /// <summary>
    /// 開始時の座標
    /// </summary>
    Vector3 stPos;

    /// <summary>
    /// 現在のSin値
    /// </summary>
    float sinNam;
    /// <summary>
    /// Sinの上昇量
    /// </summary>
    [SerializeField]
    float sinSpeed = 0;
    /// <summary>
    /// 上下の移動量
    /// </summary>
    [SerializeField]
    float upNam = 0;

    /// <summary>
    /// 移動目標座標
    /// </summary>
    Vector3 tPos;

    /// <summary>
    /// あたり判定の大きさ
    /// </summary>
    [SerializeField]
    Vector3 boxSize;
    /// <summary>
    /// あたり判定のRayの距離
    /// </summary>
    [SerializeField]
    float boxRaySize = 0.01f;

    /// <summary>
    /// 羽ばたく音
    /// </summary>
    [SerializeField, Header("Sound")]
    AudioClip flapSound;
    /// <summary>
    /// 羽ばたく音を再生する間隔
    /// </summary>
    [SerializeField]
    int flapTime;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        stPos = transform.position;
        sinNam = 1;
        tPos = transform.position + new Vector3(moveDir, Mathf.Sin(sinNam) * upNam, 0);
	}

    /// <summary>
    /// 蝙蝠のFixedUpdate
    /// </summary>
    protected override void EnemyUpdate()
    {

        int layer = LayerName.GetXMoveHitLayer();

        if (Physics.BoxCast(enemyCenterPos, boxSize, tPos - transform.position, new Quaternion(0, 0, 0, 0), boxRaySize, layer, QueryTriggerInteraction.Ignore))
        {
            moveDir = -moveDir;
            tPos = new Vector3(transform.position.x, stPos.y, transform.position.z) + new Vector3(moveDir* moveXSize, Mathf.Sin(sinNam) * upNam, 0);
        }

        Debug.DrawRay(enemyCenterPos, (tPos - transform.position ) * boxRaySize, Color.blue, 0.2f);

        if (transform.position == tPos)
        {
            sinNam += sinSpeed;
            tPos = new Vector3(transform.position.x, stPos.y, transform.position.z) + new Vector3(moveDir * moveXSize, Mathf.Sin(sinNam) * upNam, 0);
        }
        transform.position = Vector3.MoveTowards(transform.position, tPos, moveSpeed);
    }

    /// <summary>
    /// 蝙蝠のRotateEnemy
    /// </summary>
    public override void RotateEnemy()
    {
        base.RotateEnemy();
        moveDir = -moveDir;
    }

    private void OnEnable()
    {
        StartCoroutine(FlapSoundStart(flapTime));

    }

    /// <summary>
    /// timeの間隔で羽ばたく音を再生する
    /// </summary>
    /// <param name="time">羽ばたく音を再生する間隔</param>
    /// <returns>コルーチン参照</returns>
    IEnumerator FlapSoundStart(int time)
    {
        for(int i = 0; i < time+1; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        SEMaster.Play(flapSound);
        StartCoroutine(FlapSoundStart(time));
    }
}
