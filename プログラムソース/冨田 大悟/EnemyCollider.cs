using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーコライダー
/// 製作者：冨田 大悟
/// </summary>
public class EnemyCollider : MonoBehaviour {

    /// <summary>
    /// 実際のコライダーの中心座標
    /// </summary>
    public Vector3 enemyCenterPos;

    /// <summary>
    /// エネミーの中心座標調整
    /// </summary>
    [SerializeField]
    Vector3 colliderCenterAddPos;


    /// <summary>
    ///  コライダー調整用
    /// </summary>
    [System.Serializable]
    public class EnemyColliderBase
    {
        /// <summary>
        /// 参照コライダー
        /// </summary>
        [SerializeField]
        BoxCollider collider;
        
        /// <summary>
        /// コライダーの中心調整
        /// </summary>
        [SerializeField]
        Vector3 colliderBaseCenterAddPos;

        public void SetColliderPos(Vector3 basePos)
        {
            collider.center = basePos + colliderBaseCenterAddPos;
        }

        public void RotateCollider()
        {
            colliderBaseCenterAddPos = new Vector3(-colliderBaseCenterAddPos.x, colliderBaseCenterAddPos.y);
        }
    }

    [SerializeField]
    List<EnemyColliderBase> colliderBases;

    /// <summary>
    /// コライダー座標の修正
    /// </summary>
    /// <param name="basePos">中心となるワールド座標</param>
    public void SetColliderPos(Vector3 basePos)
    {
        basePos -= transform.position;
        basePos = new Vector3(basePos.x, basePos.y, 0);
        enemyCenterPos = basePos + colliderCenterAddPos;

        foreach (var cbase in colliderBases) {
            cbase.SetColliderPos(enemyCenterPos);
        }
    }

    public void RotateCollider()
    {
        colliderCenterAddPos = new Vector3(-colliderCenterAddPos.x, colliderCenterAddPos.y);
        foreach (var cbase in colliderBases)
        {
            cbase.RotateCollider();
        }
    }

    public void OnValidate()
    {
        GetComponent<Enemy>().OnValidate();
    }

    public void RotateColliderCenterAddPos()
    {
        colliderCenterAddPos = new Vector3(-colliderCenterAddPos.x, colliderCenterAddPos.y);

    }
}
