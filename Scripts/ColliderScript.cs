using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    private bool isTouch;
    private bool isTouchEnter,isTouchStay,isTouchExit;
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
        Debug.Log("何かが判定に入りました");
        isTouchEnter = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("何かが判定に入り続けています");
        isTouchStay = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("何かが判定をでました");
        isTouchExit = true;
    }
}
