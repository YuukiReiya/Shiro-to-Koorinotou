using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ハリセンボンの敵
/// 製作者：冨田 大悟
/// </summary>
public class Globefish : Enemy {

    [SerializeField,Header("Class Globefish")]
    Vector3 moveDir;
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float raySize = 1;
    [SerializeField]
    Vector3 boxRaySize;

    [SerializeField]
    GameObject cangeScaleObj;

    float sinCount;
    [SerializeField]
    float sinSpeed = 0.1f;
    [SerializeField]
    float sinSize;
    Vector3 baseSize;

    // Use this for initialization
   protected override void Start () {
        base.Start();

        baseSize = cangeScaleObj.transform.localScale;
        sinCount = 0;
    }

    private void FixedUpdate()
    {
        if (!IsWallHit())
        {
            SetColliderCenter();


            RaycastHit hit;
            if (Physics.BoxCast(enemyCenterPos, boxRaySize, moveDir, out hit, new Quaternion(0, 0, 0, 0), raySize,LayerName.GetMaltMoveHitLayer(), QueryTriggerInteraction.Ignore))
            {
                moveDir = Vector3.Reflect(this.moveDir, hit.normal);
                Debug.DrawRay(hit.point, moveDir, Color.green,2);

            }
            transform.position += moveDir.normalized * moveSpeed;
            cangeScaleObj.transform.localScale = baseSize +  Vector3.one *( (1+Mathf.Sin(sinCount)) * sinSize);
            sinCount += sinSpeed;
        }
    }

    public override void RotateEnemy()
    {
        base.RotateEnemy();
        moveDir = new Vector3(-moveDir.x, moveDir.y, moveDir.z);
    }
}
