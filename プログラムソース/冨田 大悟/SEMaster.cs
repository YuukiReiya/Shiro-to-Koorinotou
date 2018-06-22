using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SEの制御
/// 製作者：冨田 大悟
/// </summary>
public class SEMaster : MonoBehaviour
{
    static AudioSource audioSource;

    static SEMaster seMaster;

    public static bool fadeNow
    {
        get;
        private set;
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        seMaster = this;
    }

    protected void Start()
    {

        audioSource.volume = SaveData.GetInt("SEvolume") *(1f /10f);
        
    }

    public static void Play(AudioClip ac)
    {
        audioSource.PlayOneShot(ac);
    }

    public static void ReLoad()
    {
        float nam =  1f / 10f;
        audioSource.volume = SaveData.GetInt("SEvolume")*nam;
    }

    public  void AudioFadeOut(int time, int maxTime)
    {
        float nam = 1f / 10f;
        audioSource.volume = SaveData.GetInt("SEvolume") * nam * ((float)(maxTime - time) / maxTime);
    }

    public  void AudioFadeIn(int time, int maxTime)
    {
        float nam = 1f / 10f;
        audioSource.volume = SaveData.GetInt("SEvolume") * nam * ((float)time / maxTime);

    }

    IEnumerator AudioFadeOutStartCol(int _fadeTime)
    {
        fadeNow = true;
        for (int i = 0; i < _fadeTime+1; i++)
        {
            AudioFadeOut(i, _fadeTime);
            yield return new WaitForFixedUpdate();
        }
        fadeNow = false;

    }
    IEnumerator AudioFadeInStartCol(int _fadeTime)
    {
        fadeNow = true;

        for (int i = 0; i < _fadeTime+1; i++)
        {

            AudioFadeIn(i, _fadeTime);
            yield return new WaitForFixedUpdate();
        }
        fadeNow = false;

    }

    public static void AudioFadeOutStart(int time)
    {
        seMaster.StartCoroutine(seMaster.AudioFadeOutStartCol(time));
    }

    public static void AudioFadeInStart(int time)
    {
        seMaster.StartCoroutine(seMaster.AudioFadeInStartCol(time));
    }
}
