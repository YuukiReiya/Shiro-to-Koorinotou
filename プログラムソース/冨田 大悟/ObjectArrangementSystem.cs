using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクト配置システム
/// 製作者：冨田 大悟
/// </summary>
public class ObjectArrangementSystem : EnemyPopSystem {

    [SerializeField]
    GameObject leftArrangementObjectParent;
    [SerializeField]
    GameObject rightArrangementObjectParent;

    [SerializeField]
    bool simpleRotation;

    [SerializeField]
    int popTiemTime;
    int popTimeCount;

    private void Awake()
    {
        leftArrangementObjectParent.SetActive(false);
        if (rightArrangementObjectParent != null)
        {
            rightArrangementObjectParent.SetActive(false);
        }

    }

    public override void EPSFOne()
    {
        leftArrangementObjectParent.SetActive(true);
    }

    // Update is called once per frame

    override public void EPSStart()
    {
    }

    override public void EPSUpdate()
    {

    }

    override public void CamaeraUpStert()
    {
        popTimeCount = popTiemTime;
    }
    override public void CamaeraUpUpdate()
    {
        if (popTimeCount-- == 0)
        {
            if (simpleRotation)
            {
                if (hitBlock == Tower.HitBlock.RIGHTEND)
                {
                    //leftArrangementObjectParent.transform.Rotate(0, 180, 0);


                    foreach (Enemy e in leftArrangementObjectParent.GetComponentsInChildren<Enemy>())
                    {
                        
                        e.transform.position = new Vector3(-e.transform.position.x, e.transform.position.y, e.transform.position.z);
                        e.FloorRotate();
                    }
                    foreach (BlockObj bo in leftArrangementObjectParent.GetComponentsInChildren<BlockObj>())
                    {
                       
                        bo.transform.position = new Vector3(-bo.transform.position.x, bo.transform.position.y, bo.transform.position.z);
                        bo.RotateObj();
                    }

                    leftArrangementObjectParent.SetActive(true);
                }
                else
                {
                    leftArrangementObjectParent.SetActive(true);
                }

            }
            else
            {
                switch (hitBlock)
                {
                    case Tower.HitBlock.LEFTEND:
                        leftArrangementObjectParent.SetActive(true);
                        Destroy(rightArrangementObjectParent);
                        break;

                    case Tower.HitBlock.RIGHTEND:
                        rightArrangementObjectParent.SetActive(true);
                        Destroy(leftArrangementObjectParent);
                        break;
                }
            }
        }
    }
    override public void CamaeraUpEnd()
    {

    }

    public override void TowerEnd()
    {
        base.TowerEnd();
        deleteObj();
    }

    void deleteObj()
    {
        Destroy(leftArrangementObjectParent);
        if(rightArrangementObjectParent != null)
        {
            Destroy(rightArrangementObjectParent);

        }
    }
}
