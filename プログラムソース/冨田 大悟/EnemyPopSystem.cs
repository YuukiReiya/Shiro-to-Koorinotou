using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの敵の現れ方
/// 製作者：冨田 大悟
/// </summary>
public class EnemyPopSystem : MonoBehaviour {

    protected Tower.HitBlock hitBlock;

    /// <summary>
    /// 開始時の一階層目の処理
    /// </summary>
    virtual public void EPSFOne()
    {

    }
    /// <summary>
    /// 開始時の二階層目以降の処理
    /// </summary>
    virtual public void EPSFAll()
    {

    }


    /// <summary>
    /// 階層の開始
    /// </summary>
    virtual public void EPSStart()
    {
        
    }

    /// <summary>
    /// 階層の更新
    /// </summary>
    virtual public void EPSUpdate()
    {


    }

    /// <summary>
    /// 開始前のカメラ上昇の開始
    /// </summary>
    virtual public void CamaeraUpStert()
    {

    }
    /// <summary>
    /// 開始前のカメラ上昇の開始
    /// </summary>
    virtual public void CamaeraUpUpdate()
    {

    }
    /// <summary>
    /// 開始前のカメラ上昇の終了
    /// </summary>
    virtual public void CamaeraUpEnd()
    {

    }

    /// <summary>
    /// タワーが閉じた瞬間
    /// </summary>
    virtual public void TowerEnd() { }

    /// <summary>
    /// 終了したブロックの方向
    /// </summary>
    /// <param name="hb"></param>
    public void SetEndBlock(Tower.HitBlock hb)
    {
        hitBlock = hb;
    }
}
