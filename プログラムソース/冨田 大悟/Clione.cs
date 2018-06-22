using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クリオネの敵
/// 製作者：冨田 大悟
/// </summary>
public class Clione : Enemy {

    private float sy;
    [SerializeField,Header("Class Clione")]
    private float power;

    protected override void Start()
    {
        base.Start();

        sy = transform.position.y;
    }

    protected override void EnemyUpdate()
    {
        if (sy >= transform.position.y)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-3, 3), power, 0);
        }
    }

}
