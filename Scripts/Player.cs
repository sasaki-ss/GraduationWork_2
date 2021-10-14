using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isActive;                    //�v���C���[�̃A�N�e�B�u���
        
    private float speed;                     //�v���C���[�̃X�s�[�h
    private float jumpPower;                 //�v���C���[�̃W�����v��

    private GameObject[] ColliderChecks;     //���ƕǂ̔���

    ColliderScript[] ColliderScr;    //����I�u�W�F�N�g�̃X�N���v�g�i�[

    private bool isDirection;        //���E�̕����̐؂�ւ��t���O
    private bool isJump;             //�W�����v�t���O

    private int jumpCount;           //�W�����v�J�E���g
    private bool countFlg;           //�J�E���g�t���O

    SpriteRenderer sr;               //�X�v���C�g�����_���[
    AudioSource audioSrc;               //�I�[�f�B�I�\�[�X
    Rigidbody2D rb2d;                //���W�b�g�{�f�B2D

    public int JumpCount
    {
        get { return jumpCount; }
    }

    void Start()
    {
        //������
        isActive = true;
        speed = 0.0035f;              //���x
        jumpPower = 230.5f;           //�W�����v��

        ColliderChecks = new GameObject[3];

        //����I�u�W�F�N�g �n�� ���� �E��
        ColliderChecks[0] = GameObject.Find("GroundCheck");
        ColliderChecks[1] = GameObject.Find("WallCheck_L");
        ColliderChecks[2] = GameObject.Find("WallCheck_R");

        ColliderScr = new ColliderScript[3];

        //����I�u�W�F�N�g�̔��茟�m�X�N���v�g
        ColliderScr[0] = ColliderChecks[0].GetComponent<ColliderScript>();
        ColliderScr[1] = ColliderChecks[1].GetComponent<ColliderScript>();
        ColliderScr[2] = ColliderChecks[2].GetComponent<ColliderScript>();

        //�ŏ��͉E�ɑ���
        isDirection = true;

        //�W�����v�\
        isJump = true;

        //�W�����v�J�E���g
        jumpCount = 0;
        countFlg = true;

        //�X�v���C�g�����_���[
        sr = GetComponent<SpriteRenderer>();

        //�I�[�f�B�I�\�[�X
        audioSrc = GetComponent<AudioSource>();

        //���W�b�g�{�f�B2D
        rb2d = GetComponent<Rigidbody2D>();
        
        //�������W�̐ݒ�
        transform.position = new Vector3(0, -3.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDirection)                                        //�E�Ɉړ�
        {
            transform.position += new Vector3(speed, 0, 0);
            sr.flipX = false;
        }
        if (!isDirection)                                       //���Ɉړ�
        {
            transform.position += new Vector3(-speed, 0, 0);
            sr.flipX = true;
        }

        if (isJump && Input.GetKeyDown(KeyCode.Space))          //�W�����v
        {
            rb2d.velocity = Vector2.zero;                       //���x���Z�b�g
            rb2d.AddForce(jumpPower * Vector2.up);              //�͂�������
            isJump = false;
            audioSrc.Play();                                    //���ʉ�
            if (!countFlg) countFlg = true;
        }

        if (ColliderScr[0].IsGround() == true)                  //���ɐG�ꂽ��
        {
            isJump = true;
            if(rb2d.velocity == new Vector2(0, 0))              //���n��
            {
                jumpCount = 0;
                if(!countFlg) countFlg = true;
            }
            else
            {
                if (countFlg)
                {
                    jumpCount++;
                    countFlg = false;
                }
            }
        }

        if (ColliderScr[1].IsGround() == true && isJump)        //���̕�
        {
            isDirection = true;     //�E�Ɉړ�
        }

        if (ColliderScr[2].IsGround() == true && isJump)        //�E�̕�
        {
            isDirection = false;    //���Ɉړ�
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Add" + jumpCount);
        }
    }
}