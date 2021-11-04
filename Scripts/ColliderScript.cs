using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    //オブジェクトがオブジェクト(当たり判定)の範囲内にあるか検知するスクリプト
    private bool isTouch;
    private bool isTouchEnter,isTouchStay,isTouchExit;

    private GameObject _player;
    private Player _scrPlayer;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _scrPlayer = _player.GetComponent<Player>();
    }
    public bool IsGround()
    {
        if (isTouchEnter || isTouchStay)
        {
            isTouch = true;
        }
        else if (isTouchExit)
        {
            isTouch = false;
        }

        isTouchEnter = false;
        isTouchStay = false;
        isTouchExit = false;
        return isTouch;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("何かが判定に入りました");
        if (collision.gameObject.name == "JumpItem")
        {
            Destroy(collision.gameObject);
            _scrPlayer.HighJump = true;
            _scrPlayer._HighJump();
        }

        else isTouchEnter = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("何かが判定に入り続けています");
        isTouchStay = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("何かが判定をでました");
        isTouchExit = true;
    }
}
