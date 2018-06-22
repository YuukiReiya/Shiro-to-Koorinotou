using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指定フレーム後に自動でデストロイされる
/// 製作者：冨田 大悟
/// </summary>
public class AutoDestroy : MonoBehaviour {

    [SerializeField]
    bool toAwake;
    [SerializeField]
    int destroyFlame;

    private void Start()
    {
        if(toAwake)
        StertAutoDetroy(destroyFlame);
    }
    


    public IEnumerator AutoDestroyObj(GameObject obj,int flame)
    {
        while (flame-->0)
        {
            yield return new WaitForFixedUpdate();
        }
        GameObject.Destroy(obj);
    }

    public void StertAutoDetroy(int flame)
    {
        StartCoroutine( AutoDestroyObj(this.gameObject, flame));
    }
}
