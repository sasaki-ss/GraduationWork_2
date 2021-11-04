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
    private bool highJump;           //ハイジャンプフラグ
    private bool highNow;            //ハイジャンプ中
    private int highCnt;             //ハイジャンプ中のカウント

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
    public bool HighJump
    {
        set { highJump = value; }
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
        highJump = false;

        //ハイジャンプ後のジャンプ不可用
        highNow = false;
        highCnt = JUMP_COOLDOWN;

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
        Move(); //移動
        Jump(); //ジャンプ
        TouchFloor();   //床
        TouchSideWall();//壁
        if (Input.GetKeyDown(KeyCode.A))
        {
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
        if (isJump && Input.GetKeyDown(KeyCode.Space) && highJump)//ハイジャンプ
        {
            rb2d.velocity = Vector2.zero;                       //速度リセット
            rb2d.AddForce(jumpPower * 5 * Vector2.up);          //力を加える
            highJump = false;                                   //ハイジャンプ中フラグオン
            highNow = true;                                     //
            highCnt = 0;
            speed = speed / 5;
            audioSrc.Play();                                    //効果音
            if (!countFlg) countFlg = true;
        }

        if (isJump && Input.GetKeyDown(KeyCode.Space) && !highJump && JUMP_COOLDOWN <= highCnt)//ジャンプ
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
    public void _HighJump()
    {
        rb2d.velocity = Vector2.zero;                       //速度リセット
        rb2d.AddForce(jumpPower * 5 * Vector2.up);          //力を加える
        highJump = false;                                   //ハイジャンプ中フラグオン
        highNow = true;                                     //
        highCnt = 0;
        speed = speed / 5;
        audioSrc.Play();                                    //効果音
        if (!countFlg) countFlg = true;
    }
}

