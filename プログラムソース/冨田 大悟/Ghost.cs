using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オバケの敵
/// 製作者：冨田 大悟
/// </summary>
public class Ghost : Enemy {
    Player target;

    [SerializeField,Header("Class Ghost")]
    float moveSpeed;

    [SerializeField]
    GameObject rotateObj;

    [SerializeField]
    Vector3 rightRotate;
    [SerializeField]
    Vector3 leftRotate;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        model.GetComponent<SkinnedMeshRenderer>().sharedMaterial.renderQueue = 2999;

        target = Player.player;
        if (target != null)
        {
            if (target.transform.position.x > transform.position.x)//目標の左側
            {
                rotateObj.transform.rotation = Quaternion.Euler(leftRotate);
            }
            else if (target.transform.position.x < transform.position.x)//目標の右側
            {
                rotateObj.transform.rotation = Quaternion.Euler(rightRotate);
            }
        }
    }

    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();

        if (target.transform.position.x > transform.position.x && target.GetComponent<Player>().dir == 1)//目標の左側
        {
            transform.position = Vector3.MoveTowards(transform.position, target.playerCenter, moveSpeed);
            rotateObj.transform.rotation = Quaternion.Euler(leftRotate);
        }
        else if (target.transform.position.x < transform.position.x && target.GetComponent<Player>().dir == -1)//目標の右側
        {
            transform.position = Vector3.MoveTowards(transform.position, target.playerCenter, moveSpeed);
            rotateObj.transform.rotation = Quaternion.Euler(rightRotate);
        }
    }

    public override void OnValidate()
    {
        base.OnValidate();
        model.GetComponent<SkinnedMeshRenderer>().sharedMaterial.renderQueue = 2999;

    }
}
