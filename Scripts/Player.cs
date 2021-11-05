using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isActive;                    //�v���C���[�̃A�N�e�B�u���
        
    private float speed;                     //�v���C���[�̃X�s�[�h
    private const float TMP_SPEED = 0.03f;
    private float jumpPower;                 //�v���C���[�̃W�����v��

    private GameObject[] ColliderChecks;     //���ƕǂ̔���

    ColliderScript[] ColliderScr;    //����I�u�W�F�N�g�̃X�N���v�g�i�[

    private bool isDirection;        //���E�̕����̐؂�ւ��t���O
    private bool isJump;             //�W�����v�t���O
    private bool hJumpFlg;           //�n�C�W�����v�t���O
    private bool highNow;            //�n�C�W�����v��
    private int highCnt;             //�n�C�W�����v���̃J�E���g

    private bool jTableFlg;         //�W�����v��t���O
    private Vector2 rTableForce;    //�W�����v��З�
    private Vector2 lTableForce;    //�W�����v��З�

    private const int JUMP_COOLDOWN = 2 * 60;

    private int jumpCount;           //�W�����v�J�E���g
    private bool countFlg;           //�J�E���g�t���O

    SpriteRenderer sr;               //�X�v���C�g�����_���[
    AudioSource audioSrc;               //�I�[�f�B�I�\�[�X
    Rigidbody2D rb2d;                //���W�b�g�{�f�B2D

    public int JumpCount
    {
        get { return jumpCount; }
    }
    public bool HighJumpFlg
    {
        set { hJumpFlg = value; }
    }
    public bool JunpTableFlg
    {
        set { jTableFlg = value; }
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {

        //������
        isActive = true;
        speed = TMP_SPEED;              //���x
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
        hJumpFlg = false;

        //�n�C�W�����v��̃W�����v�s�p
        highNow = false;
        highCnt = JUMP_COOLDOWN;

        //�W�����v��
        jTableFlg = false;
        rTableForce = new Vector2(-jumpPower, jumpPower);
        lTableForce = new Vector2(jumpPower, jumpPower);

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
        if (hJumpFlg) HighJump();
        if (jTableFlg) JumpTable();

        Move(); //�ړ�
        Jump(); //�W�����v
        TouchFloor();   //��
        TouchSideWall();//��

        if (Input.GetKeyDown(KeyCode.A))
        {
            JumpTable();
            Debug.Log("Add" + jumpCount);
        }
    }

    private void Move()
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
    }
    private void Jump()
    {
        if (isJump && Input.GetKeyDown(KeyCode.Space) && !hJumpFlg && JUMP_COOLDOWN <= highCnt)//�W�����v
        {
            rb2d.velocity = Vector2.zero;                       //���x���Z�b�g
            rb2d.AddForce(jumpPower * Vector2.up);              //�͂�������
            isJump = false;
            audioSrc.Play();                                    //���ʉ�
            if (!countFlg) countFlg = true;
        }

        if (highNow)                                            //�n�C�W�����v��
        {
            highCnt++;
        }
        if(JUMP_COOLDOWN <= highCnt)
        {
            highNow = false;
            speed = TMP_SPEED;
        }
        
    }
    private void TouchFloor()
    {
        if (ColliderScr[0].IsGround() == true)      //���ɐG�ꂽ��
        {
            isJump = true;
            if (rb2d.velocity == new Vector2(0, 0)) //���n��
            {
                jumpCount = 0;
                if (!countFlg) countFlg = true;
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
    }
    private void TouchSideWall()
    {

        if (ColliderScr[1].IsGround() == true)        //���̕�
        {
            isDirection = true;     //�E�Ɉړ�
        }

        if (ColliderScr[2].IsGround() == true)        //�E�̕�
        {
            isDirection = false;    //���Ɉړ�
        }

    }
    private void HighJump()
    {
        rb2d.velocity = Vector2.zero;                       //���x���Z�b�g
        rb2d.AddForce(jumpPower * 5 * Vector2.up);          //�͂�������
        highCnt = 0;
        speed = speed / 5;
        audioSrc.Play();                                    //���ʉ�
        hJumpFlg = false;                                   //�n�C�W�����v�t���O�I�t
        highNow = true;                                     //�n�C�W�����v���t���O�I��
        if (!countFlg) countFlg = true;
    }

    private void JumpTable()
    {
        rb2d.velocity = Vector2.zero;                               //���x���Z�b�g
        if (!isDirection) rb2d.AddForce(lTableForce);       //�͂������� (��)
        else if (isDirection) rb2d.AddForce(rTableForce);   //�͂������� (�E)
        isJump = false;
        audioSrc.Play();                                            //���ʉ�
        jTableFlg = false;
        if (!countFlg) countFlg = true;
    }
}

