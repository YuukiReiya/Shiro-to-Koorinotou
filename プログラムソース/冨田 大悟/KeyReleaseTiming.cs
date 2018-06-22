using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キーの開放タイミング
/// 製作者：冨田 大悟
/// </summary>
[DefaultExecutionOrder(1000)]

public class KeyReleaseTiming : MonoBehaviour {

    KeyLoader keyLoder;

	// Use this for initialization
	void Start () {
        keyLoder = GetComponent<KeyLoader>();
    }

    private void FixedUpdate()
    {

        keyLoder.KeyRelease();
    }
}
