using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ゲームオーバーの文字の移動
/// 製作者：冨田 大悟
/// </summary>
public class GameOverImageMove : MonoBehaviour {

    Vector3 basePos;
    [SerializeField]
    float sinSpeed;
    [SerializeField]
    float sinSize;
    float sinNam =0;

    private void Start()
    {
        basePos = GetComponent<Image>().rectTransform.localPosition;
    }

    private void FixedUpdate()
    {
        GetComponent<Image>().rectTransform.localPosition = basePos + Vector3.up * Mathf.Sin(sinNam) * sinSize;
        sinNam += sinSpeed;
    }
}
