using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// 敵の親クラス
/// 製作者：冨田 大悟
/// </summary>
[SelectionBase]
[ExecuteInEditMode]
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 実際のエネミーの中心座標
    /// </summary>
    protected Vector3 enemyCenterPos {
        get { return transform.position + enemyCollider.enemyCenterPos; }
    }

    /// <summary>
    /// エネミーの中心の元となるオブジェ　　基本ジョイント
    /// </summary>
    [SerializeField, Header("Class Enemy")]
    protected GameObject colliderCenter;

    EnemyCollider enemyCollider;

    /// <summary>
    /// 氷漬けにされるオブジェ
    /// </summary>
    [SerializeField]
    protected GameObject model;
    /// <summary>
    /// 凍らせた時の氷
    /// </summary>
    [SerializeField]
    BlockObj aiceBlock;

    /// <summary>
    /// 氷の出現位置調整
    /// </summary>
    [SerializeField]
    protected Vector3 aiceBlockPopAddPos;

    /// <summary>
    /// 出現する氷の回転
    /// </summary>
    [SerializeField]
    protected Vector3 aiceBlockObjPopRotation;

    /// <summary>
    /// 壁に挟まれるRayの距離
    /// </summary>
    [SerializeField, Header("WallHit")]
    float wallHitRaySize;
    /// <summary>
    /// 壁に挟まれるRayのサイズ
    /// </summary>
    [SerializeField]
    Vector3 wallHitBoxSize;
    /// <summary>
    /// 壁に挟まれたときに固定される位置、（前座標）
    /// </summary>
    Vector3 wallHitBackPos;

    /// <summary>
    /// 壁に挟まれたときのエフェクト
    /// </summary>
    [SerializeField]
    GameObject hitEffect;

    protected virtual void Start()
    {
        wallHitBackPos = transform.position;
        enemyCollider = GetComponent<EnemyCollider>();
    }

    void FixedUpdate()
    {
        SetColliderCenter();
        if (!IsWallHit() && !IsAicBlockHit())
        {
            EnemyUpdate();
        }
    }

    /// <summary>
    /// 継承先でのFixedUpdate
    /// </summary>
    protected virtual void EnemyUpdate()
    {

    }

    virtual public void OnValidate()
    {
        GetComponent<EnemyCollider>().SetColliderPos(colliderCenter.transform.position);
    }

    /// <summary>
    /// モーションでの座標更新によるコライダーの座標更新
    /// </summary>
    protected virtual void SetColliderCenter()
    {
        enemyCollider.SetColliderPos(colliderCenter.transform.position);
    }

    /// <summary>
    /// 氷漬けにされたものに変換する
    /// </summary>
    [ContextMenu("CangeObj")]
    public void CangeObj()
    {
        //ブロック生成
        BlockObj bo = BlockObjPop();

        GameObject modelCopy = Instantiate(model);
        MonoAnimation monoAnimation = modelCopy.GetComponent<MonoAnimation>();
        //モデル挿入
        bo.SetModel(modelCopy);

        //解凍参照代入
        bo.enemy = this;
        this.transform.parent = bo.transform;

        this.gameObject.SetActive(false);
    }


    /// <summary>
    /// 氷を生成する
    /// </summary>
    /// <returns>生成された氷</returns>
    protected virtual BlockObj BlockObjPop()
    {
        return Instantiate(aiceBlock, enemyCenterPos + aiceBlockPopAddPos, Quaternion.Euler(aiceBlockObjPopRotation), transform.parent);
    }

    /// <summary>
    /// 壁に挟まれているか
    /// </summary>
    /// <returns>壁に挟まれていればtrue</returns>
    protected bool IsWallHit()
    {
        bool a = false, b = false, c = false;

        RaycastHit[] hits = Physics.BoxCastAll(enemyCenterPos, wallHitBoxSize, Vector3.left,
            new Quaternion(0, 0, 0, 0), wallHitRaySize, LayerMask.GetMask(LayerName.WALL), QueryTriggerInteraction.Ignore);

        a = hits.Length > 0;

         hits = Physics.BoxCastAll(enemyCenterPos , wallHitBoxSize, Vector3.right,
            new Quaternion(0, 0, 0, 0), wallHitRaySize, LayerMask.GetMask(LayerName.WALL), QueryTriggerInteraction.Ignore);

        b = hits.Length > 0;

        if(Physics.CheckBox(enemyCenterPos, wallHitBoxSize,new Quaternion(0,0,0,0), LayerMask.GetMask(LayerName.WALL), QueryTriggerInteraction.Ignore)){
            c = true;
        }

        //あたり
        if(a&&b || c)
        {
            transform.position = wallHitBackPos;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            return true;
        }else
        {
            wallHitBackPos = transform.position;

        }
        return false;
    }

    /// <summary>
    /// 氷にめり込んでいるか
    /// </summary>
    /// <returns>めり込んでいればtrue</returns>
    protected bool IsAicBlockHit()
    {
        return Physics.CheckBox(enemyCenterPos, wallHitBoxSize, new Quaternion(0, 0, 0, 0), LayerMask.GetMask(LayerName.BLOCK), QueryTriggerInteraction.Ignore);
        
    }

    /// <summary>
    /// 壁に挟まれたときのエフェクトを生成する
    /// </summary>
    public void WallebatanEffect()
    {
        Debug.Log(gameObject.name + ":壁に挟まれたエフェクト生成");
        Instantiate(hitEffect, enemyCenterPos, new Quaternion(0,0,0,0));
    }

    /// <summary>
    /// プレイヤーに当たった時の処理
    /// </summary>
    public virtual void PlayerHit()
    {

    }

    /// <summary>
    /// 左右の反転
    /// </summary>
    public virtual void RotateEnemy()
    {

    }

    /// <summary>
    /// 階層の反転
    /// </summary>
    public virtual void FloorRotate()
    {
        Start();
        RotateEnemy();
    }
}