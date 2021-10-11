using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private static int score = 0;

    private void Start()
    {
        score = 0;
    }

    public void AddScore(int _jumpCnt)
    {
        score += Define.DEFALUT_SCORE + (_jumpCnt * 2);
    }

    public static int GetScore()
    {
        return score;
    }
}
