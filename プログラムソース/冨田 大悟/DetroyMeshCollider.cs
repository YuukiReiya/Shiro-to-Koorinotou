using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メッシュコライダーをまとめて消す
/// </summary>
public class DetroyMeshCollider : MonoBehaviour {

    [SerializeField]
    GameObject go;


    [ContextMenu("de")]
    void de()
    {
        foreach(MeshCollider m in go.GetComponentsInChildren<MeshCollider>())
        {
            DestroyImmediate(m);
        }
    }
}
