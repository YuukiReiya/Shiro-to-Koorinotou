using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージセレクト行き、タイトル行きのロードシーン
/// 製作者：冨田 大悟
/// </summary>
public class NomalLoad : MonoBehaviour {

    [SerializeField]
    Load load;
    [SerializeField]
    Image[] paseNam;
    [SerializeField]
    Sprite[] pase;
    [SerializeField]
    Sprite[] paseNamImages;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 0, 0);
        StartCoroutine(load.LoadScene(true,1));
        Resources.UnloadUnusedAssets();
        SaveData.AllSave();

    }

    private void Update()
    {
        if (load.async.progress > 0.88f)
        {
            paseNam[0].sprite = paseNamImages[1];
            paseNam[1].sprite = paseNamImages[0];
            paseNam[2].sprite = paseNamImages[0];
            paseNam[0].color = new Color(1 , 1, 1, 1);
            paseNam[1].color = new Color(1, 1, 1, 1);

        }
        else
        {
            int loadNam = (int)(load.async.progress * 100);

            paseNam[0].color = new Color(1, 1, 1, 0);
            if (loadNam < 10)
            {
                paseNam[1].color = new Color(1, 1, 1, 0);

            }
            else
            {
                paseNam[1].sprite = paseNamImages[loadNam / 10];
                paseNam[1].color = new Color(1, 1, 1, 1);

            }
            Debug.Log(loadNam % 10);
            paseNam[2].sprite = paseNamImages[loadNam % 10];
        }

    }
}
