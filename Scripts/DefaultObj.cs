using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultObj : MonoBehaviour
{
    [SerializeField]
    private bool isOnPlayer; //�v���C���[����t���O

    [SerializeField]
    private GameObject floor;   //���I�u�W�F�N�g

    //����������
    private void Start()
    {
        //���I�u�W�F�N�g���擾
        floor = transform.Find("StageObj_001").gameObject;
        floor.GetComponent<BoxCollider2D>().enabled = false;
        isOnPlayer = false;
    }

    private void Update()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();

        if (!isOnPlayer)
        {
            if (transform.position.y <= player.transform.position.y - 0.3f)
            {
                floor.GetComponent<BoxCollider2D>().enabled = true;
                isOnPlayer = true;
                Debug.Log("�����蔻���t�^");
            }
        }
    }
}
