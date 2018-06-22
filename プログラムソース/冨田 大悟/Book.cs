using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本の敵
/// 製作者：冨田 大悟
/// </summary>
public class Book : Enemy {

    /// <summary>
    /// 本の撃つ玉
    /// </summary>
    [SerializeField]
    BookOffShot shot;
    /// <summary>
    /// 球の生成間隔
    /// </summary>
    [SerializeField]
    int createTime;
    /// <summary>
    /// 初回生成間隔
    /// </summary>
    [SerializeField]
    int stShotCreateTime;

    /// <summary>
    /// 球を打つ前のエフェクトの実行する時間
    /// </summary>
    [SerializeField]
    int effectPlayTime = 10;

    /// <summary>
    /// 生成用のカウント
    /// </summary>
    int createCount;

    /// <summary>
    /// 球を生成する座標の位置（半径）
    /// </summary>
    [SerializeField]
    float popPos;
    
    /// <summary>
    /// エフェクトの停止用のカウント
    /// </summary>
    [SerializeField]
    int stopTime;

    /// <summary>
    /// 球を打つ前に現れるエフェクト
    /// </summary>
    [SerializeField]
    OldEffect moveEfect;
    /// <summary>
    /// 球を打つ前に現れるエフェクトの制御
    /// </summary>
    [SerializeField]
    AutoEfectStartStop autoss;

    /// <summary>
    /// 球を撃った時の音
    /// </summary>
    [SerializeField]
    AudioClip shotSound;

    protected override void Start()
    {
        base.Start();

        moveEfect.Stop();
        createCount = stShotCreateTime;
    }
    private void OnEnable()
    {
        autoss.StertAutoEfectStert(System.Math.Max(stShotCreateTime - effectPlayTime, 0));

    }


    /// <summary>
    /// 本のFixedUpdate
    /// </summary>
    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();
        if(Player.player != null && createCount-- == 0)
        {
            createCount = createTime;

            moveEfect.Stop();
            autoss.StertAutoEfectStert(System.Math.Max( createTime - effectPlayTime,0));
            CreateGameobj();
        }
    }

    /// <summary>
    /// 球を生成する
    /// </summary>
    private void CreateGameobj()
    {
        Vector3 ppos = Player.player.playerCenter;
        Vector3 tdir = (ppos - (enemyCenterPos)).normalized;

        SEMaster.Play(shotSound);
        BookOffShot shotOb =  Instantiate(shot, transform.position +  (tdir* popPos) , new Quaternion(0, 0, 0, 0),transform.parent);
        shotOb.moves = new Vector2(tdir.x,tdir.y);
    }
}
