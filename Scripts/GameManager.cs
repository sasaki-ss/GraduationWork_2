using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float           gameOverLine;   //�Q�[���I�[�o�[�̊��
    private FollowCamara    fCamera;        //followCamera
    private GameObject      player;         //Player

    private void Start()
    {
        fCamera = GameObject.Find("Main Camera").GetComponent<FollowCamara>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        //�Q�[���I�[�o�[�̊�����X�V����
        gameOverLine = fCamera.bottomY - 0.3f;

        //����𒴂���ہA�Q�[���I�[�o�[��
        if(gameOverLine >= player.transform.position.y)
        {
            Debug.Log("GameOver");

            SceneManager.LoadScene("Result");
        }
    }
}
