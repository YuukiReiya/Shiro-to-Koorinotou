using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラをアイドル(遊泳)状態にする
/// 制作者:番場宥輝
/// </summary>
public class CameraIdoling : MonoBehaviour {

    [SerializeField]
    private GameObject CameraObj;           //遊泳させるゲームオブジェクト
    [SerializeField]
    private float x = 0f;                   //回転させるx量
    [SerializeField]
    private float y = 1f;                   //回転させるy量
    [SerializeField]
    private float z = 0f;                   //回転させるz量
    void Start () {
        StartCoroutine(StartIdoling());
	}
	
    /// <summary>
    /// 常にオブジェクトを回転させるコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartIdoling()
    {
        while(true)
        {
            ObjRotate();
            yield return new WaitForEndOfFrame();
        }
    }
    /// <summary>
    /// オブジェクトを回転させる関数
    /// </summary>
    private void ObjRotate()
    {
        CameraObj.transform.Rotate(x, y, z);
    }
}
