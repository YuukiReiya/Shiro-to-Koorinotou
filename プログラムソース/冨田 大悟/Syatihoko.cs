using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// しゃちほこの敵
/// 製作者：冨田 大悟
/// </summary>
public class Syatihoko : Enemy {


    [SerializeField]
    Vector3 boxSize;
    [SerializeField]
    float raySize;

    [SerializeField]
    bool fall = false;
    bool fallOne = false;

    [SerializeField]
    GameObject ef;

    [SerializeField]
    GameObject rotateObj;

    [SerializeField]
    GameObject breakEffect;

    [SerializeField]
    AudioClip dosun;
    [SerializeField]
    AudioClip dosundd;

    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();

        if (!fallOne)
        {
            if (!fall)
            {
                if (!Physics.BoxCast(enemyCenterPos, boxSize, Vector3.down, new Quaternion(0, 0, 0, 0), raySize, LayerName.GetWallObjectHitLayer()) && GetComponent<Rigidbody>().velocity.y < 0)
                {
                    Collider[] collid = Physics.OverlapBox(enemyCenterPos, boxSize, new Quaternion(0, 0, 0, 0), LayerMask.GetMask(LayerName.BLOCK), QueryTriggerInteraction.Ignore);
                    if (collid.Length == 0)
                    {
                        fall = true;
                    }
                }
            }
            else//落下
            {
                RaycastHit hit;

                Collider[] collid = Physics.OverlapBox(enemyCenterPos, boxSize, new Quaternion(0, 0, 0, 0), LayerMask.GetMask(LayerName.BLOCK), QueryTriggerInteraction.Ignore);

                bool collhit = false;
                if (collid.Length > 0)
                {
                    if ((collid[0].gameObject.layer & LayerName.GetWallObjectHitLayer())!=0)
                    {
                        FallHitWall();
                    }
                    else
                    {

                        FallHitBlock(collid[0].gameObject);

                    }
                }
                if (!collhit && Physics.BoxCast(enemyCenterPos, boxSize, Vector3.down, out hit, new Quaternion(0, 0, 0, 0), raySize,LayerName.GetObjectHitLayer(), QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.CompareTag("wall")|| hit.transform.CompareTag("endWall"))
                    {
                        FallHitWall();
                    }
                    else
                    {
                        FallHitBlock(hit.transform.gameObject);
                    }
                }



            }
        }
    }

    private void FallHitBlock(GameObject hit)
    {
        Instantiate(breakEffect, hit.transform.position, breakEffect.transform.rotation);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        SEMaster.Play(dosundd);
        Destroy(hit.transform.gameObject);
    }
    private void FallHitWall()
    {
        Debug.Log("どっスん");
        fall = false;
        fallOne = true;
        SEMaster.Play(dosun);

        Instantiate(ef, transform.position, ef.transform.rotation);
    }

    [ContextMenu("Rotate")]
    public override void RotateEnemy()
    {
        base.RotateEnemy();
        transform.Rotate(0, -180, 0);

        //GetComponent<EnemyCollider>().RotateColliderCenterAddPos();
        aiceBlockObjPopRotation = new Vector3(aiceBlockObjPopRotation.x, -aiceBlockObjPopRotation.y, aiceBlockObjPopRotation.z);
        aiceBlockPopAddPos = new Vector3(-aiceBlockPopAddPos.x, aiceBlockPopAddPos.y, aiceBlockPopAddPos.z);
    }
}
