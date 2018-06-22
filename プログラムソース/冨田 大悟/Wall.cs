using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 壁
/// 製作者：冨田 大悟
/// </summary>
public class Wall : MonoBehaviour
{ 
    public bool playerHit {
        get;
        private set;
    }

    // Use this for initialization
    void Start()
    {
        playerHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
                playerHit = true;
        }
    }
}
