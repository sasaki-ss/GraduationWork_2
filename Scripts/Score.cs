using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveScore
{
    public int score;
}

public class Score : MonoBehaviour
{
    private static int score = 0;
    private static int highScore = 0;
    private string filePath;
    private SaveScore save;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".savedata.json";
        save = new SaveScore();
    }

    private void Start()
    {
        Load();

        highScore = save.score;

        score = 0;
    }

    private void OnDestroy()
    {
        if(score > highScore)
        {
            save.score = score;
        }

        Save();
    }

    public void AddScore(int _jumpCnt)
    {
        score += Define.DEFALUT_SCORE + (_jumpCnt * 2);
    }

    public static int GetScore()
    {
        return score;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(save);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(json);
        sw.Flush();
        sw.Close();
    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            string data = sr.ReadToEnd();
            sr.Close();

            save = JsonUtility.FromJson<SaveScore>(data);
        }
    }
}
