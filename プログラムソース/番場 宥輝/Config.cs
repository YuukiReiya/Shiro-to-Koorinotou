using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// コンフィグの処理を行うクラス
/// 制作者:番場宥輝
/// </summary>
public class Config : MonoBehaviour {

    private ChangeScale wConfig;                        //コンフィグのウィンドウ
    private int menu = Menu.CLOSE;                      //現在選択しているメニュー
    private int adjust = SELECT_MENU;                   //設定の高さ
    [SerializeField]
    private TitleBackShadows backShadow;                //バックで表示するシャドウ
    [SerializeField]
    private TitleConfigUIOutline menuUIol;              //コンフィグメニューUIアウトライン
    [SerializeField]
    private ConfigString configString;                  //コンフィグメニューの文字列
    [SerializeField]
    private TitleConfigStringOutline configStringOl;    //コンフィグメニューの文字列アウトライン
    [SerializeField]
    private VolumeUI BGM;                               //BGMのサウンドUI
    [SerializeField]
    private VolumeCursolUI BGMcur;                      //BGMのサウンドの横矢印
    private int BGMvol;                                 //BGMの音量
    [SerializeField]
    private VolumeUI SE;                                //SEのサウンドUI
    [SerializeField]
    private VolumeCursolUI SEcur;                       //SEのサウンドの横矢印
    private int SEvol;                                  //SEの音量
    [SerializeField]
    private KeyUI jumpUI;                               //ジャンプボタンのUI
    [SerializeField]
    private KeyCursolUI jumpCur;                        //ジャンプボタン横の矢印
    private int jump;                                   //ジャンプのキー
    [SerializeField]
    private KeyUI magicUI;                              //魔法ボタンのUI
    [SerializeField]
    private KeyCursolUI magicCur;                       //魔法ボタン横のUI
    private int magic;                                  //魔法のキー
    [SerializeField]
    private Data data;                                  //データ

    private const int ResetFrame = 15;                  //ジョイスティック入力のリセットを行うフレーム

    //定数宣言
    const int SELECT_MENU = 0;                          //深度0のメニュー
    const int SELECT_SETTING1 = 1;                      //深度1のメニュー
    const int SELECT_SETTING2 = 2;                      //深度2のメニュー

    void Start()
    {
        wConfig = GetComponent<ChangeScale>();
        //メニュー初期化
        ResetMenu();
        BGMvol = SaveData.GetInt("BGMvolume");
        SEvol = SaveData.GetInt("SEvolume");
        jump = SaveData.GetInt("JumpKey");
        magic = SaveData.GetInt("MagicKey");
    }
	/// <summary>
    /// コンフィグの処理を行うアップデート関数
    /// </summary>
    public void ConfigUpdate()
    {
        //コンフィグを表示する
        if (!wConfig.isMaxScale && !wConfig.isChanging && KeyLoader.keyLoader.A) 
        {
            ResetMenu();
            wConfig.StartExpansion();
            StartCoroutine(backShadow.StartFadeIn());
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));
        }
        //コンフィグが表示中
        if(wConfig.isMaxScale&&!wConfig.isChanging)
        {
            //コンフィグのメニューUIのアウトライン
            configString.SetString(menu);
            configStringOl.SetOutline(menu, adjust);
            SetSound();
            SetKey();

            //選択メニュー変更
            if (adjust == SELECT_MENU)
            {
                if(!menuUIol.gameObject.activeSelf)
                {
                    menuUIol.gameObject.SetActive(true);
                }
                menuUIol.SetImage(menu);
                SelectMenu();
            }
            //選択設定の位置変更
            else
            {
                if(menuUIol.gameObject.activeSelf)
                {
                    menuUIol.gameObject.SetActive(false);
                }
                Setting();
            }

            //メニューを閉じる
            if (!data.Message.isChanging && !data.Message.isMaxScale &&
                KeyLoader.keyLoader.B) 
            {
                wConfig.StartNarrow();
                StartCoroutine(backShadow.StartFadeOut());
                SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Cancel));
            }

        }
    }
    /// <summary>
    /// コンフィグの処理が終了しているか判定する関数
    /// </summary>
    /// <returns>true:終了,false:継続</returns>
	public bool isConfigEnd()
    {
        if (!wConfig.isMaxScale && !wConfig.isChanging)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 各メニューの処理をまとめた関数
    /// </summary>
    void Setting()
    {
        if (menu == Menu.CLOSE)
        {
            Close();
        }
        else if (menu == Menu.SOUND)
        {
            SelectSoundVol();
        }
        else if (menu == Menu.BUTTON)
        {
            SelectKey();
        }
        else if (menu == Menu.DATA)
        {
            Delete();
        }
    }
    /// <summary>
    /// メニューの選択を行う関数
    /// </summary>
    void SelectMenu()
    {
        //左
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x < 0)
        {
            menu--;
            if (menu < Menu.CLOSE)
            {
                menu = Menu.DATA;
            }
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
        //右
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x > 0) 
        {
            menu++;
            if (menu > Menu.DATA)
            {
                menu = Menu.CLOSE;
            }
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
        //下
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y < 0)
        {
            adjust = SELECT_SETTING1;
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
        //上
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y > 0)
        {
            if (menu == Menu.CLOSE || menu == Menu.DATA)
            {
                adjust = SELECT_SETTING1;
            }
            else
            {
                adjust = SELECT_SETTING2;
            }
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
    }
    /// <summary>
    /// "閉じる"処理を行う関数
    /// </summary>
    void Close()
    {
        if(KeyLoader.keyLoader.A)
        {
            wConfig.StartNarrow();
            StartCoroutine(backShadow.StartFadeOut());
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Cancel));
        }
        if (!KeyLoader.keyLoader.StickWait &&
            (KeyLoader.keyLoader.joyStickAxis.y > 0 || KeyLoader.keyLoader.joyStickAxis.y < 0)) 
        {
            adjust = SELECT_MENU;
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
    }
    /// <summary>
    /// "サウンド"処理を行う関数
    /// </summary>
    void SelectSoundVol()
    {
        //BGM
        if (adjust == SELECT_SETTING1)
        {
            SettingBGMvol();
        }
        //SE
        else
        {
            SettingSEvol();
        }
        //深度の変更
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y > 0)
        {
            if (adjust == SELECT_SETTING1)
            {
                adjust = SELECT_MENU;
            }
            else
            {
                adjust = SELECT_SETTING1;
            }
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y < 0)
        {
            if (adjust == SELECT_SETTING1)
            {
                adjust = SELECT_SETTING2;
            }
            else
            {
                adjust = SELECT_MENU;
            }
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
    }
    /// <summary>
    /// BGMの音量変更を行う関数
    /// </summary>
    void SettingBGMvol()
    {
        BGMcur.Draw(BGMvol);
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x > 0 
            && BGMvol < Menu.MAX_VOL)
        {
            BGMvol++;
            SaveData.SaveInt("BGMvolume", BGMvol);
            BGMvol = SaveData.GetInt("BGMvolume");
            BGMMaster.ReLoad();
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x < 0
            && BGMvol > Menu.MIN_VOL) 
        {
            BGMvol--;
            SaveData.SaveInt("BGMvolume", BGMvol);
            BGMvol = SaveData.GetInt("BGMvolume");
            BGMMaster.ReLoad();
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
    }
    /// <summary>
    /// SEの音量変更を行う関数
    /// </summary>
    void SettingSEvol()
    {
        SEcur.Draw(SEvol);
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x > 0
            && SEvol < Menu.MAX_VOL)
        {
            SEvol++;
            SaveData.SaveInt("SEvolume", SEvol);
            SEvol = SaveData.GetInt("SEvolume");
            SEMaster.ReLoad();
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x < 0
            && SEvol > Menu.MIN_VOL) 
        {
            SEvol--;
            SaveData.SaveInt("SEvolume", SEvol);
            SEvol = SaveData.GetInt("SEvolume");
            SEMaster.ReLoad();
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
    }
    /// <summary>
    /// "ボタン"処理を行う関数
    /// </summary>
    void SelectKey()
    {
        //カーソル
        jumpCur.SetCursol(adjust == SELECT_SETTING1);
        magicCur.SetCursol(adjust == SELECT_SETTING2);
        //ジャンプ
        if(adjust==SELECT_SETTING1)
        {
            SettingJump();
        }
        //魔法
        else
        {
            SettingMagic();
        }
        //深度変更
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y > 0)
        {
            if (adjust == SELECT_SETTING1)
            {
                adjust = SELECT_MENU;
            }
            else
            {
                adjust = SELECT_SETTING1;
            }
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y < 0)
        {
            if (adjust == SELECT_SETTING1)
            {
                adjust = SELECT_SETTING2;
            }
            else
            {
                adjust = SELECT_MENU;
            }
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
        }
    }
    /// <summary>
    /// ジャンプボタンを変更を行う関数
    /// </summary>
    void SettingJump()
    {
        //左
        if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x < 0)
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
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
            //カーソル移動音
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
        }
        //右
        else if (!KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x > 0)
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
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
            //カーソル移動音
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
        }
        //変更した値をセーブする
        SaveData.SaveInt("JumpKey", jump);
    }
    /// <summary>
    /// 魔法ボタンを変更を行う関数
    /// </summary>
    void SettingMagic()
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
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
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
            KeyLoader.keyLoader.SetStickWait(ResetFrame);
            //カーソル移動音
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
        }
        //変更した値をセーブする
        SaveData.SaveInt("MagicKey", magic);

    }
    /// <summary>
    /// "データ"処理を行う関数
    /// </summary>
    void Delete()
    {
        if (!data.DeleteUpdate())
        {
            if (!KeyLoader.keyLoader.StickWait &&
                (KeyLoader.keyLoader.joyStickAxis.y < 0 || KeyLoader.keyLoader.joyStickAxis.y > 0))
            {
                adjust = SELECT_MENU;
                SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
                KeyLoader.keyLoader.SetStickWait(ResetFrame);
            }
        }
    }
    /// <summary>
    /// サウンドの描画を行う関数
    /// </summary>
    void SetSound()
    {
        BGM.gameObject.SetActive(menu == Menu.SOUND);
        BGMcur.Set(adjust == SELECT_SETTING1);
        if (BGM.gameObject.activeSelf)
        {
            BGM.Draw(BGMvol);
        }
        SE.gameObject.SetActive(menu == Menu.SOUND);
        SEcur.Set(adjust == SELECT_SETTING2);
        if(SE.gameObject.activeSelf)
        {
            SE.Draw(SEvol);
        }
    }
    /// <summary>
    /// ボタンの描画を行う関数
    /// </summary>
    void SetKey()
    {
        jumpUI.gameObject.SetActive(menu == Menu.BUTTON);
        magicUI.gameObject.SetActive(menu == Menu.BUTTON);
        magicCur.SetCursol(menu == Menu.BUTTON && adjust != SELECT_MENU);
        jumpCur.SetCursol(menu == Menu.BUTTON && adjust != SELECT_MENU);
        jumpUI.Draw(jump);
        magicUI.Draw(magic);
    }
    /// <summary>
    /// メニューが閉じられたときに行う初期化関数
    /// </summary>
    void ResetMenu()
    {
        menu = Menu.CLOSE;
        adjust = SELECT_MENU;
        menuUIol.SetImage(Menu.CLOSE);
        configString.SetString(Menu.CLOSE);
        configStringOl.SetOutline(Menu.CLOSE, SELECT_MENU);
        BGM.gameObject.SetActive(false);
        SE.gameObject.SetActive(false);
        BGMcur.Set(false);
        SEcur.Set(false);
        jumpUI.gameObject.SetActive(false);
        magicUI.gameObject.SetActive(false);
        jumpCur.SetCursol(false);
        magicCur.SetCursol(false);
    }
}
