using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 解凍と凍結を持つ玉
/// 製作者：冨田 大悟
/// </summary>
public class FAndDShot : PlayerShot {

    [SerializeField]
    GameObject DEfect;
    [SerializeField]
    GameObject wallHitEfect;

    bool hitOne = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hitOne)
        {
            if (collision.transform.CompareTag("blockObj"))//ブロック
            {
                collision.transform.GetComponent<BlockObj>().DecompressionObj();
                SEMaster.Play(hitSound);

                createHitF(collision, true, DEfect);
                Destroy(this.gameObject);
                hitOne = true;
            }
            else if (collision.transform.CompareTag("enemy"))//敵
            {
                createHitF(collision, true);

                collision.transform.GetComponent<Enemy>().CangeObj();

                SEMaster.Play(hitSound);
                Destroy(this.gameObject);
                hitOne = true;
            }
            else if (!(collision.gameObject.CompareTag("Player")))
            {
                createHitF(collision, false, wallHitEfect);
                SEMaster.Play(hitSound);

                Destroy(this.gameObject);
                hitOne = true;

            }
            else
            {
                Debug.Log("プレイヤーに当たった");
            }

            //Debug.Log("name = " + collision.transform.name);
        }
    }
}
