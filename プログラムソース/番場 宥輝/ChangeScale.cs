using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオブジェクトの拡大・縮小を行うクラス
/// 制作者:番場宥輝
/// </summary>
public class ChangeScale : MonoBehaviour {

    [SerializeField]
    Vector3 MaxScale = new Vector3(1, 1, 0);    //拡大する最大値
    [SerializeField]
    Vector3 MinScale = Vector3.zero;            //縮小する最小値
    [SerializeField]
    private float ScalePower = 0.05f;           //1フレームあたりの大きさ変位量

    /// <summary>
    /// ステート確認用プロパティ
    /// </summary>
    public bool isChanging      //変更中
    {
        get;
        private set;
    }

    public bool isMaxScale      //大きさが最大
    {
        get;
        private set;
    }


    void Start () {
        this.gameObject.transform.localScale = MinScale;        //アタッチされたゲームオブジェクトの取得
    }
    /// <summary>
    /// 縮小を開始する関数
    /// </summary>
    public void StartNarrow()
    {
        StartCoroutine(Narrow());
    }
    /// <summary>
    /// 拡大を開始する関数
    /// </summary>
    public void StartExpansion()
    {
        StartCoroutine(Expansion());
    }
    /// <summary>
    /// 拡大するコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator Expansion()
    {
        isChanging = true;
        while(this.gameObject.transform.localScale.x < MaxScale.x
            && this.gameObject.transform.localScale.y < MaxScale.y)
        {
            this.gameObject.transform.localScale += new Vector3(ScalePower, ScalePower);
            yield return new WaitForFixedUpdate();
        }
        isChanging = false;
        isMaxScale = true;
    }
    /// <summary>
    /// 縮小するコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator Narrow()
    {
        isChanging = true;
        while (this.gameObject.transform.localScale.x > MinScale.x
           && this.gameObject.transform.localScale.y > MinScale.y)
        {
            this.gameObject.transform.localScale -= new Vector3(ScalePower, ScalePower);
            yield return new WaitForFixedUpdate();
        }
        isChanging = false;
        isMaxScale = false;
    }

}
