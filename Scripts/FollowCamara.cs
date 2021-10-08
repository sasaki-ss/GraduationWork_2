using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamara : MonoBehaviour
{
    [SerializeField]
    private Camera cameraObj;
    private GameObject player;      //プレイヤー
    private Transform playerTf;     //プレイヤートランスフォーム
    private Transform cameraTf;     //カメラ座標
    private Vector3 keepVec;        //プレイヤー座標の保持
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
        keepVec.y = keepVec.y - 0.03924f;   //プレイヤーの一番下の段の時の座標に合わせている
    }
    private void LateUpdate()
    {
        bottomY = transform.position.y - 5f;

        if (keepVec.y < playerTf.position.y)    //一度カメラが上昇したら下がらないようにする
        {
            transform.position = new Vector3(transform.position.x, cameraTf.position.y + 2.5f, transform.position.z);
            keepVec.y = playerTf.position.y;

        }

        //if (Input.GetKey(KeyCode.A))  //座標を表示
        //{
        //    Debug.Log("keep" + keepVec.y);
        //    Debug.Log("player" + playerTf.position.y);
        //}

    }
}
