using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTriggerProc : MonoBehaviour
{
    public bool isInvasion { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInvasion = true;
        }
    }
}
