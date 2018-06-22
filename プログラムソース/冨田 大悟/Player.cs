using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// プレイヤーを制御する
/// 製作者：冨田 大悟
/// </summary>
[SelectionBase]
[DefaultExecutionOrder(-2)]
public class Player : MonoBehaviour {

    public static Player player;
    public int dir {
        get;
       private set;
    }

    [SerializeField]
    float moveSpeed = 0.1f;

    [SerializeField]
    private float xRaySize = 0.5f;//移動制限距離
    [SerializeField]
    float playerXSize;

    [SerializeField,Header("Jump")]
    private float YRaySize = 0.1f;

    [SerializeField]
    float jumpPower = 17f;
    bool jumpOn;
    [SerializeField]
    int jumpTime = 5;
    int jumpCount;

    [SerializeField]
    int landingWaitTime = 10;
    int landingWaitCount;

    bool grund;

    public bool life {
        private set;
        get;
    }

    [SerializeField, Header("Damage")]
    PlayerDamage pDamage;

    //プレイヤー中心
     public Vector3 playerCenter {
        get;
        private set;
    }

    //撃つ球
    [SerializeField,Header("Shot")]
    PlayerShot freezeShotPrfab;

    //撃った球
    PlayerShot playerShot;
    bool freezeExist;

    [SerializeField]
    float shotPos = 1.5f;
    [SerializeField]
    Vector3 shotCenterPos=new Vector3(0,1.1f,0);
    [SerializeField]
    int shotRigorTime;
    int shotRigorCount;

    [SerializeField,Header("Component")]
    PhysicMaterial pm;

    [SerializeField]
    GameObject colliderBaseObj;
    [SerializeField]
    Vector3 BoxRayScale;

    Vector3 colliderCenterPos;

    //コンポーネント
    Rigidbody rigidBody;

    Vector3 bPos;//前座標



    //エフェクト
    [SerializeField,Header("Effect")]
    OldEffect moveEffect;
    [SerializeField]
    GameObject deadEffect;


    //アニメ　NULL判定あり
    [SerializeField,Header("animes")]
    PlayerAnimation animes;
    [SerializeField]
    int idelTime;
    int idelCount;
    [SerializeField]
    float idelMoveSize;

    //サウンド
    [SerializeField,Header("Sound")]
    AudioClip jumpSound;
    [SerializeField]
    AudioClip shotSound;

    [SerializeField, Header("クリア用")]
    Vector3 clearPos = new Vector3(0,38,0);
    public bool controlOn = false;

    private void Awake()
    {
        player = this;

    }

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        pDamage.SetAnimes(animes);

        life = true;
        jumpOn = false;
        dir = 1;
        freezeExist = false;
        shotRigorCount = 0;
        moveEffect.Stop();

        //d
       // mst = meshr.material;
    }

    private void FixedUpdate()
    {
        SetColliderCenter();
        grund = IsAir();
        //anderPPos.transform.position = playerCenter;

        if (controlOn && life)
        {
            pDamage.Invicile();
            DamageCheck();

            if (pDamage.confusionCount > 0)
            {
                pDamage.confusionCount--;
            }
            
                Move();
                Jump();

                FreezeShoot();
            
            //Die();
        }

        //立ち止まり判定
        if (bPos == transform.position) 
        {
            moveEffect.Stop();
            if (controlOn)
            {
                idelCount++;
            }
        }else
        {
            moveEffect.Play();
            Vector3 movesize = bPos - transform.position;
            float movesizeP = Mathf.Abs (movesize.x) + Mathf.Abs(movesize.y)+ Mathf.Abs(movesize.z);
            if (movesizeP > idelMoveSize)
            {
                idelCount = 0;
            }
        }
        animes.IdleAnimation(idelCount> idelTime);



        if (playerShot == null)
        {
            freezeExist = false;
        }
        if(shotRigorCount >0)
        {
            shotRigorCount--;
        }

        //前座標更新
        bPos = transform.position;

    }

    /// <summary>
    /// プレイヤーの移動処理
    /// </summary>
    void Move()
    {
        if (shotRigorCount == 0)
        {
            bool runFlag = false;
            if (KeyLoader.keyLoader.joyStickAxis.x != 0 && pDamage.confusionCount == 0)
            {
                dir = KeyLoader.keyLoader.joyStickAxis.x > 0 ? 1 : -1; //向き更新
                runFlag = true;//走る

                if (IsCanCove())
                {
                    transform.position += new Vector3(moveSpeed * dir, 0, 0); //移動
                    //rigidBody.velocity = new Vector3(moveSpeed * dir, rigidBody.velocity. y, 0);
                }else
                {
                    Debug.Log("not move");
                    rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
                    
                }
            }
            else
            {
                //RunFlagsをtrue
                runFlag = false;                                        //走らない
                rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
            }
            if (dir == 1)
            {
                this.transform.rotation = Quaternion.Euler(0, 90, 0); //向き更新
            }else
            {
                this.transform.rotation = Quaternion.Euler(0, -90, 0); //向き更新

            }
            if (animes != null) animes.RunAnimation(runFlag);
        }
        else
        {
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);

        }
    }

    /// <summary>
    /// プレイヤーが移動できるかどうかの判定
    /// </summary>
    /// <returns>trueなら移動できる</returns>
    bool IsCanCove()
    {

        RaycastHit hit;
        if (Physics.BoxCast(playerCenter, BoxRayScale, new Vector3(dir, 0, 0), out hit, new Quaternion(0, 0, 0, 0), xRaySize, LayerName.GetObjectHitLayer(), QueryTriggerInteraction.Ignore))
        {
            Vector3 hitpos = hit.point;
            player.transform.position = new Vector3(hitpos.x + (playerXSize * -dir), transform.position.y, transform.position.z);
            return true;
        }

        //第二
        if (Physics.CheckBox(playerCenter, BoxRayScale, new Quaternion(0, 0, 0, 0), LayerName.GetObjectHitLayer(), QueryTriggerInteraction.Ignore)) 
        {
            return true;

        }

        return true;
    }

    /// <summary>
    /// プレイヤーのジャンプ処理
    /// </summary>
    void Jump()
    {

        if(landingWaitCount > 0)
        {
            landingWaitCount--;
        }

        if (jumpOn)
        {
            if (jumpCount > 0)
            {
                jumpCount--;

            }else if (grund)
            {
                Debug.Log("着地");
                landingWaitCount = landingWaitTime;
                jumpOn = false;
            }
        }  else  if (KeyLoader.keyLoader.Jump && pDamage.confusionCount == 0 && landingWaitCount == 0)
        {

            if(grund){
                rigidBody.velocity = new Vector3(0, jumpPower, 0);
                jumpOn = true;
                Debug.Log("ジャンプ");
                jumpCount = jumpTime;
                
                SEMaster.Play(jumpSound);
            }
        }
       
        if (animes != null) animes.JumpAnimation(jumpOn);

    }

    virtual protected void OnValidate()
    {
        SetColliderCenter();
    }

    /// <summary>
    /// プレイヤーのコライダー座標の調整
    /// </summary>
    protected void SetColliderCenter()
    {
        //debug
        if (colliderBaseObj != null)
        {
           // colliderCenterPos = new Vector3(colliderBaseObj.transform.localPosition.x, colliderBaseObj.transform.localPosition.y) + colliderCenterAddPos;
            colliderCenterPos = new Vector3(0, colliderBaseObj.transform.localPosition.y);

            playerCenter = transform.position + colliderCenterPos/2;

        }
    }

    /// <summary>
    /// プレーヤーが空中にいるか
    /// </summary>
    /// <returns>trueなら空中</returns>
    bool IsAir()
    {
        int layer = LayerName.GetObjectHitLayer() ;

        if(Physics.BoxCast(playerCenter, BoxRayScale, Vector3.down,new Quaternion(0, 0, 0, 0), YRaySize, layer, QueryTriggerInteraction.Ignore))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// プレイヤーの玉を撃つ処理
    /// </summary>
    void FreezeShoot()
    {
        if (!freezeExist && KeyLoader.keyLoader.Magic)
        {
            ShotDecision(freezeShotPrfab);
        }
    }

    /// <summary>
    /// 球の生成位置調整
    /// </summary>
    /// <param name="go">生成する球のプレハブ</param>
    void ShotDecision(PlayerShot go)
    {
        if(KeyLoader.keyLoader.joyStickAxis.y != 0 || KeyLoader.keyLoader.joyStickAxis.x != 0)
        {
            if (Mathf.Abs(KeyLoader.keyLoader.joyStickAxis.y) > Mathf.Abs(KeyLoader.keyLoader.joyStickAxis.x))
            {
                if (KeyLoader.keyLoader.joyStickAxis.y > 0)//上に打つ
                {
                    if (jumpOn)
                    {
                        //ジャンプうち
                    }
                    else if (KeyLoader.keyLoader.joyStickAxis.x != 0)
                    {
                        //移動しながらに打つ
                        shotRigorCount = shotRigorTime;
                        animes.RunAnimation(false);
                    }
                    else
                    {
                        //普通に打つ
                        shotRigorCount = shotRigorTime;

                    }
                    animes.UpMagicAnimation();
                    CreateShot(go, Vector3.up);
                }
                else if (KeyLoader.keyLoader.joyStickAxis.y < 0 && !grund) //下に打つ
                {
                    animes.UnderMagicAnimation();
                    Debug.Log("下魔法");
                    CreateShot(go, Vector3.down);


                }

            }
            else
            {
                animes.CrossMagicAnimation();
                CreateShot(go, new Vector3(dir, 0));

            }
        }else
        {
            animes.CrossMagicAnimation();
            CreateShot(go, new Vector3(dir, 0));
        }

    }

    /// <summary>
    /// 球の生成
    /// </summary>
    /// <param name="go">生成する球のプレハブ</param>
    /// <param name="shotMoveDir">球の移動方向</param>
    void CreateShot(PlayerShot go,Vector3 shotMoveDir)
    {
        playerShot = Instantiate(go);
        freezeExist = true;
        playerShot.transform.position = shotCenterPos + transform.position + (shotMoveDir * shotPos);
        playerShot.moves = shotMoveDir;
        SEMaster.Play(shotSound);
    }
    
    /// <summary>
    /// クリアした時に一度実行される処理
    /// </summary>
    public void ClearMoveStart()
    {
        pDamage.DamageCansel();
    }

    /// <summary>
    /// クリア時の移動処理
    /// </summary>
    public void ToClareMovePos()
    {

        if(Mathf.Abs( clearPos.x - transform.position.x) < 0.2)
        {

            transform.position = new Vector3(clearPos.x,transform.position.y,transform.position.z);
            //立ち止まる処理
            if (animes != null)
            {
                transform.eulerAngles = new Vector3(0,90, 0);
                animes.RunAnimation(false);
                animes.ClearAnimation(true);
            }
        }
        else
        {
            dir = clearPos.x - transform.position.x > 0 ? 1 : -1; //向き更新

            clearPos.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, clearPos, moveSpeed);
            this.transform.rotation = Quaternion.Euler(0, 90 * dir, 0); //向き更新
            if (animes != null) animes.RunAnimation(true);
        }

        if (jumpOn)
        {
            if (jumpCount > 0)
            {
                jumpCount--;

            }
            else if (!IsAir())
            {
                Debug.Log("着地");
                landingWaitCount = landingWaitTime;
                jumpOn = false;
                animes.JumpAnimation(false);

            }
        }
    }

    /// <summary>
    /// クリアした時に目的地に到着したかどうか
    /// </summary>
    /// <returns>trueなら到着した</returns>
    public bool IsClareMovePos()
    {
        if(clearPos.x == transform.position.x&& GetComponent<Rigidbody>().velocity.y >= -0.5f)
        {
            transform.position = new Vector3(clearPos.x, transform.position.y, transform.position.z);
            GetComponent<Rigidbody>().isKinematic = true;
            moveEffect.Stop();
            return true;
        }
        return false;
    }

    /// <summary>
    /// プレイヤーがダメージを受けるかチェックする
    /// </summary>

    public void DamageCheck()
    {
        if (this.CompareTag("Player"))
        {
            Collider[] collid;
            collid = Physics.OverlapBox(playerCenter, BoxRayScale, new Quaternion(0, 0, 0, 0), LayerMask.GetMask(LayerName.ENEMY), QueryTriggerInteraction.Ignore);

            foreach (Collider col in collid)
            {
                col.GetComponent<Enemy>().PlayerHit();
                pDamage.Damage();
                break;
            }
        }
    }

    /// <summary>
    /// 死亡時のエフェクトを生成する
    /// </summary>
    public void CreatDeadEffect()
    {
        Instantiate(deadEffect, playerCenter, deadEffect.transform.rotation);
    }

    /// <summary>
    /// プレイヤーが挟まれているかどうか
    /// </summary>
    /// <returns>trueなら挟まれている</returns>
    public bool PlayerSandwiched()
    {
        bool dead = false;
        Collider[] hits = Physics.OverlapBox(playerCenter,BoxRayScale,new Quaternion(0, 0, 0, 0), LayerMask.GetMask(LayerName.BLOCK, LayerName.WALL), QueryTriggerInteraction.Ignore);
        if (hits.Any(x=>x.gameObject.CompareTag("blockObj") && hits.Any(y => y.gameObject.CompareTag("wall")))){
            dead = true;
        }
        return dead;
    }
}
