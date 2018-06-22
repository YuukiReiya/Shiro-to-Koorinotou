using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの名前、レベル、コンプリートマークを制御するクラス
/// 制作者:番場 宥輝
/// </summary>
public class StageNameObj : MonoBehaviour {

    [SerializeField]
    private  float speed = 0.1f;                //オブジェクトの移動速度
    private Vector3 initializePos;              //オブジェクトの初期位置
    [SerializeField]
    private SpriteFade backWindowFade;          //バックウィンドウのフェード
    private ChangeSprite backWindowSprite;      //バックウィンドウのスプライト
    [SerializeField]
    private SpriteFade stageNameFade;           //ステージ名のフェード
    private ChangeSprite stageNameSprite;       //ステージ名のスプライト
    [SerializeField]
    private SpriteFade stageLevelFade;          //難易度のフェード
    private ChangeSprite stageLevelSprite;      //難易度のスプライト
    [SerializeField]
    private SpriteFade stageCompFade;           //コンプリートマークのフェード
    private TowerOfComplete stageComp;          //コンプリートマークの制御クラスの宣言

    void Start()
    {
        //各ChangeSpriteはSpriteFadeがアタッチされているゲームオブジェクトから参照する
        backWindowSprite=backWindowFade.gameObject.GetComponent<ChangeSprite>();
        backWindowSprite.UpdateSprite(StageSelect.stage);
        stageNameSprite = stageNameFade.gameObject.GetComponent<ChangeSprite>();
        stageNameSprite.UpdateSprite(StageSelect.stage);
        stageLevelSprite = stageLevelFade.gameObject.GetComponent<ChangeSprite>();
        stageLevelSprite.UpdateSprite(StageSelect.stage);
        stageComp = stageCompFade.gameObject.GetComponent<TowerOfComplete>();
        StartCoroutine(stageComp.UpdateSprite());
        stageComp.SetMarker();
        initializePos = this.transform.localPosition;
    }
    /// <summary>
    /// 右に動かす関数
    /// </summary>
    private void MoveRight()
    {
        this.transform.position += this.transform.right * speed;
    }
    /// <summary>
    /// 左に動かす関数
    /// </summary>
    private void MoveLeft()
    {
        this.transform.position += this.transform.right * (-1) * speed;
    }
    /// <summary>
    /// 位置を初期化する関数
    /// </summary>
    private void Reset()
    {
        this.transform.localPosition = initializePos;
    }
    /// <summary>
    /// 指定フレームの間右に動かし、その後位置を初期化するコルーチン
    /// </summary>
    /// <param name="frame">処理を行うフレーム</param>
    /// <returns></returns>
    public IEnumerator TargetFrameMoveRightReset(int frame)
    {

        for (int i = 0; i < frame; i++)
        {
            MoveRight();
            backWindowFade.Fade();
            stageNameFade.Fade();
            stageLevelFade.Fade();
            stageCompFade.Fade();
            yield return new WaitForEndOfFrame();
        }
        backWindowSprite.UpdateSprite(StageSelect.stage);
        stageNameSprite.UpdateSprite(StageSelect.stage);
        stageLevelSprite.UpdateSprite(StageSelect.stage);
        backWindowFade.Reset();
        stageNameFade.Reset();
        stageLevelFade.Reset();
        stageCompFade.Reset();
        stageComp.SetMarker();
        Reset();
    }
    /// <summary>
    /// 指定フレームの間左に動かし、その後位置を初期化するコルーチン
    /// </summary>
    /// <param name="frame">処理を行うフレーム</param>
    /// <returns></returns>
    public IEnumerator TargetFrameMoveLeftReset(int frame)
    {

        for (int i = 0; i < frame; i++)
        {
            MoveLeft();
            backWindowFade.Fade();
            stageNameFade.Fade();
            stageLevelFade.Fade();
            stageCompFade.Fade();
            yield return new WaitForEndOfFrame();
        }
        backWindowSprite.UpdateSprite(StageSelect.stage);
        stageNameSprite.UpdateSprite(StageSelect.stage);
        stageLevelSprite.UpdateSprite(StageSelect.stage);
        backWindowFade.Reset();
        stageNameFade.Reset();
        stageLevelFade.Reset();
        stageCompFade.Reset();
        stageComp.SetMarker();
        Reset();
    }
}
