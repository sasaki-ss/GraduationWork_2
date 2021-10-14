using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultObj : MonoBehaviour
{
    public bool isOnPlayer { get; set; }    //プレイヤーいるフラグ

    private GameObject  floor;  //床オブジェクト

    private void Awake()
    {
        isOnPlayer = false;
    }

    //初期化処理
    private void Start()
    {
        //床オブジェクトを取得
        floor = transform.Find("StageObj_001").gameObject;
        if (!isOnPlayer) floor.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update()
    {
        //床にプレイヤーがいない場合
        if (!isOnPlayer)
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            Score score = GameObject.Find("Score").GetComponent<Score>();

            //床よりプレイヤーが高い場合
            if (transform.position.y <= player.transform.position.y - 0.3f)
            {
                //当たり判定を有効化
                floor.GetComponent<BoxCollider2D>().enabled = true;
                isOnPlayer = true;
                score.AddScore(player.JumpCount);
            }
        }
    }
}
