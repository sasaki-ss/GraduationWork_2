using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorObj : MonoBehaviour
{
    private bool isOnPlayer;

    private void Awake()
    {
        isOnPlayer = false;
    }

    private void Start()
    {
        if (!isOnPlayer) this.GetComponent<BoxCollider2D>().enabled = false;
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
                this.GetComponent<BoxCollider2D>().enabled = true;
                isOnPlayer = true;
                score.AddScore(player.JumpCount);
            }
        }
    }
}
