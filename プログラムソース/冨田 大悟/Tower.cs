using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 左右の壁の制御する
/// 製作者：冨田 大悟
/// </summary>
public class Tower : MonoBehaviour {

    [SerializeField]//生成　のち　本体
     Wall leftWall;
    [SerializeField]
    Wall rightWall;

    float leftEnd;
    float rightEnd;

    int towerCount;

    public float time = 0.01f;

    public bool end { set; get; }

    [SerializeField]
    int endTime = (int)(1250 * 0.8f);

    public enum HitBlock
    {
        RIGHTEND,LEFTEND
    }
    public HitBlock hitBlock
    {
        get;
        private set;
    }

    public float spacing = 20;

    [SerializeField]
    AudioClip batan;

    [SerializeField]
    bool playerDiedRange;
    [SerializeField]
    string floorObjName = "FloorObj";
    GameObject floorObj;

    List<BlockObj> blockChexk;


    [ContextMenu("WallSet")]
    public void WallSet() {
        Debug.Log("ウォールセット");
        leftWall = Instantiate(leftWall, new Vector3(-spacing,this.transform.position.y,0), leftWall.transform.rotation, this.transform);
        rightWall = Instantiate(rightWall, new Vector3(spacing, transform.position.y, 0), rightWall.transform.rotation, this.transform);
    }


    private void Start()
    {
        rightEnd = leftWall.transform.localScale.x/2;
        leftEnd = -rightEnd;
        towerCount = 0;
        end = false;
        floorObj = GameObject.Find(floorObjName);
    }

    /// <summary>
    /// タワーの更新
    /// </summary>
    public void TowerUpdate()
    {
        TowerMove();
        PlayerHit();
        SpeedUpPos();
        MoveEnd();
        CheckBlockObjIns();

        towerCount++;
    }

    /// <summary>
    /// 壁の移動
    /// </summary>
    void TowerMove()
    {
        leftWall.transform.position = Vector3.MoveTowards(leftWall.transform.position, new Vector3(leftEnd, transform.position.y, 0), time);
        rightWall.transform.position = Vector3.MoveTowards(rightWall.transform.position, new Vector3(rightEnd, transform.position.y, 0), time);
    }
 
    /// <summary>
    /// プレイヤーがクリア位置に到達判定
    /// </summary>
    void PlayerHit()
    {
        if (leftWall.GetComponent<Wall>().playerHit)
        {
            time = 1;
            hitBlock = HitBlock.LEFTEND;
        }
        else if (rightWall.GetComponent<Wall>().playerHit)
        {
            time = 1;
            hitBlock = HitBlock.RIGHTEND;
        }

        GameObject player = Player.player.gameObject;
        if (MyPhysics.ObjectIns(player, rightWall.gameObject) || MyPhysics.ObjectIns(player, leftWall.gameObject))
        {
            time = 1;
        }
    }

    /// <summary>
    /// タワーの加速の制御
    /// </summary>
    void SpeedUpPos()
    {
        if (towerCount > endTime && Player. player.transform.position.y < this.transform.position.y)
        {
            time = 1;
        }
        if (BlockObj.playerDeid){
            time = 1;
        }
    }

    /// <summary>
    /// タワーが閉じたか判定
    /// </summary>
    void MoveEnd()
    {
        end = (leftWall.transform.position.x == leftEnd);
        if (end)
        {
            leftWall.transform.tag = "endWall";
            rightWall.transform.tag = "endWall";
            leftWall.gameObject.layer = LayerMask.NameToLayer( "endWall");
            rightWall.gameObject.layer = LayerMask.NameToLayer("endWall");

            SEMaster.Play(batan);
        }
    }

    /// <summary>
    /// 壁に氷が当たった時の処理
    /// </summary>
    void CheckBlockObjIns()
    {
        foreach (BlockObj tr in floorObj.GetComponentsInChildren<BlockObj>())
        {
      
            HitBlockObj(tr, leftWall.gameObject,false);
            HitBlockObj(tr, rightWall.gameObject,true);

            GameObject go = tr.gameObject;
            if(floorObj != go)
            {
                if (MyPhysics.ObjectIns(go, rightWall.gameObject) || MyPhysics.ObjectIns(go, leftWall.gameObject))
                {
                    Debug.Log("デストロイ："+gameObject.name);
                    Destroy(go);
                }
            }
        }
    }

    /// <summary>
    /// 氷が壁に当たった時の処理
    /// </summary>
    /// <param name="bo">判定するブロック</param>
    /// <param name="wallGo">壁</param>
    /// <param name="lefthit">左右の判定</param>
    void HitBlockObj(BlockObj bo,GameObject wallGo,bool lefthit)
    {
        float x;
        if (lefthit)
        {
            x = wallGo.transform.position.x - wallGo.transform.localScale.x / 2;
        }else
        {
            x = wallGo.transform.position.x + wallGo.transform.localScale.x / 2;

        }
        float gx1, gx2;
        gx1 = bo.transform.position.x - bo.transform.localScale.x/2;
        gx2 = bo.transform.position.x + bo.transform.localScale.x/2;
        if(gx1 < x &&x < gx2){
            bo.CreatEfect(x);
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDiedRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDiedRange = false;
        }
    }

    /// <summary>
    /// プレイヤーが死亡位置にいるか
    /// </summary>
    /// <returns>trueならいる</returns>
    public bool IsInPlayerDiedRange()
    {
        return playerDiedRange;
    }

    /// <summary>
    /// 終了する速度になっているか
    /// </summary>
    /// <returns>trueなら終了速度</returns>
    public bool IsDeadSpeed()
    {
        return time == 1;
    }
}
