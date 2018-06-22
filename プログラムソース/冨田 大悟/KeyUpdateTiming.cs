using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キーの更新タイミング
/// 製作者：冨田 大悟
/// </summary>
[DefaultExecutionOrder(-100)]

public class KeyUpdateTiming : MonoBehaviour {


    KeyLoader keyLoder;

    private void Awake()
    {
        keyLoder = GetComponent<KeyLoader>();
    }

    // Update is called once per frame
    void Update () {
        keyLoder.KeyUpdate();

    }
}
