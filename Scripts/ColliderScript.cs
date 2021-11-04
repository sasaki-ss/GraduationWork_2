using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    //�I�u�W�F�N�g���I�u�W�F�N�g(�����蔻��)�͈͓̔��ɂ��邩���m����X�N���v�g
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
        //Debug.Log("����������ɓ���܂���");
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
        //Debug.Log("����������ɓ��葱���Ă��܂�");
        isTouchStay = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("������������ł܂���");
        isTouchExit = true;
    }
}
