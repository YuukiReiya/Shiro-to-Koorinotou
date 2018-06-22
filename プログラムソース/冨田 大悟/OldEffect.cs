using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 古いエフェクトの停止、開始
/// 製作者：冨田 大悟
/// </summary>
public class OldEffect : MonoBehaviour {
    [SerializeField]
    EllipsoidParticleEmitter[] epe;

    public void Stop() {

        foreach(var e in epe)
        {
            e.emit = false;
        }
    }
    public void Play()
    {
        foreach (var e in epe)
        {
            e.emit = true;
        }
    }
}
