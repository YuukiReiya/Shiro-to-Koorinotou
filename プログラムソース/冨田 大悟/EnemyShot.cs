using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の玉
/// 製作者：冨田 大悟
/// </summary>
public class EnemyShot : PlayerShot {
    [SerializeField]
     GameObject wallBarrnEf;

    private void OnCollisionEnter(Collision collision)
    {

        Vector3 pos = collision.contacts[0].point;
        if (collision.gameObject.CompareTag("Player"))
        {

            //createHitF(collision, false);
            //AudioMaster.Play(hitSound);
            collision.gameObject.GetComponent<PlayerDamage>().Damage();
            Instantiate(hitef, pos, new Quaternion(0, 0, 0, 0));

            Destroy(this.gameObject);
        }else 
        {
            Instantiate(hitef,pos, new Quaternion(0, 0, 0, 0));
            Destroy(this.gameObject);

        }
    }

    public void WallebatanEffect()
    {
            Instantiate(wallBarrnEf, transform.position, new Quaternion(0, 0, 0, 0));

    }
}
