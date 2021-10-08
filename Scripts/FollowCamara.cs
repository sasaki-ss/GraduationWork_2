using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamara : MonoBehaviour
{
    [SerializeField]
    private Camera cameraObj;
    private GameObject player;      //�v���C���[
    private Transform playerTf;     //�v���C���[�g�����X�t�H�[��
    private Transform cameraTf;     //�J�������W
    private Vector3 keepVec;        //�v���C���[���W�̕ێ�
    public float bottomY { get; private set; }

    private void Awake()
    {
        cameraObj = this.GetComponent<Camera>();
        player = GameObject.Find("Player");
        playerTf = player.transform;
        cameraTf = playerTf;
    }

    private void Start()
    {
        bottomY = transform.position.y - 5f;
        keepVec = playerTf.position;
        keepVec.y = keepVec.y - 0.03924f;   //�v���C���[�̈�ԉ��̒i�̎��̍��W�ɍ��킹�Ă���
    }
    private void LateUpdate()
    {
        bottomY = transform.position.y - 5f;

        if (keepVec.y < playerTf.position.y)    //��x�J�������㏸�����牺����Ȃ��悤�ɂ���
        {
            transform.position = new Vector3(transform.position.x, cameraTf.position.y + 2.5f, transform.position.z);
            keepVec.y = playerTf.position.y;

        }

        //if (Input.GetKey(KeyCode.A))  //���W��\��
        //{
        //    Debug.Log("keep" + keepVec.y);
        //    Debug.Log("player" + playerTf.position.y);
        //}

    }
}
