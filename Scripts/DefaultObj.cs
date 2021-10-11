using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultObj : MonoBehaviour
{
    private bool        isOnPlayer; //プレイヤーいるフラグ
    private GameObject  floor;      //床オブジェクト

    //初期化処理
    private void Start()
    {
        //床オブジェクトを取得
        floor = transform.Find("StageObj_001").gameObject;
        floor.GetComponent<BoxCollider2D>().enabled = false;
        isOnPlayer = false;
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
                Debug.Log("当たり判定を付与");
            }
        }
    }
}
