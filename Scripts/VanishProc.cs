using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishProc : MonoBehaviour
{
    private float   timer;      //�^�C�}�[
    private bool    isDestroy;  //�j��t���O

    private void Start()
    {
        timer = 0.3f;
        isDestroy = false;
    }

    private void Update()
    {
        //�j��t���O���I���̏ꍇ
        if (isDestroy)
        {
            //timer��0�ȉ��ɂȂ����ۃI�u�W�F�N�g��j��
            if (timer <= 0.0f)
            {
                Destroy(this.gameObject);
            }

            //�^�C�}�[������������
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
