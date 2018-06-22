using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージの経過タイムの計測
/// 製作者：冨田 大悟
/// </summary>
public class TimeCounter: MonoBehaviour {

    public static TimeCounter timeCounter;

    public float time
    {
        get;
        private set;
    }

    bool timeCount;

    [SerializeField]
    Image[] namber = new Image[4];
    [SerializeField]
    Sprite[] namberSprite = new Sprite[9];


    [SerializeField]
    Image newRecord;
    [SerializeField]
    Sprite[] newRecordSprite;

    [SerializeField]
    float newRecordFadeSpeed;
    [SerializeField]
    float newRecordSizeSpeed;
    [SerializeField]
    float newRecordSize;
    [SerializeField]
    Vector3 newRecordBaseSize;


    private void Awake()
    {
        timeCounter = this;
    }

    private void Start()
    {
        SetImage();
        newRecord.gameObject.SetActive(false);

    }

    private void SetImage()
    {
        namber[0].sprite = namberSprite[((int)time /10)];
        namber[1].sprite = namberSprite[((int)time %10)];
        namber[2].sprite = namberSprite[((int)(time*10) % 10)];
        namber[3].sprite = namberSprite[((int)(time * 100) % 10)];

    }

    public void TimeCountStart()
    {
        timeCount = true;
        StartCoroutine(TimeCount());
    }

    public void TimeCountStop()
    {
        timeCount = false;
        StopCoroutine(TimeCount());
    }

    IEnumerator TimeCount()
    {
        while (timeCount)
        {
            time += Time.fixedDeltaTime;
            if (time > 99.99f) time = 99.99f;
            SetImage();
            yield return  new WaitForFixedUpdate();
        }
    }
    
    /// <summary>
    /// クリアタイムのセーブ
    /// </summary>
    public void SaveClearTime()
    {
        //元データ取り出し
        float bestTime = SaveData.GetFloat("Stage" + SceneNames.GetStageNam(SceneManager.GetActiveScene().name)+"-"+ SceneNames.GetFloorNam(SceneManager.GetActiveScene().name) + "ClearTime");
        //比較
        if(bestTime > time)
        {
            //セーブ
            SaveData.SaveFloat("Stage" + SceneNames.GetStageNam(SceneManager.GetActiveScene().name) + "-" + SceneNames.GetFloorNam(SceneManager.GetActiveScene().name) + "ClearTime", time);
        }
    }

    public void DrawNewRecord()
    {
        float bestTime = SaveData.GetFloat("Stage" + SceneNames.GetStageNam(SceneManager.GetActiveScene().name) + "-" + SceneNames.GetFloorNam(SceneManager.GetActiveScene().name) + "ClearTime");

        //比較
        if (bestTime > time)
        {
            newRecord.gameObject.SetActive(true);
           StartCoroutine(NewRecordFade());
            
        }
    }

    IEnumerator NewRecordFade()
    {
        int i = 0;
        int mo = 1;

        float sinNam=0;
        while (true)
        {
            if (newRecord.color.a > 1)
            {
                newRecord.color = new Color(newRecord.color.r, newRecord.color.g, newRecord.color.b, 1);
            }
            else
            {
                newRecord.color += new Color(0, 0, 0, newRecordFadeSpeed);

            }

            i += mo;
            if (i == 4* newRecordSprite.Length-1)
            {
                mo = -1;
            }
            else
            if (i == 0)
            {
                mo = 1;
            }
            newRecord.sprite = newRecordSprite[i /4];
            sinNam += newRecordSizeSpeed;
            newRecord.rectTransform.localScale = newRecordBaseSize+Vector3.one * (Mathf.Sin(sinNam)*newRecordSize);
            yield return new WaitForFixedUpdate();

        }

    }
}
