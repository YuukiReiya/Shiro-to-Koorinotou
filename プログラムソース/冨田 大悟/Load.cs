using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// シーンのロードの制御
/// 製作者：冨田 大悟
/// </summary>
public class Load : MonoBehaviour {

    static string sceneName;
    public static bool loadNow = false;

    public AsyncOperation async {
        get;
        private set;
    }

    public IEnumerator LoadScene(bool autoNext = true,int endTime = 1)
    {       
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;    // シーン遷移をしない

        Debug.Log("LoadScene now");

        while (async.progress < 0.9f)
        {
            Debug.Log(async.progress);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Scene Loaded");
        yield return new WaitForSeconds(endTime);

        async.allowSceneActivation = autoNext;    // シーン遷移許可

    }
    public static void SetNextSceneName(string name)
    {
        sceneName = name;
    }
    public static string GetNextSceneName()
    {
        return sceneName;
    }

    public static IEnumerator WaitLoadScene(string name, int waitTime = 1)
    {
        loadNow = true;
        while (waitTime-- > 0)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadSceneAsync(name);
        loadNow = false;

    }

    public IEnumerator WaitLoadSceneAndLoad(string name, int waitTime = 1)
    {
        async = SceneManager.LoadSceneAsync(name);
        async.allowSceneActivation = false;    // シーン遷移をしない


        loadNow = true;
        while (waitTime-- > 0)
        {
            yield return new WaitForEndOfFrame();
        }

        async.allowSceneActivation = true;

        loadNow = false;
    }
}
