using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamepadInput;

/// <summary>
/// キー読み込みの制御
/// 製作者：冨田 大悟
/// </summary>
public class KeyLoader : MonoBehaviour {

    public static KeyLoader keyLoader;

    [SerializeField]
    GamepadInput.GamePad.Index pnam;

    public Vector2 joyStickAxis {
        private set;
        get;
    }
    public bool A
    {
        private set;
        get;
    }
    public bool B
    {
        private set;
        get;
    }
    public bool X
    {
        private set;
        get;
    }
    public bool Y
    {
        private set;
        get;
    }

    public bool Jump
    {
        private set;
        get;
    }
    public bool Magic
    {
        private set;
        get;
    }


    public bool Back
    {
        private set;
        get;
    }
    public bool StartKey
    {
        private set;
        get;
    }

    bool buttonWait = false;
    public bool StickWait = false;

    GamePad.Button jumpButton = GamePad.Button.A;
    GamePad.Button magicButton = GamePad.Button.B;

    [SerializeField]
    bool DebugMode = true;

    private void Awake()
    {
        keyLoader = this;
        gameObject.AddComponent<KeyUpdateTiming>();
        gameObject.AddComponent<KeyReleaseTiming>();    }

    private void Start()
    {
        KeyReload();
    }

    /// <summary>
    /// キーの更新
    /// </summary>
    public void KeyUpdate()
    {
        joyStickAxis = GamePad.GetAxis(GamePad.Axis.LeftStick, pnam);
        
        if (!buttonWait)
        {
            A = GamePad.GetButtonDown(GamePad.Button.A, pnam);
            B = GamePad.GetButtonDown(GamePad.Button.B, pnam);
            Jump = GamePad.GetButtonDown(jumpButton, pnam);
            Magic = GamePad.GetButtonDown(magicButton, pnam);
            X = GamePad.GetButtonDown(GamePad.Button.X, pnam);
            Y = GamePad.GetButtonDown(GamePad.Button.Y, pnam);
            Back = GamePad.GetButtonDown(GamePad.Button.Back, pnam);
            StartKey = GamePad.GetButtonDown(GamePad.Button.Start, pnam);
        }

    }

    /// <summary>
    /// キーの解放
    /// </summary>
    public void KeyRelease()
    {
        A = false;
        B = false;
        X = false;
        Jump = false;
        Magic = false;
        Back = false;
        StartKey = false;

    }

    /// <summary>
    /// キーにウェイトをかける
    /// </summary>
    /// <param name="time">時間</param>
    public void SetKeyWait(int time)
    {
        StartCoroutine(SetKeyWaitCor(time));
    }

    IEnumerator SetKeyWaitCor(int time)
    {
        buttonWait = true;
        while(time-- > 0)
        {
            yield return null;
        }
        buttonWait = false;
    }

    /// <summary>
    /// スティックキーにウェイトをかける
    /// </summary>
    /// <param name="time">時間</param>
    public void SetStickWait(int time)
    {
        if (time != 0)
        {

            StartCoroutine(SetStickWaitCor(time));
        }else
        {
            StopCoroutine("SetStickWaitCor");
            StickWait = false;
        }
    }

    IEnumerator SetStickWaitCor(int time)
    {
        StickWait = true;

        while (time-- > 0 && StickWait)
        {
            yield return null;
        }
        StickWait = false;
    }

    /// <summary>
    /// アクションキーの更新
    /// </summary>
    public void KeyReload()
    {
        if (!DebugMode)
        {
            switch (SaveData.GetInt("JumpKey"))
            {
                case 0:
                    jumpButton = GamePad.Button.A;
                    break;
                case 1:
                    jumpButton = GamePad.Button.X;
                    break;
                case 2:
                    jumpButton = GamePad.Button.Y;
                    break;
                case 3:
                    jumpButton = GamePad.Button.B;
                    break;
            }
            switch (SaveData.GetInt("MagicKey"))
            {
                case 0:
                    magicButton = GamePad.Button.A;
                    break;
                case 1:
                    magicButton = GamePad.Button.X;
                    break;
                case 2:
                    magicButton = GamePad.Button.Y;
                    break;
                case 3:
                    magicButton = GamePad.Button.B;
                    break;

            }
        }
    }

    /// <summary>
    /// どこかのボタンが押されているか
    /// </summary>
    /// <returns>押されているか</returns>
    public bool IsAnyKey()
    {
        return A || B || X || Y || Back || StartKey;
    }

    /// <summary>
    /// バイブレーション
    /// </summary>
    /// <param name="count">時間</param>
    /// <param name="vibrationPowerx">右の強さ</param>
    /// <param name="vibrationPowery">左の強さ</param>
    public IEnumerator Vibration(float count,float vibrationPowerx, float vibrationPowery)
    {

        XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, vibrationPowerx, vibrationPowery);
        yield return new WaitForSeconds(count);
        XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 0, 0);
    }
}
