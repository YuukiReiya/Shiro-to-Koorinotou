using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェードインアウトの制御
/// 製作者：冨田 大悟
/// </summary>
public class FadeIO : MonoBehaviour {

    public static FadeIO fadeIo;

    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private Sprite Black;
    [SerializeField]
    private Sprite White;
    public bool fadeNow = false;

    public const int black = 0;
    public const int white = 1;

    private void Awake()
    {
        fadeIo = this;
    }

    public void SetFadeImage(int color)
    {
        if (color == black)
        {
            fadeImage.sprite = Black;
        }
        else if (color == white)
        {
            fadeImage.sprite = White;
        }
    }

    public void FadeOut(int time)
    {
        StartCoroutine(FadeOutCor(time));
    }
    public  void FadeIn(int time)
    {
        StartCoroutine(FadeInCor(time));
    }

    IEnumerator FadeOutCor(int time)
    {
        fadeNow = true;

        fadeImage.color = new Color(1,1,1,0);
        float speed = 1f / time;
        while (fadeImage.color.a < 1)
        {
            fadeImage.color = new Color(1, 1, 1, fadeImage.color.a + speed);
            yield return null;
        }
        fadeNow = false;

    }
    IEnumerator FadeInCor(int time)
    {
        fadeNow = true;
        fadeImage.color = new Color(1, 1, 1, 1);
        float speed = 1f / time;
        while (fadeImage.color.a > 0)
        {
            fadeImage.color = new Color(1, 1, 1, fadeImage.color.a - speed);
            yield return null;
        }
        fadeNow = false;
    }
}
