using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージ突入時の"ジャンプ"と"魔法"のUI画像を設定したものに更新するクラス
/// 制作者:番場 宥輝
/// </summary>
public class KeyUIGraph : MonoBehaviour {

    [SerializeField]
    private Image iJump;
    [SerializeField]
    private Image iMagic;
    [SerializeField]
    private Sprite[] key;
    int jump;
    int magic;
    private void Start()
    {
        UpdateKeyGraph();
    }

    /// <summary>
    /// セーブデータを読み込みUIの画像を更新する関数
    /// </summary>
    public void UpdateKeyGraph()
    {
        //セーブデータの読み込み
        jump = SaveData.GetInt("JumpKey");
        magic = SaveData.GetInt("MagicKey");
        //読み込んだ値に応じた画像に変更
        iJump.sprite = key[jump];
        iMagic.sprite = key[magic];
    }

  
}
