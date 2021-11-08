using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloorObj : StageObject
{
    GameObject floor;
    [SerializeField]
    float moveSpeed;
    bool isScore;

    private void Start()
    {
        moveSpeed = 0.05f;
        isOnPlayer = false;
        isScore = false;
        floor = transform.Find("StageObj_005").gameObject;
        floor.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();

        #region “–‚½‚è”»’èŠÖ˜A
        if (!isOnPlayer)
        {
            if (transform.position.y <= player.transform.position.y - 0.3f)
            {
                floor.GetComponent<BoxCollider2D>().enabled = true;
                isOnPlayer = true;

                if (!isScore)
                {
                    Score score = GameObject.Find("Score").GetComponent<Score>();
                    score.AddScore(player.JumpCount);
                    isScore = true;
                }
            }
        }
        else
        {
            if (transform.position.y >= player.transform.position.y)
            {
                floor.GetComponent<BoxCollider2D>().enabled = false;
                isOnPlayer = false;
            }
        }
        #endregion

        if(floor.transform.position.x <= -1.92f)
        {
            moveSpeed = 0.05f;
        }
        else if(floor.transform.position.x >= 1.92f)
        {
            moveSpeed = -0.05f;
        }

        floor.transform.position += new Vector3(moveSpeed, 0, 0);
    }
}
