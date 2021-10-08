using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearFloor : MonoBehaviour
{
    private float timer;
    private bool isDestroy;

    private void Start()
    {
        timer = 0.3f;
        isDestroy = false;
    }

    private void Update()
    {
        if (isDestroy)
        {
            if(timer <= 0.0f)
            {
                Destroy(this.gameObject);
            }
            timer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Á‚¦‚é°‚ðíœ‚µ‚Ü‚·");
            isDestroy = true;
        }
    }
}
