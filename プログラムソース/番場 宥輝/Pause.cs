using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージセレクトのポーズ処理を行うクラス
/// 制作者:番場 宥輝
/// </summary>
public class Pause : MonoBehaviour
{

    private ChangeScale wPause;                 //ポーズウィンドウ
    private int menu = Menu.CLOSE;              //現在選択中のポーズメニュー
    private int setting = SETTING_MENU;         //ポーズメニューの深度
    private int BGMvol;                         //BGM音量
    private int SEvol;                          //SE音量
    private int jump;                           //ジャンプのキー
    private int magic;                          //魔法のキー
    [SerializeField]
    private StageSelectBackShadows blackShadow; //ポーズ表示時のバックにかかるシャドウ
    [SerializeField]
    StageSelectPauseUIOutline pUIoutline;       //ポーズ画面UIアウトライン
    [SerializeField]
    PauseString pString;                        //ポーズ画面文字
    [SerializeField]
    StageSelectPauseStringOutline pStrOutline;  //ポーズ画面文字アウトライン
    [SerializeField]
    VolumeSprite bgmSprite;                     //BGMのVOLUME
    [SerializeField]
    VolumeCursolSprite bgmCS;                   //BGMのカーソル
    [SerializeField]
    VolumeSprite seSprite;                      //SEのVOLUME
    [SerializeField]
    VolumeCursolSprite seCS;                    //SEのカーソル
    [SerializeField]
    KeySprite jumpSprite;                       //ジャンプボタンのスプライト
    [SerializeField]
    KeyCursolSprite jumpCS;                     //ジャンプボタンのカーソル
    [SerializeField]
    KeySprite magicSprite;                      //魔法ボタンのスプライト
    [SerializeField]
    KeyCursolSprite magicCS;                    //魔法ボタンのカーソル
    //外部参照用プロパティ
    public bool isDisplaying { get { return wPause.isMaxScale; } }             //true:表示中,false:非表示
    public bool isChanging { get { return wPause.isChanging; }}                //true:操作可能,false:操作不可
    //定数宣言
    private const int RESET_KEY = 15;           //ジョイスティック入力のリセットフレーム
    private const int SETTING_MENU = 0;         //メニュー変更(深度0)
    private const int SETTING_1 = 1;            //設定変更(深度1)
    private const int SETTING_2 = 2;            //設定変更(深度2)
    void Start()
    {
        wPause = GetComponent<ChangeScale>();
        BGMvol = SaveData.GetInt("BGMvolume");
        SEvol = SaveData.GetInt("SEvolume");
        jump = SaveData.GetInt("JumpKey");
        magic = SaveData.GetInt("MagicKey");
        Reset();
    }

    /// <summary>
    /// ポーズの処理を行う関数
    /// </summary>
	public void PauseUpdate()
    {
        bgmSprite.Set(menu == Menu.SOUND);
        bgmCS.Set(menu == Menu.SOUND && setting == SETTING_1);
        seSprite.Set(menu == Menu.SOUND);
        seCS.Set(menu == Menu.SOUND && setting == SETTING_2);
        jumpSprite.gameObject.SetActive(menu == Menu.BUTTON);
        jumpCS.Set(menu == Menu.BUTTON && setting == SETTING_1);
        magicSprite.gameObject.SetActive(menu == Menu.BUTTON);
        magicCS.Set(menu == Menu.BUTTON && setting == SETTING_2);
        pUIoutline.SetSprite(menu, setting);
        pString.SetString(menu);
        pStrOutline.SetOutline(menu, setting);
        //ポーズを閉じる処理
        if (isDisplaying && !isChanging &&
            ( KeyLoader.keyLoader.B|| KeyLoader.keyLoader.StartKey))
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Cancel));
            Close();
        }
        //設定深度によって行う処理を変える
        switch (setting)
        {
            //選択メニューの変更
            case SETTING_MENU:
                SelectMenu();
                break;
            case SETTING_1:
                Setting1();
                break;
            case SETTING_2:
                Setting2();
                break;
        }
    }
    /// <summar>
    /// ポーズ画面を開く関数
    /// </summary>
    public void Open()
    {
        StartCoroutine(blackShadow.StartFadeIn());
        wPause.StartExpansion();
    }
    /// <summary>
    /// ポーズ画面を閉じる関数
    /// </summary>
    private void Close()
    {
        StartCoroutine(blackShadow.StartFadeOut());
        wPause.StartNarrow();
    }
    /// <summary>
    /// メニュー変更
    /// </summary>
    private void SelectMenu()
    {
        //右
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x > 0)
        {
            if (menu == Menu.TITLE)
            {
                menu = Menu.CLOSE;
            }
            else
            {
                menu++;
            }
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
        //左
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x < 0)
        {
            if (menu == Menu.CLOSE)
            {
                menu = Menu.TITLE;
            }
            else
            {
                menu--;
            }
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
        //上
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y > 0)
        {
            //深度が1までのものは深度1に
            if (menu == Menu.CLOSE || menu == Menu.TITLE)
            {
                setting = SETTING_1;
            }
            //深度が2までのものは深度を2にする
            else
            {
                setting = SETTING_2;
            }
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
        //下
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y < 0)
        {
            setting = SETTING_1;
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
    }
    /// <summary>
    /// 深度1の設定を変更する関数を制御する
    /// </summary>
    private void Setting1()
    {
        switch (menu)
        {
            case Menu.CLOSE:
                if (KeyLoader.keyLoader.A)
                {
                    Close();
                    SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));
                }
                break;
            case Menu.SOUND:
                bgmSprite.Draw(BGMvol);
                bgmCS.Draw(BGMvol);
                SettingBGM();
                break;
            case Menu.BUTTON:
                jumpSprite.Set(jump);
                SettingJump();
                break;
            case Menu.TITLE:
                ReturnTitle();
                break;
        }
        //上
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y > 0)
        {
            setting = SETTING_MENU;
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
        //下
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y < 0)
        {
            if (menu == Menu.CLOSE || menu == Menu.TITLE)
            {
                setting = SETTING_MENU;
            }
            else
            {
                setting = SETTING_2;
            }
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
    }
    /// <summary>
    /// 深度2の設定を変更する関数を制御する
    /// </summary>
    private void Setting2()
    {
        switch (menu)
        {
            case Menu.SOUND:
                seSprite.Draw(SEvol);
                seCS.Draw(SEvol);
                SettingSE();
                break;
            case Menu.BUTTON:
                magicSprite.Set(magic);
                SettingMagic();
                break;
        }
        //上
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y > 0)
        {
            setting = SETTING_1;
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
        //下
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y < 0)
        {
            setting = SETTING_MENU;
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
    }
    /// <summary>
    /// BGMの音量を変更する関数
    /// </summary>
    private void SettingBGM()
    {
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x > 0
             && BGMvol < Menu.MAX_VOL)
        {
            BGMvol++;
            SaveData.SaveInt("BGMvolume", BGMvol);
            BGMvol = SaveData.GetInt("BGMvolume");
            BGMMaster.ReLoad();
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x < 0
            && BGMvol > Menu.MIN_VOL)
        {
            BGMvol--;
            SaveData.SaveInt("BGMvolume", BGMvol);
            BGMvol = SaveData.GetInt("BGMvolume");
            BGMMaster.ReLoad();
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
    }
    /// <summary>
    /// SEの音量を変更する関数
    /// </summary>
    private void SettingSE()
    {
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x > 0
          && SEvol < Menu.MAX_VOL)
        {
            SEvol++;
            SaveData.SaveInt("SEvolume", SEvol);
            SEvol = SaveData.GetInt("SEvolume");
            SEMaster.ReLoad();
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x < 0
            && SEvol > Menu.MIN_VOL)
        {
            SEvol--;
            SaveData.SaveInt("SEvolume", SEvol);
            SEvol = SaveData.GetInt("SEvolume");
            SEMaster.ReLoad();
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
        }
    }
    /// <summary>
    /// ジャンプのキーを変更する関数
    /// </summary>
    private void SettingJump()
    {
        //右
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x > 0)
        {
            //ジャンプのキー変更
            if (jump == Menu.B)
            {
                jump = Menu.A;
            }
            else
            {
                jump++;
            }
            //ジャンプと魔法が同じ
            if (jump == magic)
            {
                jump++;
                if (jump > Menu.B)
                {
                    jump = Menu.A;
                }
            }
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
            //カーソル移動音
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
        }
        //左
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x < 0)
        {
            //ジャンプのキー変更
            if (jump == Menu.A)
            {
                jump = Menu.B;
            }
            else
            {
                jump--;
            }
            //ジャンプと魔法が同じ
            if (jump == magic)
            {
                jump--;
                if (jump < Menu.A)
                {
                    jump = Menu.B;
                }
            }
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
            //カーソル移動音
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
        }
        //変更した値をセーブする
        SaveData.SaveInt("JumpKey", jump);
    }
    /// <summary>
    /// 魔法のキーを変更する関数
    /// </summary>
    private void SettingMagic()
    {
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x < 0)
        {
            //魔法のキー変更
            if (magic == Menu.A)
            {
                magic = Menu.B;
            }
            else
            {
                magic--;
            }
            //ジャンプと魔法が同じ
            if (jump == magic)
            {
                magic--;
                if (magic < Menu.A)
                {
                    magic = Menu.B;
                }
            }
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
            //カーソル移動音
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
        }
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x > 0)
        {
            //魔法のキー変更
            if (magic == Menu.B)
            {
                magic = Menu.A;
            }
            else
            {
                magic++;
            }
            //ジャンプと魔法が同じ
            if (jump == magic)
            {
                magic++;
                if (magic > Menu.B)
                {
                    magic = Menu.A;
                }
            }
            KeyLoader.keyLoader.SetStickWait(RESET_KEY);
            //カーソル移動音
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
        }
        //変更した値をセーブする
        SaveData.SaveInt("MagicKey", magic);
    }
    /// <summary>
    /// タイトルに戻る処理を行う関数
    /// </summary>
    private void ReturnTitle()
    {
        if (KeyLoader.keyLoader.A)
        {
            KeyLoader.keyLoader.SetStickWait(Menu.FADE);
            KeyLoader.keyLoader.SetKeyWait(Menu.FADE);
            StartCoroutine(ReturnTitleRoutine(Menu.FADE));
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));
            FadeIO.fadeIo.FadeOut(Menu.FADE);
            BGMMaster.AudioFadeOutStart(Menu.FADE);
            SEMaster.AudioFadeOutStart(Menu.FADE);
        }
    }
    /// <summary>
    /// 引数フレーム待ちタイトルにシーン遷移するコルーチン
    /// </summary>
    /// <param name="frame">処理を待つフレーム数</param>
    /// <returns></returns>
    private IEnumerator ReturnTitleRoutine(int frame)
    {
        for (int i = 0; i < frame; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        Load.SetNextSceneName(SceneNames.TitleName);
        SceneManager.LoadScene(SceneNames.LoadName);
    }
    /// <summary>
    /// 値の初期化を行う関数
    /// </summary>
    public void Reset()
    {
        menu = Menu.CLOSE;
        setting = SETTING_MENU;
        pUIoutline.SetSprite(menu,setting);
        pString.SetString(menu);
        pStrOutline.SetOutline(menu, setting);
        bgmSprite.Draw(BGMvol);
        bgmSprite.Set(false);
        bgmCS.Set(false);
        seSprite.Draw(SEvol);
        seSprite.Set(false);
        seCS.Set(false);
        jumpSprite.gameObject.SetActive(false);
        jumpCS.Set(false);
        magicSprite.gameObject.SetActive(false);
        magicCS.Set(false);
    }
}
