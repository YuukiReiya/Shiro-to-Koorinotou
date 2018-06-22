using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームのReady表記
/// 製作者：冨田 大悟
/// </summary>
public class ReadyUI : MonoBehaviour {

    public static ReadyUI readyUI;

    [SerializeField]
    int readyTime = 120;
    int readyCount=0;
    int readyNam=0;

    [SerializeField]
    Image[] readyUIImage = new Image[4];

    [SerializeField]
    AudioClip pi;
    bool goSoundOne =false;

    private void Start()
    {
        readyUI = this;
    }
    public void ReadyUIUpdate()
    {
        readyCount++;
        readyNam = readyCount/ (readyTime / readyUIImage.Length);

        for(int i = 0;i < readyUIImage.Length; i++)
        {
            if(readyNam == i)
            {
                readyUIImage[i].gameObject.SetActive(true);
            }
            else
            {
                readyUIImage[i].gameObject.SetActive(false);
            }
            
        }
        if (readyNam == readyUIImage.Length - 1 && !goSoundOne)
        {
            goSoundOne = true;
            PlayGoSound();
        }
    }

    public bool IsReadyTimeEnd()
    {
        return readyCount >= readyTime;
    }

    public void PlayGoSound()
    {
        SEMaster.Play(pi);
    }
}
