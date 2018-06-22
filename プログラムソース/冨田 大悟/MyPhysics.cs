using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自作したあたり判定
/// 製作者：冨田 大悟
/// </summary>
public static class MyPhysics
{

    public static bool MyBoxCast(Vector3 pos,Vector3 half,Vector3 dir,float max, int layerMask = ~0, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore)
    {
        //第1Ray
        if (Physics.BoxCast(pos,half,dir, new Quaternion(0,0,0,0),max,layerMask, queryTriggerInteraction))
        {
            return true;
        }

        //第2
        if (Physics.CheckBox(pos,half*0.9f,new Quaternion(0,0,0,0),layerMask,queryTriggerInteraction))
        {
            return true;

        }

        return false;
    }

    public static bool ObjectIns(GameObject ins,GameObject outs)
    {
        Vector2[] insPos = new Vector2[2];
        insPos[0] = new Vector2(ins.transform.position.x - ins.transform.localScale.x / 2, ins.transform.position.y - ins.transform.localScale.y / 2);

        insPos[1] = new Vector2( ins.transform.position.x + ins.transform.localScale.x/2, ins.transform.position.y + ins.transform.localScale.y / 2);

        Vector2[] outsPos = new Vector2[2];
        outsPos[0] = new Vector2(outs.transform.position.x - outs.transform.localScale.x / 2, outs.transform.position.y - outs.transform.localScale.y / 2);

        outsPos[1] = new Vector2(outs.transform.position.x + outs.transform.localScale.x / 2, outs.transform.position.y + outs.transform.localScale.y / 2);

        if (insPos[0].x > outsPos[0].x && insPos[1].x < outsPos[1].x)
        {
            
            if (insPos[0].y > outsPos[0].y && insPos[1].y < outsPos[1].y)
                return true;
        }
            return false;
    }
}
