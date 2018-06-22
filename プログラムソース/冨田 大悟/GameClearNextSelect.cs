using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// クリア時のキー選択
/// 製作者：冨田 大悟
/// </summary>
public class GameClearNextSelect : NextSelect
{
    bool laseStage;

    private void Start()
    {
        laseStage = SceneManager.GetActiveScene().name.EndsWith("6");
        if (laseStage)
        {
            selct.gameObject.SetActive(false);
            yes.gameObject.SetActive(false);
            no.gameObject.SetActive(false);
        }
        else
        {
            SelectNam();
        }
    }

    public override void NextSelectUpdate()
    {
        base.NextSelectUpdate();

        // 末尾の文字列と一致するかどうかを判断する
        if (!laseStage)
        {
            //ステージ6以外
            KeyMove();
        }
    }


    public override string GetNextSceneName()
    {
        if(selectNam == 0)
        {
            if (laseStage)
            {
                return stageSelectSceneName;
            }
            else
            {
                int nextTowerNam = int.Parse(SceneManager.GetActiveScene().name.Substring(0, 1));
                int nextStageNam = int.Parse(SceneManager.GetActiveScene().name.Substring(2)) + 1;

                return nextTowerNam + "-" + nextStageNam;
            }
             
        }else
        {
            return stageSelectSceneName;
        }
    }

    
}
