using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obj;   //マップオブジェクト
    
    private float defaultY;         //初期座標
    private float intervalY;        //マップオブジェクトの間隔
    private float destroyPointY;    //破壊間隔

    //初期化処理
    private void Start()
    {
        FollowCamara fCamera = GameObject.Find("Main Camera").GetComponent<FollowCamara>();

        //配列を確保
        obj = new GameObject[Define.MAP_OBJECT_NUM];

        //private変数の初期化
        defaultY = -4f;
        intervalY = 1.02f;
        destroyPointY = fCamera.bottomY - 3f;

        //Resourcesフォルダのオブジェクトを読み込む
        obj[0] = (GameObject)Resources.Load("Default_Obj");
        obj[1] = (GameObject)Resources.Load("DefaultL_Obj");
        obj[2] = (GameObject)Resources.Load("DefaultR_Obj");

        //初期化を行う
        ReInit();
    }

    //更新処理
    private void Update()
    {
        FollowCamara fCamera = GameObject.Find("Main Camera").GetComponent<FollowCamara>();

        destroyPointY = fCamera.bottomY - 3f;

        Debug.Log("破壊ポイント : " + destroyPointY);

        foreach (Transform t in this.gameObject.transform)
        {
            if (t.position.y <= destroyPointY)
            {
                GameObject.Destroy(t.gameObject);
            }
        }
    }

    //再初期化処理
    private void ReInit()
    {
        //一番ベースのオブジェクトを生成
        GenerateObject(0);

        //2段目以降のオブジェクトを生成
        for (int i = 0; i < 9; i++)
        {
            defaultY += intervalY;

            int randNum = (int)Random.Range(0, 3);

            GenerateObject(randNum);
        }
    }

    //オブジェクト生成処理
    private void GenerateObject(int _objNum)
    {
        //オブジェクトを生成
        GameObject inst = (GameObject)Instantiate(obj[_objNum],
            new Vector3(0f, defaultY, 0f), Quaternion.identity);

        //親オブジェクトを設定
        inst.transform.SetParent(this.transform, false);
    }

    //子をオブジェクトを削除
    private void AllChildrenObjectDelete()
    {
        foreach(Transform t in this.gameObject.transform)
        {
            GameObject.Destroy(t.gameObject);
        }
    }
}
