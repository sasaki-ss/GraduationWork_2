using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishProc : MonoBehaviour
{
    private float   timer;      //タイマー
    private bool    isDestroy;  //破壊フラグ

    private void Start()
    {
        timer = 0.3f;
        isDestroy = false;
    }

    private void Update()
    {
        //破壊フラグがオンの場合
        if (isDestroy)
        {
            //timerが0以下になった際オブジェクトを破壊
            if (timer <= 0.0f)
            {
                Destroy(this.gameObject);
            }

            //タイマーを減少させる
            timer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isDestroy = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isDestroy = true;
        }
    }
}
