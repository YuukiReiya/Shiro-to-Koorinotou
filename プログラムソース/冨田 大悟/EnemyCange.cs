using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エディタ専用　エネミーを凍らせた状態にする
/// 製作者：冨田 大悟
/// </summary>
public class EnemyCange : MonoBehaviour {

    public void Cange()
    {
        GetComponent<Enemy>().CangeObj();
        DestroyImmediate(this);
    }
}
