using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isActive;            //プレイヤーのアクティブ状態

    private float speed;             //プレイヤーのスピード
    private float jumpPower;         //プレイヤーのジャンプ力

    private float limitPower;        //ジャンプの限界速度

    private GameObject[] ColliderChecks;  //床と壁の判定

    ColliderScript[] ColliderScr;    //判定オブジェクトのスクリプト格納

    private bool isDirection;        //左右の方向の切り替えフラグ
    private bool isJump;             //ジャンプフラグ

    SpriteRenderer sr;               //スプライトレンダラー
    Animator animator;               //アニメーター
    Rigidbody2D rb2d;                //リジットボディ2D

    void Start()
    {
        //初期化
        isActive = true;
        speed = 0.003f;              //速度
        jumpPower = 230.5f;          //ジャンプ力
        limitPower = jumpPower + 0.5f;      

        ColliderChecks = new GameObject[3];

        ColliderChecks[0] = GameObject.Find("GroundCheck");
        ColliderChecks[1] = GameObject.Find("WallCheck_L");
        ColliderChecks[2] = GameObject.Find("WallCheck_R");

        ColliderScr = new ColliderScript[3];

        ColliderScr[0] = ColliderChecks[0].GetComponent<ColliderScript>();
        ColliderScr[1] = ColliderChecks[1].GetComponent<ColliderScript>();
        ColliderScr[2] = ColliderChecks[2].GetComponent<ColliderScript>();

        isDirection = true;
        isJump = true;

        sr = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        //animator.SetFloat("Speed", 1);
        rb2d = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(0, -3.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDirection)    //右に移動
        {
            transform.position += new Vector3(speed, 0, 0);
            sr.flipX = false;
        }
        if (!isDirection)   //左に移動
        {
            transform.position += new Vector3(-speed, 0, 0);
            sr.flipX = true;
        }

        if (isJump && Input.GetKeyDown(KeyCode.Space))  //ジャンプ
        {
            rb2d.velocity = Vector2.zero;               //速度リセット
            rb2d.AddForce(jumpPower * Vector2.up);      //ジャンプ
            isJump = false;
        }

        if (ColliderScr[0].IsGround() == true)  //床に触れた時
        {
            isJump = true;
        }

        if (ColliderScr[1].IsGround() == true && isJump)//左の壁
        {
            isDirection = true;     //右に移動
        }

        if (ColliderScr[2].IsGround() == true && isJump)//右の壁
        {
            isDirection = false;    //左に移動
        }
    }
}