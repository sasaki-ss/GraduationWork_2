using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

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

    //URLをhttpsすると動きませんのでhttpにしてください
    //本当のURL
    private string serverURL = "http://25.11.163.122/userLogin.php";
    //テスト用のURL
    //private string serverURL = "http://192.168.0.15/a.php";

    //サーバー側から送られてくるテキスト
    private string resultText;

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

        resultText = null;
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
        Send();
        _backButton.Flag = false;
    }
    void Registration()
    {
        if(inputCon.GetComponent<InputField>().text != inputPass.GetComponent<InputField>().text)
        {
            Debug.Log("パスワードが一致しません");
            return;
        }

        Send();
        _backButton.Flag = false;
    }

    void Send()
    {
        // サーバへPOSTするデータを設定 
        Dictionary<string, string> dic = new Dictionary<string, string>();

        //POSTの中身の設定
        dic.Add("name", inputID.GetComponent<InputField>().text);            //ID
        dic.Add("password", inputPass.GetComponent<InputField>().text);    //パスワード

        StartCoroutine(HttpPost(serverURL, dic));  // POST
    }

    // HTTP POST リクエスト
    IEnumerator HttpPost(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();

        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        UnityWebRequest request = UnityWebRequest.Post(url, form);

        //送受信開始
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            //エラー
            Debug.Log("エラー");
            resultText = null;
        }
        else if (request.isDone)
        {
            // サーバからのレスポンスを表示
            Debug.Log("接続");
            resultText = request.downloadHandler.text;
            Debug.Log(resultText);
        }
    }
}
