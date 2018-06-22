using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フロア(GameObject)がシュッと出て来る関数
/// 制作者:番場 宥輝
/// </summary>
public class FloorMoveObj : MonoBehaviour {

    [SerializeField]
    private GameObject[] floor;             //動かすオブジェクト
    private const float initialpos = 10;    //初期x位置
    private float[] targetpos;              //目標x座標
    private const float speed = 2.5f;       //移動速度

    public bool isMoving { get; private set; }//true:移動中,false:移動してない

    /// <summary>
    /// フロアの初期位置をinitialpos分左にズラした値にする
    /// </summary>
	void Start () {
        targetpos = new float[floor.Length];
        for (int i = 0; i < floor.Length; i++)
        {
            targetpos[i] = floor[i].transform.localPosition.x;
            floor[i].transform.localPosition =
                new Vector3(floor[i].transform.localPosition.x - initialpos,
                floor[i].transform.localPosition.y, floor[i].transform.localPosition.z);
        }
	}
    /// <summary>
    /// フロア1~6まで順番にスライド移動しながら出てくるコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator SlidePut()
    {
        isMoving = true;
        for(int i=0;i<floor.Length;)
        {
            if(floor[i].transform.localPosition.x<targetpos[i])
            {
                floor[i].transform.Translate(speed, 0, 0);
            }
            else
            {
                i++;
            }
            yield return new WaitForEndOfFrame();
        }
        isMoving = false;
    }
    /// <summary>
    /// フロア1~6まで順番にスライド移動しながら画面外に移動するコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator SlideOut()
    {
        isMoving = true;
        for (int i = Menu.FLOOR6; i >= Menu.FLOOR1;) 
        {
            if (floor[i].transform.localPosition.x > (targetpos[i] - initialpos)) 
            {
                floor[i].transform.Translate(-speed, 0, 0);
            }
            else
            {
                i--;
            }
            yield return new WaitForEndOfFrame();
        }
        isMoving = false;
    }

}
