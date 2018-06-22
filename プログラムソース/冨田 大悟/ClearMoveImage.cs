using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// クリア時の文字のアニメーション
/// </summary>
public class ClearMoveImage : MonoBehaviour {

    
    [SerializeField]
    float sinSpeed;
    [SerializeField]
    float sinSize;

    [SerializeField]
    int stWait;
    [SerializeField]
    int oneTime;

    [SerializeField]
    Image[] images;

    private void Start()
    {
        StartCoroutine(ImageMove());
    }
   

    IEnumerator ImageMove()
    {
        for(int i = 0; i < stWait; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        int w = 0;
        while(true)
        {
            StartCoroutine(moves(images[w]));

            for (int j = 0; j < oneTime; j++)
            {
                
                yield return new WaitForFixedUpdate();
            }
            w = (w + 1) % images.Length;
        }
    }


    IEnumerator moves(Image image)
    {
        float time = 0;
        Vector3 basePos = image.rectTransform.localPosition;
        while (time < 3.14)
        {
            image.rectTransform.localPosition = new Vector3(basePos.x, basePos.y + (Mathf.Sin(time)*sinSize), 0);
            time += sinSpeed;
            yield return new WaitForFixedUpdate();
        }
        image.rectTransform.localPosition = basePos;

    }
    

}
