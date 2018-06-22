using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// タイトルの処理を行うクラス
/// 制作者:番場 宥輝
/// </summary>
public class Title : MonoBehaviour {

    [SerializeField]
    private GameObject gui;                  // 画面左下のUI
    [SerializeField]
    private FadeUI PushButton;               // "なにかボタンを押してください"
    private bool fadePushButton = true;      //ボタンを押してください非表示
    int menu = Menu.START;                   // 現在のメニューカーソルの位置情報
    [SerializeField]
    private TitleBackShadows backShadows;    //ウィンドウ表示中の画面暗くするやつ
    [SerializeField]
    MenuUI menuUI;                           // 表示するメニューのUI
    [SerializeField]
    private Config config;                   // コンフィグ
    [SerializeField]
    private Credit credit;                   // クレジット
    private bool menuSelect = true;          //メニュー選択可能状態
    const int ResetFrame = 15;               //キーのリセット
    private int backcnt = 0;                 // オープニングシーンに戻るために加算するカウンタ 
    [SerializeField]
    private  int BACK_OP = 5700;             // オープニングに戻るカウンタ値
    private bool ReturnOp = false;           // オープニングに遷移するフラグ　true:可能状態,false:不可能状態
    public static bool fadeIsBlack;          //false:白,true:黒
    delegate void routineFunc();             // コルーチンに渡す関数型の宣言※使用はラムダ式を用いる
    routineFunc runningFunc = null;          //関数の初期化

    void Start () {
        Application.targetFrameRate = 60;

        gui.SetActive(false);
        menuUI.gameObject.SetActive(false);
        
        //オープニングの遷移仕方によりフェードの色を変える
        if (fadeIsBlack)
        {
            //黒で遷移されたら黒に設定し
            FadeIO.fadeIo.SetFadeImage(FadeIO.black);
        }
        else
        {
            //白で遷移されたら白に設定する
            FadeIO.fadeIo.SetFadeImage(FadeIO.white);
        }
        //フェード処理
        KeyLoader.keyLoader.SetKeyWait(Menu.FADE);
        FadeIO.fadeIo.FadeIn(Menu.FADE);
        BGMMaster.AudioFadeInStart(Menu.FADE);
        SEMaster.AudioFadeInStart(Menu.FADE);
    }

    void Update()
    {
        if (fadePushButton)
        {
            PushButton.Draw();
            //ボタンを押してメニュー表示
            if (!ReturnOp && KeyLoader.keyLoader.IsAnyKey()) 
            {
                backcnt = 0;
                fadePushButton = false;
                gui.SetActive(true);
                menuUI.gameObject.SetActive(true);
                //再度表示する時のために初期化
                PushButton.Reset();
                PushButton.gameObject.SetActive(false);
                //決定音
                SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));
            }
            else
            {
                ReturnOpeningScene();
            }
        }
        else
        {
            menuUI.Draw(menu);
            if (menuSelect && KeyLoader.keyLoader.joyStickAxis.x > 0 && !KeyLoader.keyLoader.StickWait)
            {
                menu++;
                if (menu > Menu.END)
                {
                    menu = Menu.START;
                }
                SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
                KeyLoader.keyLoader.SetStickWait(ResetFrame);
            }
            else if (menuSelect && KeyLoader.keyLoader.joyStickAxis.x < 0 && !KeyLoader.keyLoader.StickWait)
            {
                menu--;
                if (menu < Menu.START) 
                {
                    menu = Menu.END;
                }
                SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
                KeyLoader.keyLoader.SetStickWait(ResetFrame);
            }
            if (menuSelect && KeyLoader.keyLoader.B) 
            {
                SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Cancel));
                gui.SetActive(false);
                fadePushButton = true;
                PushButton.gameObject.SetActive(true);
                menuUI.gameObject.SetActive(false);
            }

            switch (menu)
            {
                case Menu.START:
                    GameStart();
                    break;
                case Menu.CONFIG:
                    GameConfig();
                    break;
                case Menu.CREDIT:
                    GameCredit();
                    break;
                case Menu.END:
                    GameEnd();
                    break;

            }
        }
    }

    /// <summary>
    /// ゲーム開始
    /// </summary>
    void GameStart()
    {
        if (KeyLoader.keyLoader.A) 
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));
            menuSelect = false;
            KeyLoader.keyLoader.SetKeyWait(Menu.FADE);
            StageSelect.stage = Menu.MONSTER;
            SaveData.AllSave();
            FadeIO.fadeIo.SetFadeImage(FadeIO.black);
            FadeIO.fadeIo.FadeOut(Menu.FADE);
            BGMMaster.AudioFadeOutStart(Menu.FADE);
            SEMaster.AudioFadeOutStart(Menu.FADE);
            runningFunc = () => LoadNextScene();
            StartCoroutine(RunCoroutine(Menu.FADE, runningFunc));
        }
    }
    /// <summary>
    /// コンフィグ
    /// </summary>
    void GameConfig()
    {
        config.ConfigUpdate();
        menuSelect = config.isConfigEnd();
    }
    /// <summary>
    /// クレジット
    /// </summary>
    void GameCredit()
    {
        credit.CreditUpdate();
        menuSelect = credit.isCerditEnd();
    }
    /// <summary>
    /// ゲーム終了
    /// </summary>
    void GameEnd()
    {
        if (KeyLoader.keyLoader.A)
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Cancel));
            menuSelect = false;
            KeyLoader.keyLoader.SetKeyWait(Menu.FADE);
            SaveData.AllSave();
            FadeIO.fadeIo.SetFadeImage(FadeIO.black);
            FadeIO.fadeIo.FadeOut(Menu.FADE);
            BGMMaster.AudioFadeOutStart(Menu.FADE);
            SEMaster.AudioFadeOutStart(Menu.FADE);
            runningFunc = () => Shutdown();
            StartCoroutine(RunCoroutine(Menu.FADE, runningFunc));
        }
    }
    /// <summary>
    /// 指定のフレーム後に指定の関数を行うコルーチン
    /// </summary>
    /// <param name="frame">指定のフレーム</param>
    /// <param name="func">指定の関数</param>
    /// <returns></returns>
    private IEnumerator RunCoroutine(int frame,routineFunc func)
    {
        for (int i = 0; i < frame; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        func();
    }
    /// <summary>
    /// アプリケーションの終了を行う関数
    /// </summary>
    private void Shutdown()
    {
        Application.Quit();
    }
    /// <summary>
    /// ロードシーンをはさみ、ステージセレクトに画面を遷移する
    /// </summary>
    private void LoadNextScene()
    {
        Load.SetNextSceneName(SceneNames.StageSelectName);
        SceneManager.LoadScene(SceneNames.LoadName);
    }
    /// <summary>
    /// タイトルシーンに遷移する関数
    /// </summary>
    private void ReturnBackScene()
    {
        SceneManager.LoadScene(SceneNames.OpeningName);
    }
    /// <summary>
    /// タイトルシーンに遷移するためにカウンタを加算する関数
    /// </summary>
    private void ReturnOpeningScene()
    {
        backcnt++;
        if (backcnt == BACK_OP)
        {
            FadeIO.fadeIo.SetFadeImage(FadeIO.white);
            FadeIO.fadeIo.FadeOut(Menu.FADE);
            BGMMaster.AudioFadeOutStart(Menu.FADE);
            SEMaster.AudioFadeOutStart(Menu.FADE);
            runningFunc = () => ReturnBackScene();
            StartCoroutine(RunCoroutine(Menu.FADE, runningFunc));
        }
    }
}
