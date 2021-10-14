using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ResultScore : MonoBehaviour
{
    [SerializeField]
    private Text text;
    private bool flg;

    private void Start()
    {
        text = this.GetComponent<Text>();
        text.text = Score.GetScore().ToString();
        flg = false;
        Invoke(nameof(FlagChange), 1.5f);
    }
    private void Update()
    {
        if (flg)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("InGame");
            }
        }
    }

    private void FlagChange()
    {
        flg = true;
    }
}
