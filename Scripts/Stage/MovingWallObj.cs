using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallObj : StageObject
{
    private int     floorNum;   //床の数
    private float   stopPoint;  //壁が止まる位置

    GameObject[] walls;     //壁

    private void Awake()
    {
        floorNum = 0;
        stopPoint = 0f;
        isOnPlayer = false;

        walls = new GameObject[2];
    }

    private void Start()
    {
        //生成に必要なオブジェクトを読み込む
        GameObject floorObj = (GameObject)Resources.Load("StageObj_001");
        GameObject wallObj = (GameObject)Resources.Load("StageObj_004");

        for(int i = 0; i < floorNum; i++)
        {
            //オブジェクトを生成
            GameObject inst = (GameObject)Instantiate(floorObj,
                new Vector3(0f, 0f + (i * Define.MAP_INTERVAL_Y), 0f), Quaternion.Euler(0f, 0f, 90f));

            inst.transform.SetParent(this.transform, false);
        }

        walls[0]= (GameObject)Instantiate(wallObj,
                new Vector3(-2.9f, 0.5f, 0f), Quaternion.identity);

        walls[1] = (GameObject)Instantiate(wallObj,
                new Vector3(2.9f, 0.5f + Define.MAP_INTERVAL_Y, 0f), Quaternion.identity);

        foreach (GameObject wall in walls)
        {
            wall.transform.SetParent(this.transform, false);
        }
    }

    private void Update()
    {
        foreach(GameObject wall in walls)
        {
            if (wall.GetComponent<WallTriggerProc>().isInvasion)
            {
                Vector3 beforePos = wall.transform.position;

                wall.transform.position = new Vector3(
                    beforePos.x, beforePos.y + (Define.MAP_INTERVAL_Y * 2), beforePos.z);

                if(wall.transform.position.y >= stopPoint)
                {
                    wall.transform.position = new Vector3(
                        beforePos.x, stopPoint, beforePos.z);
                }

                wall.GetComponent<WallTriggerProc>().isInvasion = false;
            }
        }
    }

    public void SetFloorNum(int _val)
    {
        floorNum = _val;
        stopPoint = this.transform.position.y + 0.5f + ((floorNum - 1) * Define.MAP_INTERVAL_Y);
    }
}
