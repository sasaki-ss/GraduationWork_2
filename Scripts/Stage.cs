using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    //オブジェクト情報構造体
    private struct ObjectInfo
    {
        public int          coolTime;       //生成クールタイム
        public int          coolDownCnt;    //現在のクールタイムカウント
        public bool         isCoolDown;     //クールタイムフラグ
        public GameObject   obj;            //オブジェクト
    };

    //オブジェクト情報
    private enum ObjectList
    {
        Default,
        Default_L,
        Default_R,
        Default_None,
        Vanish_Obj
    };

    private ObjectInfo[] obj;       //オブジェクト情報
    
    private float defaultY;         //初期座標
    private float intervalY;        //マップオブジェクトの間隔
    private float destroyPointY;    //破壊間隔

    private int generateCnt;    //生成数
    private bool isChange;      //難易度変更フラグ

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
        generateCnt = 0;
        isChange = false;

        for (int i = 0; i < Define.MAP_OBJECT_NUM; i++)
        {
            obj[i].coolDownCnt = 0;
            obj[i].isCoolDown = false;
        }

        //初期クールタイムを設定
        obj[(int)ObjectList.Default].coolTime = 0;
        obj[(int)ObjectList.Default_L].coolTime = 4;
        obj[(int)ObjectList.Default_R].coolTime = 4;
        obj[(int)ObjectList.Default_None].coolTime = 6;
        obj[(int)ObjectList.Vanish_Obj].coolTime = 6;
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
        obj[(int)ObjectList.Vanish_Obj].obj = 
            (GameObject)Resources.Load("Vanish_Obj");

        //初期化を行う
        ReInit();
    }

    //更新処理
    private void Update()
    {
        //オブジェクトの破壊ポイントを更新
        FollowCamara fCamera = GameObject.Find("Main Camera").GetComponent<FollowCamara>();
        destroyPointY = fCamera.bottomY - 3f;

        if (generateCnt % Define.MAP_DIFFICULTY_CNT == 0 && !isChange)
        {
            for (int i = 0; i < Define.MAP_OBJECT_NUM; i++)
            {
                if (obj[i].coolTime == 0) continue;

                obj[i].coolTime--;
            }
            isChange = true;
        }
        else if (isChange && generateCnt % Define.MAP_DIFFICULTY_CNT != 0)
        {
            isChange = false;
        }

        //オブジェクト破壊処理
        foreach (Transform t in this.gameObject.transform)
        {
            //破壊ポイントより下の場合対象のオブジェクトを破壊する
            if (t.position.y <= destroyPointY)
            {
                GameObject.Destroy(t.gameObject);

                //オブジェクトを新規生成
                Generate();
            }
        }
    }

    //再初期化処理
    private void ReInit()
    {
        //一番ベースのオブジェクトを生成
        GenerateObject(0, true);

        //2段目以降のオブジェクトを生成
        for (int i = 0; i < 15; i++)
        {
            Generate();
        }
    }

    //生成処理
    private void Generate()
    {
        int     randNum = 0;        //乱数
        bool    isCheck = false;    //クールダウンチェックフラグ

        //クールダウン関連の処理
        for (int i = 0; i < Define.MAP_OBJECT_NUM; i++)
        {
            //クールダウン中の場合
            if (obj[i].isCoolDown)
            {
                //カウントがcoolTimeになったときクールダウンを終了する
                if (obj[i].coolDownCnt >= obj[i].coolTime)
                {
                    obj[i].coolDownCnt = 0;
                    obj[i].isCoolDown = false;

                    continue;
                }

                //クールダウンカウントを加算
                obj[i].coolDownCnt++;
            }
        }

        //乱数がクールダウン中のオブジェクトか判定する
        while (!isCheck)
        {
            randNum = (int)Random.Range(0, 5);

            if (!obj[randNum].isCoolDown) isCheck = true;
        }

        //オブジェクトを生成
        GenerateObject(randNum);
    }

    //オブジェクト生成処理
    private void GenerateObject(int _objNum,bool _isOnPlayer = false)
    {
        //オブジェクトを生成
        GameObject inst = (GameObject)Instantiate(obj[_objNum].obj,
            new Vector3(0f, defaultY, 0f), Quaternion.identity);

        //親オブジェクトを設定
        inst.transform.SetParent(this.transform, false);

        if(_objNum >= (int)ObjectList.Default && _objNum <= (int)ObjectList.Default_None)
        {
            inst.GetComponent<DefaultObj>().isOnPlayer = _isOnPlayer;
        }

        //デフォルトオブジェクト(0番目のObj)以外の場合クールダウンにする
        if(_objNum != (int)ObjectList.Default)
        {
            obj[_objNum].isCoolDown = true;
        }

        //間隔を更新
        defaultY += intervalY;

        generateCnt++;
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
