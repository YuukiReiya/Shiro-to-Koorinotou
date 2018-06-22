using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 止まって首を振る敵
/// 製作者：冨田 大悟
/// </summary>
[SelectionBase]
public class Stagnation : Enemy {

    [SerializeField,Header("Class Stagnation")]
    GameObject rotateObj;
    [SerializeField]
    Vector3 enemyRotate;

    [SerializeField]
    float rotateSize;
    [SerializeField]
    float rotateSpeed =0.1f;
    float sinCount;
	
    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();
        rotateObj.transform.rotation = Quaternion.Euler(enemyRotate + new Vector3(0, Mathf.Sin(sinCount) * rotateSize, 0));
        sinCount += rotateSpeed;
    }
}
