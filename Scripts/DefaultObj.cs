using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultObj : MonoBehaviour
{
    public bool isOnPlayer { get; set; }    //�v���C���[����t���O

    private GameObject  floor;  //���I�u�W�F�N�g

    private void Awake()
    {
        isOnPlayer = false;
    }

    //����������
    private void Start()
    {
        //���I�u�W�F�N�g���擾
        floor = transform.Find("StageObj_001").gameObject;
        if (!isOnPlayer) floor.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update()
    {
        //���Ƀv���C���[�����Ȃ��ꍇ
        if (!isOnPlayer)
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            Score score = GameObject.Find("Score").GetComponent<Score>();

            //�����v���C���[�������ꍇ
            if (transform.position.y <= player.transform.position.y - 0.3f)
            {
                //�����蔻���L����
                floor.GetComponent<BoxCollider2D>().enabled = true;
                isOnPlayer = true;
                score.AddScore(player.JumpCount);
            }
        }
    }
}
