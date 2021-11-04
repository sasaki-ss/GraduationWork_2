using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishObj : StageObject
{
    private float   defaultX;       //初期座標
    private float   intervalX;      //間隔
    private bool    isScore;        //スコアフラグ

    private GameObject floorObj;    //床オブジェクト
    private GameObject wallObj;     //壁オブジェクト

    private void Start()
    {
        defaultX = -2.2169f;
        intervalX = 0.5f;
        isOnPlayer = false;
        isScore = false;

        floorObj = (GameObject)Resources.Load("StageObj_003");
        wallObj = (GameObject)Resources.Load("StageObj_002");

        for (int i = 0; i < 10; i++)
        {
            //オブジェクトを生成
            GameObject inst = (GameObject)Instantiate(floorObj,
                new Vector3(defaultX, 0f, 0f), Quaternion.Euler(0f, 0f, 90f));

            inst.transform.SetParent(this.transform, false);
            inst.tag = "Floor";

            BoxCollider2D bc = inst.GetComponent<BoxCollider2D>();

            bc.isTrigger = false;
            bc.enabled = false;

            //間隔を加算
            defaultX += intervalX;
        }

        int randNum = (int)Random.Range(0, 5);  //乱数

        //1～2の場合通常の壁を生成
        if (randNum > 0 && randNum <= 2)
        {
            GameObject wall = (GameObject)Instantiate(wallObj,
                new Vector3(2.496f, 0.509f, 0f), Quaternion.Euler(0f, 0f, 90f));
            wall.transform.SetParent(this.transform, false);

            if (randNum == 2)
            {
                GameObject wall2 = (GameObject)Instantiate(wallObj,
                    new Vector3(-2.496f, 0.509f, 0f), Quaternion.Euler(0f, 0f, 90f));
                wall2.transform.SetParent(this.transform, false);
            }
        }

        //3～4の場合消える壁を生成
        if (randNum > 2 && randNum <= 4)
        {
            GameObject wall = (GameObject)Instantiate(floorObj,
                new Vector3(2.496f, 0.74f, 0f), Quaternion.identity);
            GameObject wall2 = (GameObject)Instantiate(floorObj,
                new Vector3(2.496f, 0.278f, 0f), Quaternion.identity);

            wall.transform.SetParent(this.transform, false);
            wall2.transform.SetParent(this.transform, false);

            if (randNum == 4)
            {
                GameObject wall3 = (GameObject)Instantiate(floorObj,
                    new Vector3(-2.496f, 0.74f, 0f), Quaternion.identity);
                GameObject wall4 = (GameObject)Instantiate(floorObj,
                    new Vector3(-2.496f, 0.278f, 0f), Quaternion.identity);

                wall3.transform.SetParent(this.transform, false);
                wall4.transform.SetParent(this.transform, false);
            }
        }

    }

    private void Update()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();

        //プレイヤーが乗っていない場合
        if (!isOnPlayer)
        {
            //プレイヤーの座標が床より高い場合、床の当たり判定をオンに
            if (transform.position.y <= player.transform.position.y - 0.3f)
            {
                foreach (Transform t in gameObject.transform)
                {
                    if (t.tag == "Floor")
                    {
                        t.GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
                isOnPlayer = true;

                if (!isScore)
                {
                    Score score = GameObject.Find("Score").GetComponent<Score>();
                    score.AddScore(player.JumpCount);
                    isScore = true;
                }
            }
        }
        //プレイヤーが乗っている場合
        else
        {
            //プレイヤーの座標が床より高い場合、床の当たり判定をオフに
            if (transform.position.y >= player.transform.position.y)
            {
                foreach (Transform t in gameObject.transform)
                {
                    if (t.tag == "Floor")
                    {
                        t.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
                isOnPlayer = false;
            }
        }
    }
}
