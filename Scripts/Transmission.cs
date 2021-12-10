using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transmission : MonoBehaviour
{
    string sceneName;

    //ボタン以外のUI(ランキング以外)

    GameObject textWorning;     //警告テキスト
    GameObject inputID;         //ID
    GameObject inputPass;       //パスワード
    GameObject inputCon;        //(登録シーン) パスワード再入力
    GameObject button;          //(共通)ボタン
    BackButton _backButton;     //ボタンスクリプト

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        textWorning = GameObject.Find("WarningText");
        inputID = GameObject.Find("InputID");
        inputPass = GameObject.Find("InputPass");
        if (sceneName == "UserLogin") button = GameObject.Find("LoginButton");                  //ログインシーン
        if (sceneName == "UserRegistration") inputCon = GameObject.Find("InputConfirmation");   //登録シーン
        if (sceneName == "UserRegistration") button = GameObject.Find("CreateButton");
        _backButton = button.GetComponent<BackButton>();
    }

    void Update()
    {
        if (_backButton.Flag == true)   //ボタンが押された場合
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
