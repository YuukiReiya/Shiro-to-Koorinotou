using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// ゲームオーバー　ゲームクリアーのキー選択継承元
/// 製作者：冨田 大悟
/// </summary>
public class NextSelect : MonoBehaviour {

    [SerializeField]
    protected int selectNam = 0;

    protected const string stageSelectSceneName = "StageSelect";

    [SerializeField]
    protected int keyWaitTime = 15;

    [SerializeField]
    protected Image selct;
    [SerializeField]
    protected Image yes, no;

    [SerializeField]
    protected Color yesSelectColor;
    [SerializeField]
    protected Color noSelectColor;
    [SerializeField]
    protected Color notSelectColor;

    [SerializeField]
    protected Sprite notYes;
    [SerializeField]
    protected Sprite notNo;
    [SerializeField]
    protected Sprite selectYes;
    [SerializeField]
    protected Sprite selectNo;

    virtual protected void SelectNam()
    {
        if (selectNam == 0)
        {
            yes.color = yesSelectColor;
            no.color = notSelectColor;
        }
        else
        {
            yes.color = notSelectColor;
            no.color = noSelectColor;
        }
        if (selectNam == 0)
        {
            yes.sprite = selectYes;
            no.sprite = notNo;
        }
        else
        {

            yes.sprite = notYes;
            no.sprite = selectNo;
        }
    }

    virtual public void NextSelectStart()
    {
        this.gameObject.SetActive(true);
        KeyLoader.keyLoader.SetKeyWait(30);
        KeyLoader.keyLoader.SetStickWait(30);

    }

    virtual public void NextSelectUpdate() { }

    virtual public string GetNextSceneName() { return ""; }

    protected virtual void OnValidate()
    {
        SelectNam();
    }

    protected void KeyMove()
    {
        if (KeyLoader.keyLoader.joyStickAxis.x != 0 && !KeyLoader.keyLoader.StickWait)
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            selectNam += 1;
            selectNam %= 2;
            KeyLoader.keyLoader.SetStickWait(keyWaitTime);
            SelectNam();
        }
        else if (KeyLoader.keyLoader.joyStickAxis.x == 0)
        {
            KeyLoader.keyLoader.SetStickWait(0);

        }
    }
}
