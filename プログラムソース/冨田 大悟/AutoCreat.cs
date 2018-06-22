using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 開始時に自動生成されるオブジェクト群
/// 製作者：冨田 大悟
/// </summary>
public class AutoCreat : MonoBehaviour {

    [SerializeField]
     List<GameObject> goList;

    private void Awake()
    {
        foreach(GameObject go in goList)
        {
            Instantiate(go, this.transform);
        }
        Destroy(this);
    }

    [ContextMenu("Set")]
    void Set()
    {
        foreach (GameObject go in goList)
        {
            Instantiate(go, this.transform);
        }
    }
    [ContextMenu("Destroy")]
    void DestroyGo()
    {
        foreach(Transform tr in GetComponentsInChildren<Transform>())
        {
            if(tr != transform)
            {
                DestroyImmediate(tr.gameObject);
            }
        }
    }
}
