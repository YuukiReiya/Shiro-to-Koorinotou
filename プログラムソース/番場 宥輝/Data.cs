using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "データ"の消去処理を行うクラス
/// 制作者:番場宥輝
/// </summary>
public class Data : MonoBehaviour
{

    [SerializeField]
    private ChangeScale message;    //メッセージウィンドウ

    /// <summary>
    /// プロパティの外部参照用宣言
    /// </summary>
    public ChangeScale Message
    {
       private set { message = value; }
        get { return message; }
    }
    /// <summary>
    /// "データ"のアップデート関数
    /// </summary>
    /// <returns>true:継続,false:終了</returns>
    public bool DeleteUpdate()
    {
        if (!message.isMaxScale && !message.isChanging && KeyLoader.keyLoader.A)
        {
            Delete();
            SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Decide));
            message.StartExpansion();
            return true;
        }
        else if (message.isMaxScale && !message.isChanging) 
        {
            if (KeyLoader.keyLoader.A)
            {
                SEMaster.Play(KeyInputSE.keyInoutSE.GetClip(KeyInputSE.Type.Cancel));
                message.StartNarrow();
            }
            return true;
        }
        else if(message.isChanging)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// データを消す関数
    /// </summary>
    private void Delete()
    {
        //データ消す処理
        for (int i = 1; i <= Menu.TOWER_NUM; i++) 
        {
            //クリアフラグを消す
            SaveData.SaveInt("Stage" + i + "ClearFlag", 0);
            for (int j = 1; j <= Menu.FLOOR_NUM; j++) 
            {
                //ベストタイムを初期化する
                SaveData.SaveFloat("Stage" + i + "-" + j + "ClearTime", 99.99f);
            }
        }
        //消したデータをセーブする処理
        SaveData.AllSave();
    }

}

