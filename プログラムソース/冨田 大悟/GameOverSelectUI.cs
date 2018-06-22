using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームオーバーのキー選択
/// 製作者：冨田 大悟
/// </summary>
public class GameOverSelectUI : NextSelect {
    [SerializeField]
    GameObject drawGo;
    [SerializeField]
    int waitTime = 45;

    [SerializeField]
    AudioClip gameOverSound;

    private void Start()
    {
        
        BGMMaster.Stop();
        StartCoroutine(wait());
    }

    public override void NextSelectUpdate()
    {
        if (waitTime <= 0)
        {
            base.NextSelectUpdate();
            KeyMove();
        }
    }

    public override string GetNextSceneName()
    {
        if(selectNam == 0)
        {
            return SceneManager.GetActiveScene().name;
        }else
        {
            return stageSelectSceneName;
        }
    }

    IEnumerator wait()
    {
        drawGo.SetActive(false);
        while (waitTime-->0)
        {
            yield return new WaitForFixedUpdate();
        }
        drawGo.SetActive(true);
        if (gameOverSound != null)
            SEMaster.Play(gameOverSound);
    }
}
