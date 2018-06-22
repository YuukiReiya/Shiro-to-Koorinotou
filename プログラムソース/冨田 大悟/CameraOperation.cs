using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム中のカメラの制御
/// 製作者：冨田 大悟
/// </summary>
public class CameraOperation : MonoBehaviour {

    public int nam
    {
        get;
        set;
    }

    [SerializeField]
    Vector3 cameraStPos;
    [SerializeField]
    Vector3 cameraPos;
    [SerializeField]
    float cameraMoveSpeed;
    [SerializeField]
    Vector3 cameraUpPos;

    Vector3 tergetPos;

    Vector3 clearCPos = new Vector3(0,0,0);



    // Use this for initialization
    void Start()
    {
        Camera.main.transform.position = cameraStPos;
    }

    public void SetTergetPos()
    {
        tergetPos = new Vector3(cameraPos.x, cameraPos.y * nam, cameraPos.z) + cameraStPos;
    }

    // Update is called once per frame
    public void CamareMove()
    {
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, tergetPos, cameraMoveSpeed);
    }

    public bool CoordinateReaching()
    {
        return transform.position == tergetPos;
    }

    //クリア時初回処理
    public void GameClearEventCameraStart()
    {
        tergetPos = new Vector3(cameraPos.x, cameraPos.y * (nam + 1), cameraPos.z) + cameraStPos + clearCPos;

    }
    //クリア時処理
    public void GameClearEventCameraUpdate()
    {
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, tergetPos, cameraMoveSpeed);
    }
    public bool IsClearCameraPos()
    {
        return Camera.main.transform.position == tergetPos;
    }


    [ContextMenu("SetCPos")]
    public void SetGameClearPos()
    {
        Camera.main.transform.position = new Vector3(cameraPos.x, cameraPos.y * (3), cameraPos.z) + cameraStPos + clearCPos;
    }

    [ContextMenu("SetStPos")]
    public void SetGameStPos()
    {
        Camera.main.transform.position = cameraStPos;
    }

}
