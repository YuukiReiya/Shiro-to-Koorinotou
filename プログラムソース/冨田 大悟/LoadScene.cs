using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ステージロードのロードシーン
/// 製作者：冨田 大悟
/// </summary>
public class LoadScene : MonoBehaviour {

    [SerializeField]
    string str;

    [SerializeField]
    Load load;

    [SerializeField]
    TextMeshProUGUI stageNameText;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        int nextTowerNam = int.Parse(Load.GetNextSceneName().Substring(0, 1))-1;
        int nextFloorNam = int.Parse(Load.GetNextSceneName().Substring(2))-1;

        ImageSet(nextTowerNam, nextFloorNam);

        XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 0, 0);
        StartCoroutine(load.LoadScene());
        SaveData.AllSave();
        Resources.UnloadUnusedAssets();
	}

    private void OnValidate()
    {
        int nextTowerNam = int.Parse(str.Substring(0, 1)) - 1;
        int nextFloorNam = int.Parse(str.Substring(2)) - 1;

        ImageSet(nextTowerNam, nextFloorNam);
    }

    void ImageSet(int nextTowerNam,int nextFloorNam)
    {
        stageNameText.text = FloorNameList.FLOOR_NAME[nextTowerNam][nextFloorNam];
    }
}
