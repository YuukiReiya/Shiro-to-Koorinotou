using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


using UnityEngine.SceneManagement;

/// <summary>
/// ステージ全体の制御クラス
/// 製作者：冨田 大悟
/// </summary>
public class ALLTower : MonoBehaviour {


    [SerializeField]
    ALLTowerBaseDeta allTowerBaseDeta;

    [SerializeField]
    int towerSize = 3;

    [SerializeField]
    CameraOperation cameraOp;

    [SerializeField]
    Vector3 playerPopPos = new Vector3(-10,1);
    [SerializeField]
    Player player;//プレハブのち本体


    [SerializeField]
    Tower tower;
    [SerializeField]
    List<Tower> towers;
    [SerializeField]
    float startHeight;
    [SerializeField]
    float height;

    int floorNamber;

    [SerializeField]//EPSが入っている親
    GameObject EPSParent;
    [SerializeField]
    List< EnemyPopSystem> EPSystem;
    

    private enum TowerState
    {
        GameStart,TowerStart,TowerUpdate,CametaUpUpdate,GameClearEnd,GameOverEnd,Pose,LoadWait
    };
    
    [SerializeField]
    TowerState towerState;
    TowerState backTowerState;

    AudioClip clearSE;
    AudioClip clearBGM;
    StartCreatPrefabs startCreatPrefabs;

    //全ステージ共通
    GamePauseUI gamePoiseUI;
    GameClearNextSelect clearNextSelectUI;
    GameOverSelectUI gameOverNextSelectUI;
    ReadyUI readyUI;
    GameObject keyCanvas;
    FloorUI floorUI;

    [SerializeField]
    List<GameObject> autoCreatObj;

    /// <summary>
    /// Towerを生成する
    /// </summary>
    [ContextMenu("FloorsSet")]
    private void FloorsSet()
    {

        Debug.Log("タワーセット");

        Transform tr = transform.FindChild("Floors");
        if (tr != null)
        {
            DestroyImmediate(tr.gameObject);
        }

        GameObject newParent = new GameObject("Floors");
        newParent.transform.parent = this.transform;

        towers = new List<Tower>();
        for (int i = 0; i < towerSize; i++)
        {
            towers.Add(Instantiate<Tower>(tower, new Vector3(0, startHeight + height * i, 0), tower.transform.rotation, newParent.transform));
        }

        foreach(Tower t in towers){
            t.WallSet();
        }
    }

    private void Awake()
    {
        FloorsSet();

        SetEPS();
        CreatBase();

        foreach (GameObject go in autoCreatObj)
        {
            Instantiate(go);
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;

        floorNamber = 0;

        EPSystem[0].EPSFOne();

        BlockObj.playerDeid = false;
        StageEffect.EffectActive(floorNamber);

        towerState = TowerState.GameStart;
        FadeIO.fadeIo.FadeIn(40);
        SEMaster.AudioFadeInStart(40);
        BGMMaster.AudioFadeInStart(40);
    }

    // Update is called once per frame

    void Update () {
        switch(towerState)
        { 
            case TowerState.Pose:
                if (gamePoiseUI.GamePauseUIUpdate())
                {
                    switch (gamePoiseUI.pauseEndPattern)
                    {
                        case GamePauseUI.PauseEndPattern.Back:
                            PoseEnd();

                            break;
                        case GamePauseUI.PauseEndPattern.Retry:
                            Retry();
                            break;
                        case GamePauseUI.PauseEndPattern.StageSelect:
                            GotoStageSelect();
                            break;
                        case GamePauseUI.PauseEndPattern.Title:
                            GotoTitle();

                            break;
                    }
                }
                break;
        }

    }

    private void FixedUpdate()
    {
        switch (towerState)
        {
            case TowerState.GameStart:
                GameStart();
                break;
            case TowerState.TowerStart:
                towerState = TowerState.TowerUpdate;

                if (KeyLoader.keyLoader.StartKey)
                {
                    PoseStart();
                }else
                if (KeyLoader.keyLoader.Back)
                {
                    Retry();
                }
                break;
            case TowerState.TowerUpdate://更新
                TowerUpdate();
                if (KeyLoader.keyLoader.StartKey)
                {
                    PoseStart();
                }
                else
                if (KeyLoader.keyLoader.Back)
                {
                    Retry();
                }
                break;
            case TowerState.CametaUpUpdate:
                CamaraUpUpdate();
                if (KeyLoader.keyLoader.StartKey)
                {
                    PoseStart();
                }
                else
                if (KeyLoader.keyLoader.Back)
                {
                    Retry();
                }
                break;
            case TowerState.GameClearEnd:
                GameClearEventUpdate();
                break;

            case TowerState.GameOverEnd:
                GameOverUpdate();
                break;
            case TowerState.LoadWait:
                //処理なし
                break;
        }
    }

    /// <summary>
    /// ゲームの開始時の処理
    /// </summary>
    void GameStart()
    {
        if (!FadeIO.fadeIo.fadeNow)
        {
            readyUI.ReadyUIUpdate();
            if (readyUI.IsReadyTimeEnd())
            {
                TimeCounter.timeCounter.TimeCountStart();
                EPSystem[0].EPSStart();
                
                towerState = TowerState.TowerUpdate;
                player.controlOn = true;
            }
        }
    }
    /// <summary>
    /// タワーの更新
    /// </summary>
    void TowerUpdate()
    {
        EPSystem[floorNamber].EPSUpdate();
        towers[floorNamber].TowerUpdate();

        if (towers[floorNamber].GetComponent<Tower>().end)
        {
            FloorEnd();
        }else if (!towers[floorNamber].GetComponent<Tower>().IsDeadSpeed() && player.PlayerSandwiched())
        {
            GameOverStert();
        }
        

    }
    /// <summary>
    /// カメラ上昇の更新
    /// </summary>
    void CamaraUpUpdate() {
        if (!cameraOp.CoordinateReaching())//カメラ上昇
        {
            cameraOp.CamareMove();
            EPSystem[floorNamber].CamaeraUpUpdate();
        }
        else//カメラ上昇完了
        {
            CameraUpEnd();
        }
    }


    //---フロアクリア----
    /// <summary>
    /// フロアクリア時の処理
    /// </summary>
    private void FloorEnd()
    {
        bool allLife;

        allLife = !towers[floorNamber].IsInPlayerDiedRange();

        if (allLife)//プレイヤー生存
        {

            if (floorNamber < towers.Count - 1)//階層クリア
            {
                FloorClear();
            }
            else//階層をすべて攻略(クリア)
            {
                FloorAllClear();
            }
            Camera.main.GetComponent<CameraShake>().Shake();
        }
        else
        {
            DestoryAnderObj();
            EPSystem[floorNamber].TowerEnd();

            GameOverStert();
        }
    }
    /// <summary>
    /// クリア時の処理
    /// </summary>
    private void FloorClear()
    {
        floorNamber++;
        cameraOp.nam = floorNamber;
        cameraOp.SetTergetPos();
        DestoryAnderObj();
        EPSystem[floorNamber-1].TowerEnd();

        EPSystem[floorNamber].SetEndBlock(towers[floorNamber-1].hitBlock);

        EPSystem[floorNamber].EPSStart();
        EPSystem[floorNamber].CamaeraUpStert();
        StageEffect.EffectActive(floorNamber);

        floorUI.setTowerNam(floorNamber);

        towerState = TowerState.CametaUpUpdate;
    }
    /// <summary>
    /// オブジェクトの破棄
    /// </summary>
    private void DestoryAnderObj()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("blockObj"))
        {
            DestroyImmediate(go);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("enemy"))
        {

            go.GetComponent<Enemy>().WallebatanEffect();
            DestroyImmediate(go);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Shot"))
        {
            if (go.GetComponent<EnemyShot>())
            {
                go.GetComponent<EnemyShot>().WallebatanEffect();
                DestroyImmediate(go);
            }
        }
    }

    /// <summary>
    /// カメラの上昇の終了処理
    /// </summary>
    private void CameraUpEnd()
    {
        EPSystem[floorNamber].CamaeraUpEnd();
        towerState = TowerState.TowerStart;
    }

    //---ゲームクリア-----
    /// <summary>
    /// フロアをすべてクリアした
    /// </summary>
    private void FloorAllClear()
    {
        GameClearEventStert();
        DestoryAnderObj();
        EPSystem[floorNamber].TowerEnd();
        StageEffect.EffectAllStop();
        SEMaster.Play(clearSE);
        BGMMaster.CangeBGM(clearBGM);

        towerState = TowerState.GameClearEnd;
    }

    /// <summary>
    /// ゲームクリア演出の開始
    /// </summary>
    private void GameClearEventStert()
    {
        TimeCounter.timeCounter.TimeCountStop();

        cameraOp.GameClearEventCameraStart();
        player.controlOn = false;
        player.ClearMoveStart();
        //UIを破壊
        Destroy(floorUI.gameObject);
        Destroy(keyCanvas.gameObject);

        
    }
    /// <summary>
    /// クリア演出の更新
    /// </summary>
    private void GameClearEventUpdate()
    {
        if (clearNextSelectUI!= null)
        {
            clearNextSelectUI.NextSelectUpdate();
            if (KeyLoader.keyLoader.A)
            {
                SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));

                string nextName = clearNextSelectUI.GetNextSceneName();
                if (nextName == SceneNames.StageSelectName)
                {
                    GotoStageSelect();
                }
                else
                {
                    GoToNextStage(nextName);
                }
            }

        }
        else
        {
            
            cameraOp.GameClearEventCameraUpdate();

            player.ToClareMovePos();
            if (cameraOp.IsClearCameraPos() && player.IsClareMovePos())
            {//到着瞬間
                clearNextSelectUI = Instantiate(startCreatPrefabs.clearNextSelectUI);
                clearNextSelectUI.NextSelectStart();
                TimeCounter.timeCounter.DrawNewRecord();

                //セーブ処理
                ClearSave();

                Instantiate(startCreatPrefabs.gameClearUiManager);
                Instantiate(startCreatPrefabs.clearEffect);
            }
        }
    }

    //---ゲームオーバー---
    /// <summary>
    /// ゲームオーバーの開始
    /// </summary>
    void GameOverStert()
    {
        towerState = TowerState.GameOverEnd;
        gameOverNextSelectUI = Instantiate(startCreatPrefabs.gameOverNextSelectUI);
        gameOverNextSelectUI.NextSelectStart();
        player.CreatDeadEffect();
        Destroy(player.gameObject);
        StartCoroutine(KeyLoader.keyLoader.Vibration(1f, 1f, 1f));
    }

    /// <summary>
    /// ゲームオーバーの更新
    /// </summary>
    void GameOverUpdate()
    {
        keyCanvas.SetActive(false);

        player.controlOn = false;
        TimeCounter.timeCounter.TimeCountStop();
        gameOverNextSelectUI.NextSelectUpdate();
        if (KeyLoader.keyLoader.A)
        {
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));
            string nextName = gameOverNextSelectUI.GetNextSceneName();
            if (nextName == SceneNames.StageSelectName)
            {
                GotoStageSelect();
            }
            else
            {
                Retry();
            }
        }
    }

    //EPS展開
    [ContextMenu("SetEPS")]
    private void SetEPS()
    {
        EPSystem.Clear();
        EPSystem.AddRange( EPSParent.transform.GetComponentsInChildren<EnemyPopSystem>());
    }
    [ContextMenu("CreatBase")]
    private void CreatBase()
    {
        //ベース代入値
        startCreatPrefabs = allTowerBaseDeta.startCreatPrefabs;
        clearSE = allTowerBaseDeta.clearSE;
        clearBGM = allTowerBaseDeta.clearBGM;


        //生成
        player = Instantiate(player, playerPopPos, player.transform.rotation);
        gamePoiseUI = Instantiate(startCreatPrefabs.gamePoiseUI);
        readyUI = Instantiate(startCreatPrefabs.readyUI);
        keyCanvas = Instantiate(startCreatPrefabs.keyCanvas);

        floorUI = Instantiate(startCreatPrefabs.floorUI);
    }


    /// <summary>
    /// ポーズの開始
    /// </summary>
    private void PoseStart()
    {
        SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Pause));
        Time.timeScale = 0;
        KeyLoader.keyLoader.KeyRelease();
        backTowerState = towerState;
        towerState = TowerState.Pose;
        gamePoiseUI.GamePauseUIStart();
        XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 0, 0);
        keyCanvas.SetActive(false);

        BGMMaster.Stop();
    }
    /// <summary>
    /// ポーズの終了
    /// </summary>
    private void PoseEnd()
    {
        SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Cancel));

        Time.timeScale = 1;
        //再開後にキーの情報が残っているので解放
        KeyLoader.keyLoader.KeyRelease();
        towerState = backTowerState;
        keyCanvas.SetActive(true);

        BGMMaster.Play();
    }



    //------------------シーン移行--------------------------
    /// <summary>
    /// リトライの実行
    /// </summary>
    public void Retry()
    {
        Load.SetNextSceneName(SceneManager.GetActiveScene().name);

        StartCoroutine(Load.WaitLoadScene(SceneNames.StageLoadName, 40));
        LoadSet();
    }

    /// <summary>
    /// ステージセレクトにシーンを移行
    /// </summary>
    public void GotoStageSelect()
    {
        Load.SetNextSceneName(SceneNames.StageSelectName);

        StartCoroutine(Load.WaitLoadScene(SceneNames.LoadName, 40));
        LoadSet();
    }
    /// <summary>
    /// タイトルにシーン移行
    /// </summary>
    public void GotoTitle()
    {
        Load.SetNextSceneName(SceneNames.TitleName);

        StartCoroutine(Load.WaitLoadScene(SceneNames.LoadName, 40));
        LoadSet();
    }

    /// <summary>
    /// 次のステージに移行
    /// </summary>
    /// <param name="nextStageName">次のシーンの名前</param>
    public void GoToNextStage(string nextStageName)
    {
        Load.SetNextSceneName(nextStageName);
        StartCoroutine(Load.WaitLoadScene(SceneNames.StageLoadName, 40));
        LoadSet();
    }

    /// <summary>
    /// シーン移行の前処理
    /// </summary>
    void LoadSet()
    {
        SEMaster.AudioFadeOutStart(40);
        BGMMaster.AudioFadeOutStart(40);
        FadeIO.fadeIo.FadeOut(40);
        towerState = TowerState.LoadWait;
        KeyLoader.keyLoader.SetKeyWait(45);
        XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 0, 0);
    }

    /// <summary>
    /// クリア情報の保存
    /// </summary>
    void ClearSave()
    {
        SaveData.SaveInt("Stage" + SceneNames.GetStageNam(SceneManager.GetActiveScene().name) + "ClearFlag", SaveData.GetInt("Stage" + SceneNames.GetStageNam(SceneManager.GetActiveScene().name) + "ClearFlag") | (1 << (SceneNames.GetFloorNam(SceneManager.GetActiveScene().name)-1)));
        TimeCounter.timeCounter.SaveClearTime();
    }
}
