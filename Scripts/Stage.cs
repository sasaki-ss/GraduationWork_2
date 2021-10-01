using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obj;   //�}�b�v�I�u�W�F�N�g
    
    private float defaultY;     //�������W
    private float intervalY;    //�}�b�v�I�u�W�F�N�g�̊Ԋu

    //����������
    private void Start()
    {
        //�z����m��
        obj = new GameObject[Define.MAP_OBJECT_NUM];

        //private�ϐ��̏�����
        defaultY = -4.0f;
        intervalY = 1.02f;

        //Resources�t�H���_�̃I�u�W�F�N�g��ǂݍ���
        obj[0] = (GameObject)Resources.Load("Default_Obj");
        obj[1] = (GameObject)Resources.Load("DefaultL_Obj");
        obj[2] = (GameObject)Resources.Load("DefaultR_Obj");

        //���������s��
        ReInit();
    }

    //�X�V����
    private void Update()
    {
        Debug.Log("�X�V��");
    }

    //�ď���������
    private void ReInit()
    {
        //��ԃx�[�X�̃I�u�W�F�N�g�𐶐�
        GenerateObject(0);

        //2�i�ڈȍ~�̃I�u�W�F�N�g�𐶐�
        for (int i = 0; i < 9; i++)
        {
            defaultY += intervalY;

            int randNum = (int)Random.Range(0, 3);

            GenerateObject(randNum);
        }
    }

    //�I�u�W�F�N�g��������
    private void GenerateObject(int _objNum)
    {
        //�I�u�W�F�N�g�𐶐�
        GameObject inst = (GameObject)Instantiate(obj[_objNum],
            new Vector3(0.0f, defaultY, 0.0f), Quaternion.identity);

        //�e�I�u�W�F�N�g��ݒ�
        inst.transform.SetParent(this.transform, false);
    }

    //�q���I�u�W�F�N�g���폜
    private void AllChildrenObjectDelete()
    {
        foreach(Transform t in this.gameObject.transform)
        {
            GameObject.Destroy(t.gameObject);
        }
    }
}
