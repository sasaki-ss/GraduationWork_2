using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private int score;

    private void Start()
    {
        score = 0;
    }

    public void AddScore(int _jumpCnt)
    {
        score += Define.DEFALUT_SCORE + (_jumpCnt * 2);
    }
}
