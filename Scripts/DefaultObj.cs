using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultObj : MonoBehaviour
{
    [SerializeField]
    private GameObject floor;   //床オブジェクト

    //初期化処理
    private void Start()
    {
        //床オブジェクトを取得
        floor = transform.Find("StageObj_001").gameObject;
    }
}
