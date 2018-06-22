using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMを制御するクラス
/// 製作者：冨田 大悟
/// </summary>
public class BGMMaster : MonoBehaviour
{

    static AudioSource audioSource;
    static BGMMaster bgmMaster;
    static float restartTime =0;

    public static bool fadeNow
    {
        get;
        private set;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        bgmMaster = this;
    }
    protected void Start()
    {

        audioSource.volume = SaveData.GetInt("BGMvolume") * (1f / 10f);
        restartTime = 0;
    }

    /// <summary>
    /// BGMの再生
    /// </summary>
    public static void Play()
    {
         audioSource.Play();
        audioSource.time = restartTime;
    } 

    /// <summary>
    /// BGMの停止
    /// </summary>
    public static void Stop()
    {
        Debug.Log("bgm stop");
        restartTime = audioSource.time;
        audioSource.Stop();
    }

    /// <summary>
    /// ボリュームの再更新
    /// </summary>
    public static void ReLoad()
    {
        float nam = 1f / 10f;
        audioSource.volume = SaveData.GetInt("BGMvolume") * nam;
    }

    /// <summary>
    /// BGMの変更
    /// </summary>
    /// <param name="ac">変える音楽</param>
    public static void CangeBGM(AudioClip ac)
    {
        audioSource.clip = ac;
        audioSource.Play();
    }

    /// <summary>
    /// BGMの音量を割合で小さくする
    /// </summary>
    /// <param name="time">現在の値</param>
    /// <param name="maxTime">最大の値</param>
    public void AudioFadeOut(int time, int maxTime)
    {
        float nam = 1f / 10f;
        audioSource.volume = SaveData.GetInt("BGMvolume") * nam * ((float)(maxTime - time) / maxTime);
    }

    /// <summary>
    /// BGMの音量を割合で大きくする
    /// </summary>
    /// <param name="time">現在の値</param>
    /// <param name="maxTime">最大の値</param>
    public void AudioFadeIn(int time, int maxTime)
    {
        float nam = 1f / 10f;
        audioSource.volume = SaveData.GetInt("BGMvolume") * nam * ((float)time / maxTime);

    }

    /// <summary>
    /// 音を小さくするコルーチン
    /// </summary>
    /// <param name="_fadeTime">時間</param>
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
    /// <summary>
    /// 音を大きくするコルーチン
    /// </summary>
    /// <param name="_fadeTime">時間</param>
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

    public static void AudioFadeOutStart( int time)
    {
        bgmMaster.StartCoroutine(bgmMaster.AudioFadeOutStartCol(time));
    }

    public static void AudioFadeInStart(int time)
    {
        bgmMaster.StartCoroutine(bgmMaster.AudioFadeInStartCol(time));

    }
}
