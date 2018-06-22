using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラのシェイク処理
/// 製作者：冨田 大悟
/// </summary>
public class CameraShake : MonoBehaviour {

    public float shake_decay = 0.002f;
    public float coef_shake_intensity = 0.3f;
    private Vector3 originPosition;
    private Quaternion originRotation;
    private float shake_intensity;

    private Quaternion stRotate;

    void Update()
    {
        if (shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
                                             originRotation.x ,//+ Random.Range(-shake_intensity, shake_intensity),
                                             originRotation.y + Random.Range(-shake_intensity, shake_intensity),
                                             originRotation.z ,//+ Random.Range(-shake_intensity, shake_intensity),
                                             originRotation.w );//+ Random.Range(-shake_intensity, shake_intensity));
            shake_intensity -= shake_decay;
            if(shake_intensity == 0)
            {
                this.transform.rotation = stRotate;
            }
        }
    }

    /// <summary>
    /// カメラを揺らす
    /// </summary>
    public void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        shake_intensity = coef_shake_intensity;
        stRotate = this.transform.rotation;
    }

}
