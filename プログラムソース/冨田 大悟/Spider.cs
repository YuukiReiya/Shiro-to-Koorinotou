using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 蜘蛛の敵
/// 製作者：冨田 大悟
/// </summary>
public class Spider : Enemy {

    [SerializeField,Header("Class Spider")]
    float moveSpeed;
    [SerializeField]
    int moveDir;
    [SerializeField]
    GameObject loopObj;

    [SerializeField]
    int moveNam;

    [SerializeField]
    GameObject rotateObj;
    [SerializeField]
    Vector3[] rotates = new Vector3[4];

    [SerializeField]
    int grMoveDir;
    
    [SerializeField]
    Vector3 rayBoxScale;
    [SerializeField]
    float rotateRaySize;
    [SerializeField]
    Vector3 blockHitBoxScale;
    [SerializeField]
    float blockHitRay;

    [SerializeField]
    float addP;

    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();
        if (loopObj != null)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            RaycastHit hit;
            if (BlockRayHit(enemyCenterPos, blockHitBoxScale, -rotateObj.transform.up, out hit))
            {
                loopObj = hit.transform.gameObject;

                transform.position += -rotateObj.transform.right * moveSpeed * moveDir;
                if (RotateRayHit(enemyCenterPos, rayBoxScale, -rotateObj.transform.right, out hit))
                {
                    loopObj = hit.transform.gameObject;
                    moveNam += 4 - moveDir;
                    moveNam %= 4;
                    rotateObj.transform.eulerAngles = rotates[moveNam];

                    transform.position = hit.point + (rotateObj.transform.up * addP);

                    Debug.Log("壁移り");
                }

            }

            else
            {
                moveNam += 4 + moveDir;
                moveNam %= 4;
                rotateObj.transform.eulerAngles = rotates[moveNam];

                transform.position += -rotateObj.transform.right * moveSpeed * 3 * moveDir;
                if (BlockRayHit(enemyCenterPos, blockHitBoxScale, -rotateObj.transform.up, out hit))
                {
                    transform.position = hit.point + (rotateObj.transform.up * addP);
                    Debug.Log("壁回転 : 座標調整");
                    loopObj = hit.transform.gameObject;
                }
                else
                {
                    loopObj = null;
                }
                Debug.Log("壁回転");
            }
        }
        else
        {
            rotateObj.transform.eulerAngles = rotates[0];
            GetComponent<Rigidbody>().useGravity = true;
            RaycastHit hit;

            if (Physics.BoxCast(enemyCenterPos + new Vector3(0, 0.1f, 0), rayBoxScale, Vector3.down, out hit, Quaternion.Euler(rotates[0]), blockHitRay, LayerName.GetObjectHitLayer(), QueryTriggerInteraction.Ignore))
            {

                if (hit.transform.CompareTag("wall") || (hit.transform.CompareTag("endWall")))
                {
                    if (Physics.BoxCast(enemyCenterPos, rayBoxScale, new Vector3(grMoveDir, 0, 0), Quaternion.Euler(rotates[0]), rotateRaySize, LayerName.GetXMoveHitLayer(), QueryTriggerInteraction.Ignore))
                    {
                        grMoveDir = -grMoveDir;
                    }
                    transform.position += new Vector3(grMoveDir * moveSpeed, 0, 0);
                }
                else
                {
                    loopObj = hit.transform.gameObject;
                    moveNam = 0;
                    GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    transform.position = hit.point + (rotateObj.transform.up * addP);

                }

            }
        }
    }

    bool BlockRayHit(Vector3 pos, Vector3 box, Vector3 dir,out RaycastHit hit)
    {
        return Physics.BoxCast(pos, box, dir,out hit, new Quaternion(0, 0, 0, 0), blockHitRay, LayerMask.GetMask(LayerName.BLOCK), QueryTriggerInteraction.Ignore);
    }


    bool RotateRayHit(Vector3 pos, Vector3 box, Vector3 dir,out RaycastHit hit)
    {
        return Physics.BoxCast(transform.position, rayBoxScale, dir,out hit, new Quaternion(0, 0, 0, 0), rotateRaySize, LayerMask.GetMask(LayerName.BLOCK), QueryTriggerInteraction.Ignore);
    }

    public override void RotateEnemy()
    {
        base.RotateEnemy();
        moveDir = -moveDir;
    }
}
