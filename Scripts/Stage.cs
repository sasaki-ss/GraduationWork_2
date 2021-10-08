using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private struct ObjectInfo
    {
        public int          coolTime;
        public int          coolDownCnt;
        public bool         isCoolDown;
        public GameObject   obj;
    };

    private enum ObjectList
    {
        Default,
        Default_L,
        Default_R,
        Default_None,
        DisappearFloor
    };

    ObjectInfo[] obj;

    [SerializeField]
    private GameObject[] obj2;   //マップオブジェクト
    
    private float defaultY;         //初期座標
    private float intervalY;        //マップオブジェクトの間隔
    private float destroyPointY;    //破壊間隔

    //初期化処理
    private void Start()
    {
        FollowCamara fCamera = GameObject.Find("Main Camera").GetComponent<FollowCamara>();

        //配列を確保
        obj = new ObjectInfo[Define.MAP_OBJECT_NUM];

        //private変数の初期化
        defaultY = -4f;
        intervalY = 1.02f;
        destroyPointY = fCamera.bottomY - 3f;

        for (int i = 0; i < Define.MAP_OBJECT_NUM; i++)
        {
            obj[i].coolDownCnt = 0;
            obj[i].isCoolDown = false;
        }

        obj[(int)ObjectList.Default].coolTime = 0;
        obj[(int)ObjectList.Default_L].coolTime = 4;
        obj[(int)ObjectList.Default_R].coolTime = 4;
        obj[(int)ObjectList.Default_None].coolTime = 6;
        obj[(int)ObjectList.DisappearFloor].coolTime = 6;
        obj[5].coolTime = 0;
        obj[6].coolTime = 0;
        obj[7].coolTime = 0;
        obj[8].coolTime = 0;
        obj[9].coolTime = 0;

        //Resourcesフォルダのオブジェクトを読み込む
        obj[(int)ObjectList.Default].obj = 
            (GameObject)Resources.Load("Default_Obj");
        obj[(int)ObjectList.Default_L].obj = 
            (GameObject)Resources.Load("DefaultL_Obj");
        obj[(int)ObjectList.Default_R].obj = 
            (GameObject)Resources.Load("DefaultR_Obj");
        obj[(int)ObjectList.Default_None].obj = 
            (GameObject)Resources.Load("DefaultNone_Obj");
        obj[(int)ObjectList.DisappearFloor].obj = 
            (GameObject)Resources.Load("DisappearFloor_Obj");

        //初期化を行う
        ReInit();
    }

    //更新処理
    private void Update()
    {
        FollowCamara fCamera = GameObject.Find("Main Camera").GetComponent<FollowCamara>();

        destroyPointY = fCamera.bottomY - 3f;

        foreach (Transform t in this.gameObject.transform)
        {
            if (t.position.y <= destroyPointY)
            {
                GameObject.Destroy(t.gameObject);

                Generate();
            }
        }
    }

    //再初期化処理
    private void ReInit()
    {
        //一番ベースのオブジェクトを生成
        GenerateObject(0);

        //2段目以降のオブジェクトを生成
        for (int i = 0; i < 15; i++)
        {
            Generate();
        }
    }

    private void Generate()
    {
        int     randNum = 0;
        bool    isCheck = false;

        for (int i = 0; i < Define.MAP_OBJECT_NUM; i++)
        {
            if (obj[i].isCoolDown)
            {
                if (obj[i].coolDownCnt == obj[i].coolTime)
                {
                    obj[i].coolDownCnt = 0;
                    obj[i].isCoolDown = false;

                    continue;
                }

                obj[i].coolDownCnt++;
            }
        }

        while (!isCheck)
        {
            randNum = (int)Random.Range(0, 5);

            if (!obj[randNum].isCoolDown) isCheck = true;
        }

        GenerateObject(randNum);
    }

    //オブジェクト生成処理
    private void GenerateObject(int _objNum)
    {
        //オブジェクトを生成
        GameObject inst = (GameObject)Instantiate(obj[_objNum].obj,
            new Vector3(0f, defaultY, 0f), Quaternion.identity);

        //親オブジェクトを設定
        inst.transform.SetParent(this.transform, false);

        if(_objNum != (int)ObjectList.Default)
        {
            obj[_objNum].isCoolDown = true;
        }

        defaultY += intervalY;
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
