using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 氷のブロック
/// 製作者：冨田 大悟
/// </summary>
public class BlockObj : MonoBehaviour {

    public static bool playerDeid;

    public Enemy enemy;

    [SerializeField]
    GameObject wallHitEffect;

    /// <summary>
    /// 氷を解かす
    /// </summary>
    public void DecompressionObj()
    {

        if (enemy != null)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.parent = this.transform.parent;
            enemy = null;
        }
        Destroy(this.gameObject);
    }

    public void FixedUpdate()
    {
        Vector3 r = GetComponent<Rigidbody>().velocity;
        if (r.y > 0)
            GetComponent<Rigidbody>().velocity = new Vector3(r.x, 0, r.z);

        GetComponent<Rigidbody>().WakeUp();
    }

    private void OnDestroy()
    {
        if (enemy != null)
        {
            Destroy(enemy.gameObject);
        }
    }

    /// <summary>
    /// モデルを入れる
    /// </summary>
    /// <param name="go">入れるモデル</param>
    public void SetModel(GameObject go)
    {
        Vector3 bp = go.transform.localPosition;
        go.transform.parent = this.transform;
        go.transform.localPosition = bp;
    }

    /// <summary>
    /// 壁に当たった時のエフェクトを生成する
    /// </summary>
    /// <param name="xPos">生成する位置</param>
    public void CreatEfect(float xPos)
    {
        GameObject go = Instantiate(wallHitEffect, new Vector3(xPos, transform.position.y, transform.position.z), new Quaternion(0, 0, 0, 0));
        go.AddComponent<AutoDestroy>().StertAutoDetroy(10);
    }

    /// <summary>
    ///フロアの回転による回転
    /// </summary>
    public void RotateObj()
    {
        transform.Rotate(0, 180, 0);
        if (enemy != null)
        {
            enemy.transform.Rotate(0, 180, 0);
            enemy.FloorRotate();
        }
    }
}
