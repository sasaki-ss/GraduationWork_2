using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isActive;            //�v���C���[�̃A�N�e�B�u���

    private float speed;             //�v���C���[�̃X�s�[�h
    private float jumpPower;         //�v���C���[�̃W�����v��

    private float limitPower;        //�W�����v�̌��E���x

    private GameObject[] ColliderChecks;  //���ƕǂ̔���

    ColliderScript[] ColliderScr;    //����I�u�W�F�N�g�̃X�N���v�g�i�[

    private bool isDirection;        //���E�̕����̐؂�ւ��t���O
    private bool isJump;             //�W�����v�t���O

    SpriteRenderer sr;               //�X�v���C�g�����_���[
    Animator animator;               //�A�j���[�^�[
    Rigidbody2D rb2d;                //���W�b�g�{�f�B2D

    void Start()
    {
        //������
        isActive = true;
        speed = 0.003f;              //���x
        jumpPower = 230.5f;          //�W�����v��
        limitPower = jumpPower + 0.5f;      

        ColliderChecks = new GameObject[3];

        ColliderChecks[0] = GameObject.Find("GroundCheck");
        ColliderChecks[1] = GameObject.Find("WallCheck_L");
        ColliderChecks[2] = GameObject.Find("WallCheck_R");

        ColliderScr = new ColliderScript[3];

        ColliderScr[0] = ColliderChecks[0].GetComponent<ColliderScript>();
        ColliderScr[1] = ColliderChecks[1].GetComponent<ColliderScript>();
        ColliderScr[2] = ColliderChecks[2].GetComponent<ColliderScript>();

        isDirection = true;
        isJump = true;

        sr = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        //animator.SetFloat("Speed", 1);
        rb2d = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(0, -3.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDirection)    //�E�Ɉړ�
        {
            transform.position += new Vector3(speed, 0, 0);
            sr.flipX = false;
        }
        if (!isDirection)   //���Ɉړ�
        {
            transform.position += new Vector3(-speed, 0, 0);
            sr.flipX = true;
        }

        if (isJump && Input.GetKeyDown(KeyCode.Space))  //�W�����v
        {
            rb2d.velocity = Vector2.zero;               //���x���Z�b�g
            rb2d.AddForce(jumpPower * Vector2.up);      //�W�����v
            isJump = false;
        }

        if (ColliderScr[0].IsGround() == true)  //���ɐG�ꂽ��
        {
            isJump = true;
        }

        if (ColliderScr[1].IsGround() == true && isJump)//���̕�
        {
            isDirection = true;     //�E�Ɉړ�
        }

        if (ColliderScr[2].IsGround() == true && isJump)//�E�̕�
        {
            isDirection = false;    //���Ɉړ�
        }
    }
}