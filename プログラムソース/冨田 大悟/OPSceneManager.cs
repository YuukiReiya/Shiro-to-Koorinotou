using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// オープニングのシーン制御
/// 製作者：冨田 大悟
/// </summary>
public class OPSceneManager : MonoBehaviour {

    Load load;

    [SerializeField]
    VideoPlayer videoPlayer;
    bool playEnd=false;

    [SerializeField]
    OPPushAnyButton pushAnyButton;
    bool pushAnyButtonDraw;

    [SerializeField]
    float pushAnyButtonDrawWaitTime;

    [SerializeField]
    int endTime = 15;

    private void Start()
    {
        Application.targetFrameRate = 60;

        load = gameObject.AddComponent<Load>();

        KeyLoader.keyLoader.SetKeyWait(10);
        Load.SetNextSceneName(SceneNames.TitleName);
        StartCoroutine( load.LoadScene(false, 1));
        Invoke("pushAnyButtonSetDraw", pushAnyButtonDrawWaitTime);
    }

    private void FixedUpdate()
    {
        if ((long)videoPlayer.frameCount- endTime <= videoPlayer.frame && !playEnd)
        {
            playEnd = true;
            videoPlayer.Pause();
            StartCoroutine(SceneWait(40));
            Title.fadeIsBlack = true;
            FadeIO.fadeIo.FadeOut(40);
            BGMMaster.AudioFadeOutStart(40);
        }
        else if (KeyLoader.keyLoader.IsAnyKey() && !playEnd)
        {

            playEnd = true;
            videoPlayer.Pause();
            StartCoroutine(SceneWait(40));
            Title.fadeIsBlack = true;
            FadeIO.fadeIo.FadeOut(40);
            BGMMaster.AudioFadeOutStart(40);
        }
    }


    IEnumerator SceneWait(int wait)
    {
        while (wait-- > 0)
            yield return null;
        load.async.allowSceneActivation = true;

    }

    void pushAnyButtonSetDraw()
    {
        pushAnyButton.DrawStart();
    }
}


