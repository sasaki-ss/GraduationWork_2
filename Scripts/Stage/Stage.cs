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
        Vanish_Obj,
        Moving_Obj,
        MovingFloor_Obj,
        SwapFloor_Obj
    };

    private ObjectInfo[]    obj;            //オブジェクト情報
    
    private float   defaultY;         //初期座標
    private float   destroyPointY;    //破壊間隔

    private int     generateCnt;    //生成数
    private int     oneBeforeObj;   //ひとつ前の生成オブジェクト
    private bool    isChange;       //難易度変更フラグ

    private GameObject  jumpItemObj;        //ジャンプアイテム用オブジェクト
    private int         itemCoolDownCnt;    //アイテムの生成クールタイムカウント
    private int         itemCoolTime;       //アイテムの生成クールタイム
    private bool        isItemCoolDown;     //アイテムのクールダウンフラグ

    private GameObject  jumpTableObj;
    private int         tableCoolDownCnt;
    private int         tableCoolTime;
    private bool        isTableCoolDown;

    //初期化処理
    private void Start()
    {
        FollowCamara fCamera = GameObject.Find("Main Camera").GetComponent<FollowCamara>();

        //配列を確保
        obj = new ObjectInfo[Define.MAP_OBJECT_NUM];

        //private変数の初期化
        defaultY = -4f;
        destroyPointY = fCamera.bottomY - 3f;
        generateCnt = 0;
        oneBeforeObj = 0;
        isChange = false;

        for (int i = 0; i < Define.MAP_OBJECT_NUM; i++)
        {
            obj[i].coolDownCnt = 0;
            obj[i].isCoolDown = false;
        }

        itemCoolDownCnt = 0;
        isItemCoolDown = true;
        tableCoolDownCnt = 0;
        isTableCoolDown = true;

        obj[(int)ObjectList.Vanish_Obj].isCoolDown = true;
        obj[(int)ObjectList.Moving_Obj].isCoolDown = true;
        obj[(int)ObjectList.MovingFloor_Obj].isCoolDown = true;
        obj[(int)ObjectList.SwapFloor_Obj].isCoolDown = true;

        //初期クールタイムを設定
        obj[(int)ObjectList.Default].coolTime = 0;
        obj[(int)ObjectList.Default_L].coolTime = 2;
        obj[(int)ObjectList.Default_R].coolTime = 2;
        obj[(int)ObjectList.Default_None].coolTime = 10;
        obj[(int)ObjectList.Vanish_Obj].coolTime = 8;
        obj[(int)ObjectList.Moving_Obj].coolTime = 20;
        obj[(int)ObjectList.MovingFloor_Obj].coolTime = 8;
        obj[(int)ObjectList.SwapFloor_Obj].coolTime = 14;
        obj[8].coolTime = 0;
        obj[9].coolTime = 0;

        itemCoolTime = 50;
        tableCoolTime = 15;

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
        obj[(int)ObjectList.Moving_Obj].obj =
            (GameObject)Resources.Load("MovingWallObj");
        obj[(int)ObjectList.MovingFloor_Obj].obj =
            (GameObject)Resources.Load("MovingFloor_Obj");
        obj[(int)ObjectList.SwapFloor_Obj].obj =
            (GameObject)Resources.Load("SwapFloor_Obj");

        jumpItemObj = (GameObject)Resources.Load("JumpItem");
        jumpTableObj = (GameObject)Resources.Load("JumpTable");

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
            if (t.GetComponent<StageObject>().destroyPoint <= destroyPointY)
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

        if (isItemCoolDown)
        {
            if(itemCoolDownCnt >= itemCoolTime)
            {
                itemCoolDownCnt = 0;
                isItemCoolDown = false;
            }
            else
            {
                itemCoolDownCnt++;
            }
        }

        if (isTableCoolDown)
        {
            if (tableCoolDownCnt >= tableCoolTime)
            {
                tableCoolDownCnt = 0;
                isTableCoolDown = false;
            }
            else
            {
                tableCoolDownCnt++;
            }
        }

        //乱数がクールダウン中のオブジェクトか判定する
        while (!isCheck)
        {
            randNum = (int)Random.Range(0, 8);

            if (!(oneBeforeObj >= (int)ObjectList.Default &&
                oneBeforeObj <= (int)ObjectList.Default_R) &&
                (randNum == (int)ObjectList.MovingFloor_Obj || 
                randNum == (int)ObjectList.SwapFloor_Obj)) continue;

            if (oneBeforeObj != (int)ObjectList.Default &&
                randNum == (int)ObjectList.Moving_Obj) continue;

            if (!obj[randNum].isCoolDown)
            {
                isCheck = true;
                oneBeforeObj = randNum;
            }
        }

        //オブジェクトを生成
        GenerateObject(randNum);
    }

    //オブジェクト生成処理
    private void GenerateObject(int _objNum, bool _isOnPlayer = false)
    {
        //オブジェクトを生成
        GameObject inst = (GameObject)Instantiate(obj[_objNum].obj,
            new Vector3(0f, defaultY, 0f), Quaternion.identity);

        //親オブジェクトを設定
        inst.transform.SetParent(this.transform, false);

        if (_objNum != (int)ObjectList.MovingFloor_Obj && _objNum != (int)ObjectList.SwapFloor_Obj)
        {
            int itemRandNum = (int)Random.Range(0, 100);

            if (itemRandNum % 10 == 0 && !isItemCoolDown)
            {
                GameObject item = (GameObject)Instantiate(jumpItemObj,
                    new Vector3(Random.Range(-2.4f, 2.4f), defaultY + 0.5f, 0f), Quaternion.identity);

                item.name = "JumpItem";
                item.transform.SetParent(this.transform, false);
                item.GetComponent<StageObject>().destroyPoint = defaultY;

                isItemCoolDown = true;
            }

            int tableRandNum = (int)Random.Range(0, 100);

            if (tableRandNum % 5 == 0 && !isTableCoolDown)
            {
                GameObject item = (GameObject)Instantiate(jumpTableObj,
                    new Vector3(Random.Range(-2.2f, 2.2f), defaultY + 0.5f, 0f), Quaternion.identity);

                item.name = "JumpTable";
                item.transform.SetParent(this.transform, false);
                item.GetComponent<StageObject>().destroyPoint = defaultY;

                isTableCoolDown = true;
            }
        }

        //ノーマル系の生成の場合の処理
        if (_objNum >= (int)ObjectList.Default && _objNum <= (int)ObjectList.Default_None)
        {
            inst.GetComponentInChildren<FloorObj>().isOnPlayer = _isOnPlayer;
        }

        if(_objNum == (int)ObjectList.Moving_Obj)
        {
            int floorNum = (int)Random.Range(5, 10);

            inst.GetComponent<MovingWallObj>().SetFloorNum(floorNum);

            defaultY += (Define.MAP_INTERVAL_Y * floorNum);
        }

        //デフォルトオブジェクト(0番目のObj)以外の場合クールダウンにする
        if(_objNum != (int)ObjectList.Default)
        {
            obj[_objNum].isCoolDown = true;
        }

        //間隔を更新
        if(_objNum != (int)ObjectList.Moving_Obj)
        {
            defaultY += Define.MAP_INTERVAL_Y;
        }

        inst.GetComponent<StageObject>().destroyPoint = defaultY;
        generateCnt++;
    }
}
