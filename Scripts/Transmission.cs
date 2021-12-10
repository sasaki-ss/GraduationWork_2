using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transmission : MonoBehaviour
{
    string sceneName;

    //�{�^���ȊO��UI(�����L���O�ȊO)

    GameObject textWorning;     //�x���e�L�X�g
    GameObject inputID;         //ID
    GameObject inputPass;       //�p�X���[�h
    GameObject inputCon;        //(�o�^�V�[��) �p�X���[�h�ē���
    GameObject button;          //(����)�{�^��
    BackButton _backButton;     //�{�^���X�N���v�g

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        textWorning = GameObject.Find("WarningText");
        inputID = GameObject.Find("InputID");
        inputPass = GameObject.Find("InputPass");
        if (sceneName == "UserLogin") button = GameObject.Find("LoginButton");                  //���O�C���V�[��
        if (sceneName == "UserRegistration") inputCon = GameObject.Find("InputConfirmation");   //�o�^�V�[��
        if (sceneName == "UserRegistration") button = GameObject.Find("CreateButton");
        _backButton = button.GetComponent<BackButton>();
    }

    void Update()
    {
        if (_backButton.Flag == true)   //�{�^���������ꂽ�ꍇ
        {
            if (sceneName == "UserLogin") Login();
            if (sceneName == "UserRegistration") Registration();
        }
    }

    void Login()
    {
        _backButton.Flag = false;
    }
    void Registration()
    {
        _backButton.Flag = false;
    }
}
