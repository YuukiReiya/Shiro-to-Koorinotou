using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// フロアのUIの制御
/// </summary>
public class FloorUI : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI stageNameText;
    [SerializeField]
    TextMeshProUGUI stageNameTextShadow;


    [SerializeField]
    Image towerImage;
    [SerializeField]
    Sprite[] towerImageSprite;

    [SerializeField]
    Image floorImage;
    [SerializeField]
    Sprite[] floorImageSprite;

    [SerializeField]
    Image towerFloorImage;

    [SerializeField]
    Sprite[] towerFloorImageSprite;
    [SerializeField]
    Image[] fadeImage;


    [SerializeField]
    Image siroImage;

    float baseY;
    [SerializeField]
    float upY;
    [SerializeField]
    float speed =1;


	// Use this for initialization
	void Start () {

        stageNameText.text = FloorNameList.FLOOR_NAME[SceneNames.GetStageNam(SceneManager.GetActiveScene().name)-1 ][SceneNames.GetFloorNam(SceneManager.GetActiveScene().name)-1];
        stageNameTextShadow.text = FloorNameList.FLOOR_NAME[SceneNames.GetStageNam(SceneManager.GetActiveScene().name) - 1][SceneNames.GetFloorNam(SceneManager.GetActiveScene().name) - 1];

        towerImage.sprite = towerImageSprite[SceneNames.GetStageNam(SceneManager.GetActiveScene().name)-1];
        towerFloorImage.sprite = towerFloorImageSprite[SceneNames.GetStageNam(SceneManager.GetActiveScene().name) - 1];

        floorImage.sprite = floorImageSprite[SceneNames.GetFloorNam(SceneManager.GetActiveScene().name)-1];
        baseY = siroImage.rectTransform.localPosition.y;
        setTowerNam(0);
    }

   public void setTowerNam(int nam)
    {
        float ypos = baseY +(upY * nam);
        Vector3 pos = siroImage.rectTransform.localPosition;
        //siroImage.rectTransform.localPosition = new Vector3(pos.x, ypos, pos.z);
        StartCoroutine(moveToPos(new Vector3(pos.x, ypos, pos.z)));
        for (int i = 0; i<fadeImage.Length; i++)
        {
            fadeImage[i].gameObject.SetActive(i != nam);
        }
    }

    IEnumerator moveToPos(Vector3 pos)
    {
        while(siroImage.rectTransform.localPosition != pos)
        {
            siroImage.rectTransform.localPosition = Vector3.MoveTowards(siroImage.rectTransform.localPosition, pos, speed);
            yield return new WaitForFixedUpdate();
        }
    }
}