using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private string tmpText;

    [SerializeField]
    private GameObject score;
    [SerializeField]
    private GameObject highScore;
    [SerializeField]
    private GameObject newScore;

    private void Start()
    {
        tmpText = "現在のハイスコア\n";

        score = GameObject.Find("Result_Score");
        highScore = GameObject.Find("Result_HighScore");
        newScore = GameObject.Find("Result_NewScore");

        score.GetComponent<Text>().text = Score.GetScore().ToString();
        highScore.GetComponent<Text>().text = tmpText + Score.GetHighScore().ToString();

        if (!Score.GetIsHighScore())
        {
            newScore.SetActive(false);
        }
    }
}
