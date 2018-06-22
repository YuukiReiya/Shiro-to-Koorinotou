using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 接触時消去
/// 製作者：冨田 大悟
/// </summary>
public class USD : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
