using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultObj : MonoBehaviour
{
    [SerializeField]
    private GameObject floor;   //���I�u�W�F�N�g

    //����������
    private void Start()
    {
        //���I�u�W�F�N�g���擾
        floor = transform.Find("StageObj_001").gameObject;
    }
}
