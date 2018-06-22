using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// まとめて影を消す
/// 製作者：冨田 大悟
/// </summary>
public class ShadowOff : MonoBehaviour {

    [SerializeField]
    GameObject go;

    [ContextMenu("de")]
    void de()
    {
        foreach (MeshRenderer m in go.GetComponentsInChildren<MeshRenderer>())
        {
            m.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }
}
