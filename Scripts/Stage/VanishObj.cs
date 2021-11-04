using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishObj : StageObject
{
    private float   defaultX;       //�������W
    private float   intervalX;      //�Ԋu
    private bool    isScore;        //�X�R�A�t���O

    private GameObject floorObj;    //���I�u�W�F�N�g
    private GameObject wallObj;     //�ǃI�u�W�F�N�g

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
            //�I�u�W�F�N�g�𐶐�
            GameObject inst = (GameObject)Instantiate(floorObj,
                new Vector3(defaultX, 0f, 0f), Quaternion.Euler(0f, 0f, 90f));

            inst.transform.SetParent(this.transform, false);
            inst.tag = "Floor";

            BoxCollider2D bc = inst.GetComponent<BoxCollider2D>();

            bc.isTrigger = false;
            bc.enabled = false;

            //�Ԋu�����Z
            defaultX += intervalX;
        }

        int randNum = (int)Random.Range(0, 5);  //����

        //1�`2�̏ꍇ�ʏ�̕ǂ𐶐�
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

        //3�`4�̏ꍇ������ǂ𐶐�
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

        //�v���C���[������Ă��Ȃ��ꍇ
        if (!isOnPlayer)
        {
            //�v���C���[�̍��W������荂���ꍇ�A���̓����蔻����I����
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
        //�v���C���[������Ă���ꍇ
        else
        {
            //�v���C���[�̍��W������荂���ꍇ�A���̓����蔻����I�t��
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
