using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 爆発する敵
/// 製作者：冨田 大悟
/// </summary>
public class Wamonn : Enemy {

    [SerializeField]
    int moveDir;

    [SerializeField]
    float speed;
    [SerializeField]
    float boostSpeed;

    [SerializeField]
    float boostOnTime;
    [SerializeField]
    float boostOffTime;
    float boostCount;

    [SerializeField]
    float retrunRaySize;
    [SerializeField]
    Vector3 retrunRayBoxSize;

    [SerializeField]
    Vector3 leftRotate;
    [SerializeField]
    Vector3 rightRotate;

    bool explosiondd =false;

    [SerializeField]
    float explosionRadius;
    [SerializeField]
    GameObject explosionEfect;
    [SerializeField]
    AudioClip explosionSound;

    [SerializeField]
    GameObject ef;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        boostCount = 0;
        if (moveDir > 0)
        {
            this.transform.rotation = Quaternion.Euler(rightRotate);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(leftRotate);

        }
    }

    private void FixedUpdate()
    {
        if (!IsWallHit())
        {

            if (boostCount % (boostOnTime + boostOffTime) < boostOnTime)
            {
                GetComponent<Rigidbody>().position += new Vector3(moveDir * boostSpeed, 0, 0);
            }else
            {
                GetComponent<Rigidbody>().position += new Vector3(moveDir * speed, 0, 0);
            }

            if(Physics.BoxCast(enemyCenterPos, retrunRayBoxSize, new Vector3 (moveDir,0,0),new Quaternion(0, 0, 0, 0), retrunRaySize, LayerMask.GetMask(LayerName.BLOCK, LayerName.WALL), QueryTriggerInteraction.Ignore)){
                changeRotation();
            }

            

            boostCount++;

            SetColliderCenter();
        }
    }

    private void changeRotation()
    {
        moveDir = -moveDir;
        if (moveDir > 0)
        {
            this.transform.rotation = Quaternion.Euler(rightRotate);
        }
        else { 
            this.transform.rotation = Quaternion.Euler(leftRotate);

        }
    }

    void Explosion()
    {
        if (!explosiondd)
        {
            explosiondd = true;
            Instantiate(explosionEfect, enemyCenterPos, new Quaternion(0, 0, 0, 0));
            SEMaster.Play(explosionSound);
            Destroy(gameObject);

            RaycastHit[] hits;
            hits = Physics.SphereCastAll(enemyCenterPos, explosionRadius, Vector3.up, 0.001f, LayerMask.GetMask(LayerName.BLOCK, LayerName.ENEMY), QueryTriggerInteraction.Ignore);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.CompareTag("blockObj"))
                {
                    Debug.Log("Explosion name =" + LayerMask.LayerToName(hit.transform.gameObject.layer));
                    Destroy(hit.transform.gameObject);
                    Instantiate(ef, hit.transform.position, new Quaternion(0, 0, 0, 0));

                }
                else if (hit.transform.GetComponent<Wamonn>() != null)
                {
                    hit.transform.GetComponent<Wamonn>().Explosion();
                }
            }
        }
    }

    public override void PlayerHit()
    {
        Explosion();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy")){
            PlayerHit();
        }
    }

    public override void RotateEnemy()
    {
        base.RotateEnemy();
        moveDir = -moveDir;
    }
}
