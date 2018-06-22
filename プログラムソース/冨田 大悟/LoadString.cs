using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 読み込み中文字の点滅
/// 製作者：冨田 大悟
/// </summary>
public class LoadString : MonoBehaviour {

    [SerializeField]
    Image[] dotImage = new Image[3];
    int count;
    [SerializeField]
    int countNam;

    // Use this for initialization
    void Start () {
        foreach (Image image in dotImage)
        {
            image.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 3; i++)
        {
            int drawNam = countNam * (i + 1);
            if (count > drawNam)
            {
                dotImage[i].gameObject.SetActive(true);
            }
            else
            {
                dotImage[i].gameObject.SetActive(false);
            }
        }
        count = (count + 1) % (countNam * 4);
    }
}
