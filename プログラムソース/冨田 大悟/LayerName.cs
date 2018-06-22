using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レイヤーの名前
/// 製作者：冨田 大悟
/// </summary>
public class LayerName : MonoBehaviour {

    public const string BLOCK = "block";
    public const string WALL = "wall";
    public const string ENDWALL = "endWall";
    public const string PLAYER = "Player";
    public const string ENEMY = "Enemy";

    public static int GetXMoveHitLayer()
    {
        return LayerMask.GetMask(BLOCK, WALL, ENEMY);
    }
    public static int GetYMoveHitLayer()
    {
        return LayerMask.GetMask(BLOCK, ENDWALL, ENEMY);
    }

    public static int GetMaltMoveHitLayer()
    {
        return LayerMask.GetMask(BLOCK, WALL,ENDWALL, ENEMY);
    }

    public static int GetObjectHitLayer()
    {
        return LayerMask.GetMask(BLOCK, WALL, ENDWALL);
    }
    public static int GetWallObjectHitLayer()
    {
        return LayerMask.GetMask(WALL, ENDWALL);
    }
}
