using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

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

    GameObject score;           //スコア

    bool flg;                   //一回だけ行うフラグ

    //URLをhttpsすると動きませんのでhttpにしてください
    //本当のURL
    string serverURL;
    //テスト用のURL
    //private string serverURL = "http://192.168.0.15/a.php";

    //サーバー側から送られてくるテキスト
    string resultText;

    //サーバー側から送られてくる固有ID
    public static string privateID { get; set; }

    //サーバー側から取得したユーザーのスコアなどをいれるやつ
    public string[] getUserData { get; set; }

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        textWorning = GameObject.Find("WarningText");
        inputID = GameObject.Find("InputID");
        inputPass = GameObject.Find("InputPass");
        if (sceneName == "Result") score = GameObject.Find("Result_Score");                     //リザルトシーン
        if (sceneName == "UserLogin") button = GameObject.Find("LoginButton");                  //ログインシーン
        if (sceneName == "UserRegistration") inputCon = GameObject.Find("InputConfirmation");   //登録シーン
        if (sceneName == "UserRegistration") button = GameObject.Find("CreateButton");
        if (sceneName == "UserLogin" || sceneName == "UserRegistration") _backButton = button.GetComponent<BackButton>();

        serverURL   = "";
        resultText  = "";
        flg         = false;
    }

    void Update()
    {
        //データの更新や挿入はphp側の処理

        //スコアを更新する時の処理
        if (sceneName == "Result" && flg == false)
        {
            ScoreUpdate();
            flg = true;
        }

        //ランキング表示のためのデータ取得の処理
        if (sceneName == "Ranking" && flg == false)
        {
            Ranking();
            flg = true;
        }

        //ボタンが押された場合
        //ログイン、サインインの場合の処理
        if (sceneName != "Result" && sceneName != "Ranking" && _backButton.Flag == true)
        {
            if (sceneName == "UserLogin") Login();
            if (sceneName == "UserRegistration") Registration();
        }
    }

    void Login()
    {
        //ログインの処理
        //Debug.Log("Login");

        serverURL = "http://25.11.163.122/userLogin.php";
        //serverURL = "http://192.168.0.15/userLogin.php";

        Send();
        if (sceneName != "Result") _backButton.Flag = false;
    }

    void Registration()
    {
        //サインアップの処理
        //Debug.Log("Registration");

        if (inputCon.GetComponent<InputField>().text != inputPass.GetComponent<InputField>().text)
        {
            Debug.Log("パスワードが一致しません");
            return;
        }

        serverURL = "http://25.11.163.122/userSinUp.php";
        //serverURL = "http://192.168.0.15/userSinUp.php";

        Send();
        if (sceneName != "Result") _backButton.Flag = false;
    }

    void Ranking()
    {
        //ユーザーデータを取得して変数に代入する処理
        //Debug.Log("Ranking");

        serverURL = "http://25.11.163.122/getRanking.php";
        //serverURL = "http://192.168.0.15/getRanking.php";

        // サーバへPOSTするデータを設定 
        Dictionary<string, string> dic = new Dictionary<string, string>();

        if (Transmission.privateID == null) Transmission.privateID = "-1";

        //POSTの中身の設定
        dic.Add("id", Transmission.privateID);     //固有ID

        StartCoroutine(HttpPost(serverURL, dic));  // POST
    }

    void ScoreUpdate()
    {

        //スコア更新するために必要な情報を送信する処理
        //Debug.Log("ScoreUpdate");

        if (Transmission.privateID == null) return;

        serverURL = "http://25.11.163.122/scoreUpdate.php";
        //serverURL = "http://192.168.0.15/scoreUpdate.php";

        // サーバへPOSTするデータを設定 
        Dictionary<string, string> dic = new Dictionary<string, string>();

        //時間を取得
        DateTime TodayNow = DateTime.Now;

        string date = TodayNow.Year.ToString() + "-" + TodayNow.Month.ToString() + "-" + TodayNow.Day.ToString() + " " + DateTime.Now.ToLongTimeString();

        //POSTの中身の設定
        dic.Add("id", Transmission.privateID);                //固有ID
        dic.Add("score", score.GetComponent<Text>().text);    //スコア
        dic.Add("date", date);                                //日付

        //Debug.Log(Transmission.privateID);
        //Debug.Log(score.GetComponent<Text>().text);
        //Debug.Log(date);

        StartCoroutine(HttpPost(serverURL, dic));  // POST
    }

    void Send()
    {
        //接続処理
        //Debug.Log("Send");

        if (  inputID.GetComponent<InputField>().text.Length <  1 ||
            inputPass.GetComponent<InputField>().text.Length <  1 ||
              inputID.GetComponent<InputField>().text.Length > 10 ||
            inputPass.GetComponent<InputField>().text.Length > 12)
        {
            Debug.Log("パスワードまたは名前の文字が入力されていない、または数が大きすぎます。");
            return;
        }

        // サーバへPOSTするデータを設定 
        Dictionary<string, string> dic = new Dictionary<string, string>();

        //POSTの中身の設定
        dic.Add("name", inputID.GetComponent<InputField>().text);          //ID
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
            privateID = null;
        }
        else if (request.isDone)
        {
            // サーバからのレスポンスを表示
            Debug.Log("接続");

            //サーバーからの返信
            resultText = request.downloadHandler.text;

            if (sceneName == "Ranking")
            {
                //ユーザーデータを分割して代入
                getUserData = resultText.Split('\n');
                for (int i = 0; i < getUserData.Length - 1; i++) Debug.Log(getUserData[i]);
            }
            else
            if (sceneName == "UserLogin")
            {
                //固有IDを代入
                privateID = request.downloadHandler.text;
                //Debug.Log(privateID);
            }
            else
            {
                resultText = request.downloadHandler.text;
                //Debug.Log(resultText);
            }
        }
    }
}