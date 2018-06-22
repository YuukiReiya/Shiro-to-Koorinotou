using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのダメージを制御
/// 製作者：冨田 大悟
/// </summary>
public class PlayerDamage : MonoBehaviour {

    [SerializeField]
    Player player;

    PlayerAnimation animes;

    [SerializeField, Header("Damage")]
    int invincibleTime = 50;
    int invincibleCount;


    [SerializeField]
    private int stanTime = 20;
    int flashingCount;
    [SerializeField]
    int flashingOnTime = 2;
    [SerializeField]
    int flashingOffTime = 3;

     public int confusionCount;//行動不能時間

    //点滅参照用
    [SerializeField]
    GameObject flashingObj;

    [SerializeField, Header("Vibration")]
    float vibrationTime;//秒
    [SerializeField]
    Vector2 vibrationPower;


    /// <summary>
    /// プレイヤーのダメージ状態を元に戻す
    /// </summary>
    public void DamageCansel()
    {
        invincibleCount = 0;
        flashingObj.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    /// <summary>
    /// プレイヤーにダメージを与える
    /// </summary>
    public void Damage()
    {
        if (invincibleCount == 0)
        {
            confusionCount = stanTime;
            invincibleCount = invincibleTime + stanTime;
            animes.DamageAnimation();
            StartCoroutine(KeyLoader.keyLoader.Vibration(vibrationTime,vibrationPower.x,vibrationPower.y));
        }
    }

    /// <summary>
    /// プレイヤーの点滅処理
    /// </summary>
    public void Invicile()
    {
        if (invincibleCount > 0)
        {
            invincibleCount--;

            int inam = invincibleCount % (flashingOffTime + flashingOnTime);

            if (inam >= flashingOnTime)
            {
                flashingObj.SetActive(false);
            }
            else
            {
                flashingObj.SetActive(true);
            }
            gameObject.layer = LayerMask.NameToLayer("FlashingPlayer");

        }
        else
        { 
                flashingObj.SetActive(true);

                gameObject.layer = LayerMask.NameToLayer("Player");

        }

    }

    
    /// <summary>
    /// プレイヤーのアニメをセットする
    /// </summary>
    /// <param name="_animes">セットするアニメ</param>
    public void SetAnimes(PlayerAnimation  _animes)
    {
        animes = _animes;
    }
    
    
}
