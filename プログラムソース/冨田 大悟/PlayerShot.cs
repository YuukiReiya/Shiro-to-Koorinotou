using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの玉の挙動制御
/// 製作者：冨田 大悟
/// </summary>
public class PlayerShot : MonoBehaviour {

    public Vector2 moves;
    public float speed;

    [SerializeField]
    protected int lifeTime = 45;


    [SerializeField]
    protected AudioClip hitSound;
    [SerializeField]
    protected GameObject hitef;
    [SerializeField]
    protected int efDeleteTime = 60;

    private void FixedUpdate()
    {
        transform.position += new Vector3(moves.x, moves.y) * speed;
        if (lifeTime-- <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// エフェクトの生成
    /// </summary>
    /// <param name="collision">当たった位置</param>
    /// <param name="toHitTergetPos">エフェクトの生成位置は当たったオブジェクトか</param>
    /// <param name="ather">生成するエフェクト</param>
    protected void createHitF(Collision collision, bool toHitTergetPos = false, GameObject ather = null)
    {
        GameObject ef = hitef;
        if (ather != null)
        {
            ef = ather;
        }
        if (ef != null)
        {
            GameObject efObj;
            if (toHitTergetPos)
            {
                efObj = Instantiate(ef, collision.transform.position, new Quaternion(0, 0, 0, 0));
            }
            else
            {
                efObj= Instantiate(ef, transform.position, new Quaternion(0, 0, 0, 0));
            }
            efObj.AddComponent<AutoDestroy>().StertAutoDetroy(efDeleteTime);
        }

    }


}
