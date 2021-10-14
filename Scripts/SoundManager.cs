using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip   bgmFile;    //BGM�t�@�C��
    private AudioSource aSrc;       //�I�[�f�B�I�\�[�X

    private void Start()
    {
        aSrc = gameObject.GetComponent<AudioSource>();
        aSrc.clip = bgmFile;
        aSrc.volume = 0.3f;
        aSrc.loop = true;
        aSrc.Play();
    }
}
