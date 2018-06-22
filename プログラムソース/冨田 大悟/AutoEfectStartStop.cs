using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指定フレーム後にエフェクトの停止をする
/// 製作者：冨田 大悟
/// </summary>
public class AutoEfectStartStop : MonoBehaviour {

    [SerializeField]
    bool toAwakeStert;
    [SerializeField]
    bool toAwakeStop;
    [SerializeField]
    int stopFlame;
    [SerializeField]
    int stertFlame;

    //OldEffect moveEfect;

    private void Start()
    {
       // moveEfect = GetComponent<OldEffect>();

        if (toAwakeStop)
            StertAutoEfectStop(stopFlame);
        if (toAwakeStert)
            StertAutoEfectStert(stertFlame);
    }



    public IEnumerator AutoEfectStopObj(GameObject obj, int flame)
    {
        while (flame-- > 0)
        {
            yield return new WaitForFixedUpdate();
        }
        GetComponent<OldEffect>().Stop();
    }

    public void StertAutoEfectStop(int flame)
    {
        StartCoroutine(AutoEfectStopObj(this.gameObject, flame));
    }


    public IEnumerator AutoEfectStertObj(GameObject obj, int flame)
    {
        while (flame-- > 0)
        {
            yield return new WaitForFixedUpdate();
        }
        GetComponent<OldEffect>().Play();
    }

    public void StertAutoEfectStert(int flame)
    {
        StartCoroutine(AutoEfectStertObj(this.gameObject, flame));
    }
}
