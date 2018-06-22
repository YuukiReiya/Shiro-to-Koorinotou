using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オープニングのボタンを押してスキップ
/// 製作者：冨田 大悟
/// </summary>
public class OPPushAnyButton : MonoBehaviour {

    [SerializeField]
    Color startCollor;
    [SerializeField]
    float colorASpeed;

    private void Start()
    {
        GetComponent<Image>().color = startCollor;
    }

    public void DrawStart()
    {
        StartCoroutine(OPPushAnyButtonDraw());
    }

    IEnumerator OPPushAnyButtonDraw()
    {
        while (true)
        {
            if (colorASpeed > 0)
            {
                if (GetComponent<Image>().color.a + colorASpeed < 1)
                {
                    GetComponent<Image>().color += new Color(0, 0, 0, colorASpeed);

                }
                else
                {
                    Color icolor = GetComponent<Image>().color;
                    GetComponent<Image>().color = new Color(icolor.r, icolor. g, icolor.b, 1);
                    colorASpeed = -colorASpeed;
                }
            }else
            {
                if (GetComponent<Image>().color.a + colorASpeed > 0)
                {
                    GetComponent<Image>().color += new Color(0, 0, 0, colorASpeed);

                }
                else
                {
                    Color icolor = GetComponent<Image>().color;
                    GetComponent<Image>().color = new Color(icolor.r, icolor.g, icolor.b, 0);
                    colorASpeed = -colorASpeed;
                }
            }
            yield return null;
        }
    }


}
