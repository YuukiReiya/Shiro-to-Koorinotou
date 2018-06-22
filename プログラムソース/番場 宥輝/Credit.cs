using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クレジットの処理を行うクラス
/// 制作者:番場 宥輝
/// </summary>
public class Credit : MonoBehaviour
{

    private ChangeScale wCredit;            //ウィンドウ
    [SerializeField]
    private TitleBackShadows backShadow;    //バックのシャドウ

    void Start()
    {
        wCredit = GetComponent<ChangeScale>();  //取得
    }
    /// <summary>
    /// クレジットのアップデート関数
    /// </summary>
    public void CreditUpdate()
    {
        if (!wCredit.isChanging && !wCredit.isMaxScale && KeyLoader.keyLoader.A)
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));
            wCredit.StartExpansion();
            StartCoroutine(backShadow.StartFadeIn());
        }
        else if (!wCredit.isChanging && wCredit.isMaxScale && (KeyLoader.keyLoader.A || KeyLoader.keyLoader.B))
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Cancel));
            wCredit.StartNarrow();
            StartCoroutine(backShadow.StartFadeOut());
        }
    }
    /// <summary>
    /// クレジットの処理が終了しているか判定する関数
    /// </summary>
    /// <returns>true:終了,false:継続</returns>
    public bool isCerditEnd()
    {
        if (!wCredit.isChanging && !wCredit.isMaxScale)
        {
            return true;
        }
        return false;
    }

}
