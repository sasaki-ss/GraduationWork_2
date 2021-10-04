using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isActive;    //プレイヤーのアクティブ状態

    private float speed;     //プレイヤーのスピード
    private float jumpPower; //プレイヤーのジャンプ力

    private GameObject[] GroundChecks;  //床のチェック用
    private GameObject[] WallChecks;    //壁のチェック用

    private bool isGround;      //地面の判定
    private bool isGroundPrev;
    private bool isWall;        //壁の判定
    private bool isJump;     //ジャンプフラグ

    SpriteRenderer sr;          //スプライトレンダラー
    Animator animator;          //アニメーター
    Rigidbody2D rb2d;           //リジットボディ2D

    void Start()
    {
        //初期化
        isActive = true;
        speed = 0.01f;
        jumpPower = 300.0f;

        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 1);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed, 0, 0);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-speed, 0, 0);
            sr.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
           rb2d.AddForce(Vector2.up * jumpPower);
        }

    }
}
