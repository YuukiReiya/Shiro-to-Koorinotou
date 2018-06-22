using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 幽霊の敵
/// 製作者：冨田 大悟
/// </summary>
public class Phantom : Enemy {

    [SerializeField]
    string layerName;
    [SerializeField]
    string invilayerName;

    int flashingCount = 0;
    [SerializeField]
    int flashingOnTime = 2;
    [SerializeField]
    int flashingOffTime = 3;

    Material baseColor;
    [SerializeField]
    Material flashingColor;

    protected override void Start()
    {
        base.Start();

        baseColor = model.GetComponent<SkinnedMeshRenderer>().sharedMaterial;
    }


    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();
        flashingCount++;
        if(flashingCount % (flashingOffTime + flashingOnTime) > flashingOnTime)
        {
            gameObject.layer = LayerMask.NameToLayer(invilayerName);
            model.GetComponent<SkinnedMeshRenderer>().material = flashingColor;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer(layerName);
            model.GetComponent<SkinnedMeshRenderer>().material= baseColor;

        }
    }
}
