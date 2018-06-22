using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージセレクトの処理をするクラス
/// 制作者:番場 宥輝
/// </summary>
public class StageSelect : MonoBehaviour
{

    [SerializeField]
    private CameraRotation mainCameraObj;           //カメラObject
    public static int stage;                        //現在選択しているステージ
    private int floor = Menu.FLOOR1;                //現在選択しているフロア
    public static int[] ClearFloor                  //クリアされてるフロア
    {
        get;
        private set;
    }
    [SerializeField]
    private FloorBackSprite floorBackSprite;        //フロアの背景
    [SerializeField]
    private FloorBestTimeStringSprite BestTimeStr;  //ベストタイムの文字
    [SerializeField]
    private FloorBestTimeNumSprite BestTimeNum;     //ベストタイムの数字
    [SerializeField]
    private FloorClearSprite floorClearMarker;      //フロアのクリアマーカー
    [SerializeField]
    private FloorTopSprite floorTop;                //フロアTopの屋根
    [SerializeField]
    private FloorMoveObj floorMove;                 //フロアObjectの移動
    [SerializeField]
    private Pause pause;                            //ポーズ
    [SerializeField]
    private StageNameObj stNameObj;                 //ステージの名前、レベル、コンプマーク
    [SerializeField]
    private StageSelectCursol stCur;                //ステージカーソル
    [SerializeField]
    private FloorNameManager floorNameManager;      //フロア名制御クラス宣言
    [SerializeField]
    AudioClip stageSelectSE;

    private int mode = STAGE_SELECT;                //ステージセレクトの選択状態
    private int temp = STAGE_SELECT;                //ポーズを開く時にmodeを退避させておく退避変数
    //定数宣言
    const int RESET_STAGE_FRAME = 30;               //ステージ選択でのジョイスティック入力のリセットフレーム
    const int RESET_FLOOR_FRAME = 15;               //フロア選択でのジョイスティック入力のリセットフレーム
    const int CAMERA_ROTATION_TIME = 18;            //カメラが回転しているフレーム
    private const int STAGE_SELECT = 0;             //ステージを選ぶ
    private const int FLOOR_SELECT = 1;             //フロアを選ぶ
    private const int PAUSE = 2;                    //ポーズ
    /// <summary>
    /// コルーチンに渡す関数型の宣言
    /// </summary>
    delegate void routineFunc();
    /// <summary>
    /// コルーチンの行う関数
    /// </summary>
    routineFunc runningFunc = null;
    void Start()
    {
        Application.targetFrameRate = 60;
        KeyLoader.keyLoader.SetKeyWait(Menu.FADE);
        FadeIO.fadeIo.FadeIn(Menu.FADE);
        BGMMaster.AudioFadeInStart(Menu.FADE);
        SEMaster.AudioFadeInStart(Menu.FADE);
        ClearFloor = new int[Menu.TOWER_NUM];
        stCur.Set(true);
        StartCoroutine(stCur.SpriteUpdate());
        int bit;//ビット演算に用いるローカル変数
        for (int i = 0; i < Menu.TOWER_NUM; i++) 
        {
            bit = 1;
            for (int j = 0; j <= Menu.FLOOR_NUM; j++) 
            {
                //挑戦可能フロアを代入
                ClearFloor[i] = j;
                //ビットが立たなくなったら処理を抜ける
                if ((SaveData.GetInt("Stage" + (i + 1) + "ClearFlag") & bit) == 0)
                {
                    break;
                }
                //ビットをシフト
                bit = bit << 1;
            }
        }
    }

    void Update()
    {
        //現在の選択状況に応じた処理を行う
        switch (mode)
        {
            //ステージ選択
            case STAGE_SELECT:
                SelectStage();
                break;
            //フロア選択
            case FLOOR_SELECT:
                SelectFloor();
                break;
            //ポーズ
            case PAUSE:
                PauseMenu();
                break;
        }

    }
    /// <summary>
    /// ステージを選ぶ関数
    /// </summary>
    private void SelectStage()
    {
        
        //ステージを決定する処理
        if (!mainCameraObj.isRotation && !floorMove.isMoving
            && !KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.A)
        {
            //modeを変更し、フロア選択に処理を移る
            mode = FLOOR_SELECT;
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));
            StartCoroutine(floorMove.SlidePut());
            floorBackSprite.SetImage(); 
            BestTimeNum.SetBestTime();  
            floorTop.SetImage();        
            floor = Menu.FLOOR1;

            floorNameManager.SetName();
            stCur.Set(false);

            KeyLoader.keyLoader.SetStickWait(RESET_STAGE_FRAME);
        }
        //タイトルに戻る処理
        else if (!mainCameraObj.isRotation && !floorMove.isMoving &&
            !KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.B) 
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Cancel));
            KeyLoader.keyLoader.SetKeyWait(Menu.FADE);
            FadeIO.fadeIo.FadeOut(Menu.FADE);
            BGMMaster.AudioFadeOutStart(Menu.FADE);
            SEMaster.AudioFadeOutStart(Menu.FADE);
            runningFunc = () => LoadTitleScene();
            StartCoroutine(RunCoroutine(Menu.FADE, runningFunc));
            KeyLoader.keyLoader.SetStickWait(RESET_STAGE_FRAME);
        }
        //ポーズ画面を開く処理
        else if(!mainCameraObj.isRotation && !floorMove.isMoving &&
            !KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.StartKey)
        {
            temp = mode;
            mode = PAUSE;
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Pause));
            pause.Open();

            stCur.Set(false);
        }
        //ジョイスティック入力:右
        else if (!mainCameraObj.isRotation && !KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x > 0)
        {
            if (stage == Menu.GOD)
            {
                stage = Menu.MONSTER;
            }
            else
            {
                stage++;
            }

            SEMaster.Play(stageSelectSE);
            StartCoroutine(mainCameraObj.Right(stage));
            StartCoroutine(stNameObj.TargetFrameMoveRightReset(CAMERA_ROTATION_TIME));
            KeyLoader.keyLoader.SetStickWait(RESET_STAGE_FRAME);
        }
        //ジョイスティック入力:左
        else if (!mainCameraObj.isRotation && !KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.x < 0)
        {
            if (stage == Menu.MONSTER)
            {
                stage = Menu.GOD;
            }
            else
            {
                stage--;
            }
            SEMaster.Play(stageSelectSE);
            StartCoroutine(mainCameraObj.Left(stage));
            StartCoroutine(stNameObj.TargetFrameMoveLeftReset(CAMERA_ROTATION_TIME));
            KeyLoader.keyLoader.SetStickWait(RESET_STAGE_FRAME);
        }
    }
    /// <summary>
    /// フロアを選ぶ関数
    /// </summary>
    private void SelectFloor()
    {
        Floor_Draw();
        //選んだステージのフロアに入る処理
        if (!floorMove.isMoving && !KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.A)
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));
            KeyLoader.keyLoader.SetKeyWait(Menu.FADE);
            KeyLoader.keyLoader.SetStickWait(Menu.FADE);
            FadeIO.fadeIo.FadeOut(Menu.FADE);
            BGMMaster.AudioFadeOutStart(Menu.FADE);
            SEMaster.AudioFadeOutStart(Menu.FADE);
            runningFunc = () => LoadGameScene();
            StartCoroutine(RunCoroutine(Menu.FADE, runningFunc));
        }
        //ステージ選択に戻る処理
        else if (!floorMove.isMoving && !KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.B)
        {
            mode = STAGE_SELECT;
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Cancel));
            StartCoroutine(floorMove.SlideOut());
            KeyLoader.keyLoader.SetStickWait(Menu.FADE);

            stCur.Set(true);
        }
        //ポーズ画面を開く処理
        else if (!floorMove.isMoving && !KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.StartKey)
        {
            temp = mode;
            mode = PAUSE;
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Pause));
            pause.Open();       //画面を開く処理
        }
        //ジョイスティック入力:上
        else if (!floorMove.isMoving && !KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y > 0)
        {
            int before = floor;
            //一つ上のフロアが未開放フロアまたはフロア6ならフロア1に戻す
            if (ClearFloor[stage] <= floor || floor >= Menu.FLOOR6)
            {
                floor = Menu.FLOOR1;
            }
            else
            {
                floor++;
            }
            if (before != floor)
            {
                SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            }
            KeyLoader.keyLoader.SetStickWait(RESET_FLOOR_FRAME);
        }
        //ジョイスティック入力:下
        else if (!floorMove.isMoving && !KeyLoader.keyLoader.StickWait && KeyLoader.keyLoader.joyStickAxis.y < 0)
        {
            int before = floor;
            //1階目で操作した際に挑戦可能な最高フロアに戻す処理
            if (floor == Menu.FLOOR1)
            {
                if (ClearFloor[stage] == Menu.FLOOR_NUM) 
                {
                    floor = Menu.FLOOR6;
                }
                else
                {
                    floor = ClearFloor[stage];
                }
            }
            else
            {
                floor--;
            }
            if (before != floor)
            {
                SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Move));
            }
            KeyLoader.keyLoader.SetStickWait(RESET_FLOOR_FRAME);
        }
    }
    /// <summary>
    /// ロード画面をはさみ、ゲームシーンへ遷移する
    /// </summary>
    private void LoadGameScene()
    {
        Load.SetNextSceneName((stage + 1) + "-" + (floor + 1));
        SceneManager.LoadScene(SceneNames.StageLoadName);
    }
    /// <summary>
    /// ロード画面をはさみ、タイトルに戻る
    /// </summary>
    private void LoadTitleScene()
    {
        Load.SetNextSceneName(SceneNames.TitleName);
        SceneManager.LoadScene(SceneNames.LoadName);
    }
    /// <summary>
    /// 指定フレーム後に指定の関数を行うコルーチン
    /// ※関数はdelegate型で宣言したものにラムダ式を用いて代入している
    /// </summary>
    /// <param name="frame">待つフレーム</param>
    /// <param name="func">行う関数</param>
    /// <returns>1フレームずつ値を返す</returns>
    private IEnumerator RunCoroutine(int frame, routineFunc func)
    {
        for (int i = 0; i < frame; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        func();
    }
    /// <summary>
    /// フロア選択中に毎フレーム呼ぶ描画系処理を一括にした関数
    /// </summary>
    private void Floor_Draw()
    {
        floorBackSprite.Draw(floor);
        BestTimeStr.Draw(floor);
        BestTimeNum.Draw(floor);
        floorClearMarker.Draw(floor);
        floorTop.Draw(floor);

        floorNameManager.Draw(floor);
    }
    /// <summary>
    /// ポーズメニューの処理
    /// </summary>
    private void PauseMenu()
    {
        if (!pause.isChanging && pause.isDisplaying)
        {
            pause.PauseUpdate();
        }
        else if(!pause.isChanging && !pause.isDisplaying)
        {
            pause.Reset();
            mode = temp;
            if (mode == STAGE_SELECT)
            {
                stCur.Set(true);
            }
        }
    }
}
