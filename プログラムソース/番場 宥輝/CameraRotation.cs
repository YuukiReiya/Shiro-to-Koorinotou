using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクト(カメラ)を回転させるクラス
/// 制作者:番場 宥輝
/// </summary>
public class CameraRotation : MonoBehaviour {

    [SerializeField]
    private float[] targetAngle_y;         //y軸に対し回転させるオブジェクトのローカル座標
    private const int amountOfRotation = 4;//回転量

    public bool isRotation
    {
        get;
        private set;
    }//true:回転中,false:回転してない

   
    void Start () {
        isRotation = false;
        this.transform.localEulerAngles = new Vector3(0, targetAngle_y[StageSelect.stage], 0);
	}

    /// <summary>
    /// 配列に格納した角度まで右に回転する関数
    /// </summary>
    /// <param name="index">配列の添え字</param>
    /// <returns></returns>
    public IEnumerator Right(int index)
    {
        //角度差を求めて回転量より差が大きければ回転の処理を行う
        while (Mathf.DeltaAngle(this.transform.eulerAngles.y, targetAngle_y[index]) >= amountOfRotation)
        {
            //回転
            this.transform.Rotate(new Vector3(0, amountOfRotation));
            isRotation = true;
            yield return new WaitForEndOfFrame();
        }
        //回転が終了したら値を直接代入し、isRotationをfalseにする
        {
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, targetAngle_y[index], this.transform.eulerAngles.z);
        }
        isRotation = false;
    }
    /// <summary>
    /// 配列に格納した角度まで左に回転する関数
    /// </summary>
    /// <param name="index">配列の添え字</param>
    /// <returns></returns>
    public IEnumerator Left(int index)
    {
        //角度差を求めて回転量より差が大きい間、回転の処理を行う
        while (Mathf.DeltaAngle(targetAngle_y[index], this.transform.eulerAngles.y) >= amountOfRotation)
        {
            //回転
            this.transform.Rotate(new Vector3(0, -amountOfRotation));
            isRotation = true;
            yield return new WaitForEndOfFrame();
        }
        //回転が終了したら値を直接代入し、isRotationをfalseにする
        {
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, targetAngle_y[index], this.transform.eulerAngles.z);
        }
        isRotation = false;
    }
}
