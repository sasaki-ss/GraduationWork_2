using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapFloor_Obj : StageObject
{
    [SerializeField]
    private GameObject[] objs;

    private float changeTime;   //�؂�ւ�����
    private float changeCnt;    //�؂�ւ��p�̃J�E���g

    private bool isLeft;    //���t���O
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

        //�v���C���[������Ă��Ȃ��ꍇ
        if (!isOnPlayer)
        {
            //�v���C���[�̍��W������荂���ꍇ�A���̓����蔻����I����
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
        //�v���C���[������Ă���ꍇ
        else
        {
            //�v���C���[�̍��W������荂���ꍇ�A���̓����蔻����I�t��
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
