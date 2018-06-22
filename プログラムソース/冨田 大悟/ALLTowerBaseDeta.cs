using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// オールタワーの全ステージ共通な値をここに保存
/// 製作者：冨田 大悟
/// </summary>
public class ALLTowerBaseDeta : MonoBehaviour {
    [SerializeField, Header("Sound")]
    public AudioClip clearSE;
    public AudioClip clearBGM;
    [SerializeField, Header("StartCreatPrefabs")]
    public StartCreatPrefabs startCreatPrefabs;
}
