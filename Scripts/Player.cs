using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isActive;                    //プレイヤーのアクティブ状態
        
    private float speed;                     //プレイヤーのスピード
    private const float TMP_SPEED = 0.03f;
    private float jumpPower;                 //プレイヤーのジャンプ力

    private GameObject[] ColliderChecks;     //床と壁の判定

    ColliderScript[] ColliderScr;    //判定オブジェクトのスクリプト格納

    private bool isDirection;        //左右の方向の切り替えフラグ
    private bool isJump;             //ジャンプフラグ
    private bool hJumpFlg;           //ハイジャンプフラグ
    private bool highNow;            //ハイジャンプ中
    private int highCnt;             //ハイジャンプ中のカウント

    private bool jTableFlg;         //ジャンプ台フラグ
    private Vector2 rTableForce;    //ジャンプ台威力
    private Vector2 lTableForce;    //ジャンプ台威力

    private const int JUMP_COOLDOWN = 2 * 60;

    private int jumpCount;           //ジャンプカウント
    private bool countFlg;           //カウントフラグ

    SpriteRenderer sr;               //スプライトレンダラー
    AudioSource audioSrc;               //オーディオソース
    Rigidbody2D rb2d;                //リジットボディ2D

    public int JumpCount
    {
        get { return jumpCount; }
    }
    public bool HighJumpFlg
    {
        set { hJumpFlg = value; }
    }
    public bool JunpTableFlg
    {
        set { jTableFlg = value; }
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {

        //初期化
        isActive = true;
        speed = TMP_SPEED;              //速度
        jumpPower = 230.5f;           //ジャンプ力

        ColliderChecks = new GameObject[3];

        //判定オブジェクト 地面 左壁 右壁
        ColliderChecks[0] = GameObject.Find("GroundCheck");
        ColliderChecks[1] = GameObject.Find("WallCheck_L");
        ColliderChecks[2] = GameObject.Find("WallCheck_R");

        ColliderScr = new ColliderScript[3];

        //判定オブジェクトの判定検知スクリプト
        ColliderScr[0] = ColliderChecks[0].GetComponent<ColliderScript>();
        ColliderScr[1] = ColliderChecks[1].GetComponent<ColliderScript>();
        ColliderScr[2] = ColliderChecks[2].GetComponent<ColliderScript>();

        //最初は右に走る
        isDirection = true;

        //ジャンプ可能
        isJump = true;
        hJumpFlg = false;

        //ハイジャンプ後のジャンプ不可用
        highNow = false;
        highCnt = JUMP_COOLDOWN;

        //ジャンプ台
        jTableFlg = false;
        rTableForce = new Vector2(-jumpPower, jumpPower);
        lTableForce = new Vector2(jumpPower, jumpPower);

        //ジャンプカウント
        jumpCount = 0;
        countFlg = true;

        //スプライトレンダラー
        sr = GetComponent<SpriteRenderer>();

        //オーディオソース
        audioSrc = GetComponent<AudioSource>();

        //リジットボディ2D
        rb2d = GetComponent<Rigidbody2D>();
        
        //初期座標の設定
        transform.position = new Vector3(0, -3.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (hJumpFlg) HighJump();
        if (jTableFlg) JumpTable();

        Move(); //移動
        Jump(); //ジャンプ
        TouchFloor();   //床
        TouchSideWall();//壁

        if (Input.GetKeyDown(KeyCode.A))
        {
            JumpTable();
            Debug.Log("Add" + jumpCount);
        }
    }

    private void Move()
    {
        if (isDirection)                                        //右に移動
        {
            transform.position += new Vector3(speed, 0, 0);
            sr.flipX = false;
        }
        if (!isDirection)                                       //左に移動
        {
            transform.position += new Vector3(-speed, 0, 0);
            sr.flipX = true;
        }
    }
    private void Jump()
    {
        if (isJump && Input.GetKeyDown(KeyCode.Space) && !hJumpFlg && JUMP_COOLDOWN <= highCnt)//ジャンプ
        {
            rb2d.velocity = Vector2.zero;                       //速度リセット
            rb2d.AddForce(jumpPower * Vector2.up);              //力を加える
            isJump = false;
            audioSrc.Play();                                    //効果音
            if (!countFlg) countFlg = true;
        }

        if (highNow)                                            //ハイジャンプ中
        {
            highCnt++;
        }
        if(JUMP_COOLDOWN <= highCnt)
        {
            highNow = false;
            speed = TMP_SPEED;
        }
        
    }
    private void TouchFloor()
    {
        if (ColliderScr[0].IsGround() == true)      //床に触れた時
        {
            isJump = true;
            if (rb2d.velocity == new Vector2(0, 0)) //着地中
            {
                jumpCount = 0;
                if (!countFlg) countFlg = true;
            }
            else
            {
                if (countFlg)
                {
                    jumpCount++;
                    countFlg = false;
                }
            }
        }
    }
    private void TouchSideWall()
    {

        if (ColliderScr[1].IsGround() == true)        //左の壁
        {
            isDirection = true;     //右に移動
        }

        if (ColliderScr[2].IsGround() == true)        //右の壁
        {
            isDirection = false;    //左に移動
        }

    }
    private void HighJump()
    {
        rb2d.velocity = Vector2.zero;                       //速度リセット
        rb2d.AddForce(jumpPower * 5 * Vector2.up);          //力を加える
        highCnt = 0;
        speed = speed / 5;
        audioSrc.Play();                                    //効果音
        hJumpFlg = false;                                   //ハイジャンプフラグオフ
        highNow = true;                                     //ハイジャンプ中フラグオン
        if (!countFlg) countFlg = true;
    }

    private void JumpTable()
    {
        rb2d.velocity = Vector2.zero;                               //速度リセット
        if (!isDirection) rb2d.AddForce(lTableForce);       //力を加える (左)
        else if (isDirection) rb2d.AddForce(rTableForce);   //力を加える (右)
        isJump = false;
        audioSrc.Play();                                            //効果音
        jTableFlg = false;
        if (!countFlg) countFlg = true;
    }
}

