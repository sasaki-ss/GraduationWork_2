using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearFloorObj : MonoBehaviour
{
    private float   defaultY;   //初期座標
    private float   intervalY;
    private bool    isOnPlayer;

    [SerializeField]
    private GameObject floorObj;

    private void Start()
    {
        defaultY = -2.2169f;
        intervalY = 0.5f;
        isOnPlayer = false;

        floorObj = (GameObject)Resources.Load("StageObj_003");

        for(int i = 0; i < 10; i++)
        {
            //オブジェクトを生成
            GameObject inst = (GameObject)Instantiate(floorObj,
                new Vector3(defaultY, 0f, 0f), Quaternion.Euler(0f, 0f, 90f));

            //親オブジェクトを設定
            inst.transform.SetParent(this.transform, false);
            inst.GetComponent<BoxCollider2D>().enabled = false;

            defaultY += intervalY;
        }
    }

    private void Update()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();

        if (!isOnPlayer)
        {
            if (transform.position.y <= player.transform.position.y - 0.5f)
            {
                foreach(Transform t in gameObject.transform)
                {
                    t.GetComponent<BoxCollider2D>().enabled = true;
                }
                isOnPlayer = true;
                Debug.Log("当たり判定を付与");
            }
        }
        else
        {
            if (transform.position.y >= player.transform.position.y)
            {
                foreach (Transform t in gameObject.transform)
                {
                    t.GetComponent<BoxCollider2D>().enabled = false;
                }
                isOnPlayer = false;
                Debug.Log("当たり判定を奪取");
            }
        }
    }
}
