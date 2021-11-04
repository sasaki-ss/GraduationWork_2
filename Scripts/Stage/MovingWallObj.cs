using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallObj : StageObject
{
    private int floorNum;   //床の数

    private void Awake()
    {
        floorNum = 0;
        isOnPlayer = false;
    }

    private void Start()
    {
        GameObject floorObj = (GameObject)Resources.Load("StageObj_001");

        for(int i = 0; i < floorNum; i++)
        {
            //オブジェクトを生成
            GameObject inst = (GameObject)Instantiate(floorObj,
                new Vector3(0f, 0f + (i * Define.MAP_INTERVAL_Y), 0f), Quaternion.Euler(0f, 0f, 90f));

            inst.transform.SetParent(this.transform, false);
        }
    }

    public void SetFloorNum(int _val)
    {
        floorNum = _val;
    }
}
