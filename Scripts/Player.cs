using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isActive;    //�v���C���[�̃A�N�e�B�u���

    private float speed;     //�v���C���[�̃X�s�[�h
    private float jumpPower; //�v���C���[�̃W�����v��

    private GameObject[] GroundChecks;  //���̃`�F�b�N�p
    private GameObject[] WallChecks;    //�ǂ̃`�F�b�N�p

    private bool isGround;      //�n�ʂ̔���
    private bool isGroundPrev;
    private bool isWall;        //�ǂ̔���
    private bool isJump;     //�W�����v�t���O

    SpriteRenderer sr;          //�X�v���C�g�����_���[
    Animator animator;          //�A�j���[�^�[
    Rigidbody2D rb2d;           //���W�b�g�{�f�B2D

    void Start()
    {
        //������
        isActive = true;
        speed = 0.01f;
        jumpPower = 300.0f;

        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 1);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed, 0, 0);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-speed, 0, 0);
            sr.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
           rb2d.AddForce(Vector2.up * jumpPower);
        }

    }
}
