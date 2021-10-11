using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float           gameOverLine;   //ゲームオーバーの基準線
    private FollowCamara    fCamera;        //followCamera
    private GameObject      player;         //Player

    private void Start()
    {
        fCamera = GameObject.Find("Main Camera").GetComponent<FollowCamara>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        //ゲームオーバーの基準線を更新する
        gameOverLine = fCamera.bottomY - 0.3f;

        //基準線を超える際、ゲームオーバーへ
        if(gameOverLine >= player.transform.position.y)
        {
            Debug.Log("GameOver");

            SceneManager.LoadScene("Result");
        }
    }
}
