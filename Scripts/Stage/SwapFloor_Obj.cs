using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapFloor_Obj : StageObject
{
    [SerializeField]
    private GameObject[] objs;

    private float changeTime;   //切り替え時間
    private float changeCnt;    //切り替え用のカウント

    private bool isLeft;    //左フラグ
    private bool isScore;

    private void Start()
    {
        changeTime = 2.5f;
        changeCnt = changeTime;
        isLeft = true;
        isScore = false;

        objs[1].SetActive(false);
        objs[3].SetActive(false);

        foreach(var obj in objs)
        {
            obj.GetComponent<BoxCollider2D>().enabled = false;
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
                foreach (GameObject obj in objs)
                {
                    obj.GetComponent<BoxCollider2D>().enabled = true;
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
                foreach (GameObject obj in objs)
                {
                    obj.GetComponent<BoxCollider2D>().enabled = false;
                }
                isOnPlayer = false;
            }
        }

        changeCnt -= Time.deltaTime;

        if(changeCnt <= 0.0f)
        {
            if (isLeft)
            {
                objs[0].SetActive(true);
                objs[2].SetActive(true);
                objs[1].SetActive(false);
                objs[3].SetActive(false);

                objs[0].GetComponent<BoxCollider2D>().enabled = false;
                objs[2].GetComponent<BoxCollider2D>().enabled = false;
                isLeft = false;
            }
            else
            {
                objs[0].SetActive(false);
                objs[2].SetActive(false);
                objs[1].SetActive(true);
                objs[3].SetActive(true);

                objs[1].GetComponent<BoxCollider2D>().enabled = false;
                objs[3].GetComponent<BoxCollider2D>().enabled = false;
                isLeft = true;
            }

            changeCnt = changeTime;
        }
    }
}
