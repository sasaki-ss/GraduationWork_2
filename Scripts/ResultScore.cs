using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private void Start()
    {
        text = this.GetComponent<Text>();
        text.text = Score.GetScore().ToString();
    }
}
