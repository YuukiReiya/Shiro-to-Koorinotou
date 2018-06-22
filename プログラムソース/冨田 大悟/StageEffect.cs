using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージのエフェクトの制御
/// 製作者：冨田 大悟
/// </summary>
public class StageEffect : MonoBehaviour {

    public static StageEffect stageEffect;

    [SerializeField]
    GameObject[] floorParticle;

    private void Awake()
    {
        stageEffect = this;
    }

    public static void EffectActive(int floor)
    {
        for(int i = 0; i < stageEffect.floorParticle.Length; i++)
        {
             foreach(ParticleSystem ps in stageEffect.floorParticle[i].GetComponentsInChildren<ParticleSystem>())
            {
                if (i == floor)
                {
                    ps.Play();
                }else
                {
                    ps.Stop();
                }
            }
        }
    }
    public static void EffectAllStop()
    {
        for (int i = 0; i < stageEffect.floorParticle.Length; i++)
        {
            foreach (ParticleSystem ps in stageEffect.floorParticle[i].GetComponentsInChildren<ParticleSystem>())
            {
                ps.Stop();
            }
        }
    }
}
