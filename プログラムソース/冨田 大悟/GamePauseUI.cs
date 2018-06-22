using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// ポーズ画面の操作
/// 製作者：冨田 大悟
/// </summary>
public class GamePauseUI : MonoBehaviour {

    public static GamePauseUI gamePauseUI;

    //ポーズ中のためFIXEDは無理

    [SerializeField]
    Image[] me;
    [SerializeField]
    Image[] line;

    [SerializeField]
    GameObject[] back;

    [SerializeField]
    int stMeNam;
    int meNam;

    public enum PauseEndPattern
    {
        Back,Retry,StageSelect,Title
    }
    public PauseEndPattern pauseEndPattern {
        get;
        private set;
    }


    [SerializeField]
    int keyWait;

    [SerializeField]
    Color selectColor;
    [SerializeField]
    Color notSelectColor;

    private void Start()
    {
        gameObject.SetActive(false);
        gamePauseUI = this;
    }

    public void GamePauseUIStart()
    {
        meNam = stMeNam %me.Length;
        KeyLoader.keyLoader.SetKeyWait(keyWait);
        KeyLoader.keyLoader.SetStickWait(keyWait);

        ImageSet();
        gameObject.SetActive(true);

    }
    public bool GamePauseUIUpdate()
    {
        bool end = false;


        keyMoves();

        if (KeyLoader.keyLoader.A)
        {

            if (meNam != 2)
            {
                end = true;
                SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));
                switch (meNam)
                {
                    case 0:
                        pauseEndPattern = PauseEndPattern.Back;
                        break;

                    case 1:
                        pauseEndPattern = PauseEndPattern.Retry;

                        break;

                    case 3:
                        pauseEndPattern = PauseEndPattern.StageSelect;

                        break;

                    case 4:
                        pauseEndPattern = PauseEndPattern.Title;

                        break;
                }
            }
            
        }
        else if (KeyLoader.keyLoader.B || KeyLoader.keyLoader.StartKey)
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Cancel));

            end = true;
            pauseEndPattern = PauseEndPattern.Back;
        }

        if (end)
        {
            gameObject.SetActive(false);
        }

        return end;

    }

    void keyMoves() {
        if (KeyLoader.keyLoader.joyStickAxis.x > 0 && !KeyLoader.keyLoader.StickWait)
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));

            meNam += 1;
            meNam %= me.Length;
            KeyLoader.keyLoader.SetStickWait(keyWait);
            ImageSet();

        }
        else if (KeyLoader.keyLoader.joyStickAxis.x < 0 && !KeyLoader.keyLoader.StickWait)
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));

            meNam += me.Length - 1;
            meNam %= me.Length;
            KeyLoader.keyLoader.SetStickWait(keyWait);
            ImageSet();
        }
        else if (KeyLoader.keyLoader.joyStickAxis.x == 0)
        {
            KeyLoader.keyLoader.SetStickWait(0);

        }
    }

    void ImageSet()
    {
        for(int i = 0; i < me.Length;i++)
        {
            if(i == meNam)
            {
                back[i].gameObject.SetActive(true);
                me[i].color = selectColor;
                line[i].gameObject.SetActive(true);
            }
            else
            {
                back[i].gameObject.SetActive(false);
                me[i].color = notSelectColor ;
                line[i].gameObject.SetActive(false);

            }
            me[i].gameObject.SetActive(true);

        }

    }

    private void OnValidate()
    {
        if(stMeNam < 0 )stMeNam = 0;
        meNam = stMeNam % me.Length;
        ImageSet();
    }
}
