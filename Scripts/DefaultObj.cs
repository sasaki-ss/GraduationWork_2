using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultObj : MonoBehaviour
{
    [SerializeField]
    private bool isOnPlayer; //プレイヤーいるフラグ

    [SerializeField]
    private GameObject floor;   //床オブジェクト

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
        Player player = GameObject.Find("Player").GetComponent<Player>();

        if (!isOnPlayer)
        {
            if (transform.position.y <= player.transform.position.y - 0.3f)
            {
                floor.GetComponent<BoxCollider2D>().enabled = true;
                isOnPlayer = true;
                Debug.Log("当たり判定を付与");
            }
        }
    }
}
