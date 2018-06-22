using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本の打つ玉
/// 製作者：冨田 大悟
/// </summary>
public class BookOffShot : EnemyShot
{
    /// <summary>
    /// 生成される氷
    /// </summary>
    [SerializeField]
    BlockObj bo;

    /// <summary>
    /// 複数回当たるのを防止用
    /// </summary>
    bool hitOne = false;

    /// <summary>
    /// エフェクト制御用
    /// </summary>
    [SerializeField]
    OldEffect moveEfect;

    private void OnCollisionEnter(Collision collision)
    {
        if (hitOne) return;
        if (!collision.gameObject.CompareTag("Player"))
        {
            ToHit();
            Destroy(this);
            gameObject.AddComponent<AutoDestroy>().StertAutoDetroy(30);
            moveEfect.Stop();
            Destroy(GetComponent<SphereCollider>());
            Destroy(GetComponent<Rigidbody>());
        }
        else
        {
            creatHitF();
            collision.gameObject.GetComponent<PlayerDamage>().Damage();
            //Destroy(this.gameObject);
            Destroy(this);
            gameObject.AddComponent<AutoDestroy>().StertAutoDetroy(30);
            moveEfect.Stop();
            Destroy(GetComponent<SphereCollider>());
            Destroy(GetComponent<Rigidbody>());
        }
        hitOne = true;
    }

    /// <summary>
    /// ブロックとエフェクトの生成
    /// </summary>
    void ToHit()
    {
        Instantiate(bo, transform.position, new Quaternion(0, 0, 0, 0),transform.parent);
        creatHitF();
    }
    
    /// <summary>
    /// エフェクトの生成
    /// </summary>
    void creatHitF()
    {
       GameObject go = Instantiate(hitef, transform.position, new Quaternion(0, 0, 0, 0), transform.parent);
        go.AddComponent<AutoDestroy>().StertAutoDetroy(efDeleteTime);
    }
}
