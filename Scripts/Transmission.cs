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

    //�{�^���ȊO��UI(�����L���O�ȊO)

    GameObject textWorning;     //�x���e�L�X�g
    GameObject inputID;         //ID
    GameObject inputPass;       //�p�X���[�h
    GameObject inputCon;        //(�o�^�V�[��) �p�X���[�h�ē���
    GameObject button;          //(����)�{�^��
    BackButton _backButton;     //�{�^���X�N���v�g

    GameObject score;           //�X�R�A

    bool flg;                   //��񂾂��s���t���O

    //URL��https����Ɠ����܂���̂�http�ɂ��Ă�������
    //�{����URL
    string serverURL;
    //�e�X�g�p��URL
    //private string serverURL = "http://192.168.0.15/a.php";

    //�T�[�o�[�����瑗���Ă���e�L�X�g
    string resultText;

    //�T�[�o�[�����瑗���Ă���ŗLID
    public static string privateID { get; set; }

    //�T�[�o�[������擾�������[�U�[�̃X�R�A�Ȃǂ��������
    public string[] getUserData { get; set; }

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        textWorning = GameObject.Find("WarningText");
        inputID = GameObject.Find("InputID");
        inputPass = GameObject.Find("InputPass");
        if (sceneName == "Result") score = GameObject.Find("Result_Score");                     //���U���g�V�[��
        if (sceneName == "UserLogin") button = GameObject.Find("LoginButton");                  //���O�C���V�[��
        if (sceneName == "UserRegistration") inputCon = GameObject.Find("InputConfirmation");   //�o�^�V�[��
        if (sceneName == "UserRegistration") button = GameObject.Find("CreateButton");
        if (sceneName == "UserLogin" || sceneName == "UserRegistration") _backButton = button.GetComponent<BackButton>();

        serverURL   = "";
        resultText  = "";
        flg         = false;
    }

    void Update()
    {
        //�f�[�^�̍X�V��}����php���̏���

        //�X�R�A���X�V���鎞�̏���
        if (sceneName == "Result" && flg == false)
        {
            ScoreUpdate();
            flg = true;
        }

        //�����L���O�\���̂��߂̃f�[�^�擾�̏���
        if (sceneName == "Ranking" && flg == false)
        {
            Ranking();
            flg = true;
        }

        //�{�^���������ꂽ�ꍇ
        //���O�C���A�T�C���C���̏ꍇ�̏���
        if (sceneName != "Result" && sceneName != "Ranking" && _backButton.Flag == true)
        {
            if (sceneName == "UserLogin") Login();
            if (sceneName == "UserRegistration") Registration();
        }
    }

    void Login()
    {
        //���O�C���̏���
        //Debug.Log("Login");

        serverURL = "http://25.11.163.122/userLogin.php";
        //serverURL = "http://192.168.0.15/userLogin.php";

        Send();
        if (sceneName != "Result") _backButton.Flag = false;
    }

    void Registration()
    {
        //�T�C���A�b�v�̏���
        //Debug.Log("Registration");

        if (inputCon.GetComponent<InputField>().text != inputPass.GetComponent<InputField>().text)
        {
            Debug.Log("�p�X���[�h����v���܂���");
            return;
        }

        serverURL = "http://25.11.163.122/userSinUp.php";
        //serverURL = "http://192.168.0.15/userSinUp.php";

        Send();
        if (sceneName != "Result") _backButton.Flag = false;
    }

    void Ranking()
    {
        //���[�U�[�f�[�^���擾���ĕϐ��ɑ�����鏈��
        //Debug.Log("Ranking");

        serverURL = "http://25.11.163.122/getRanking.php";
        //serverURL = "http://192.168.0.15/getRanking.php";

        // �T�[�o��POST����f�[�^��ݒ� 
        Dictionary<string, string> dic = new Dictionary<string, string>();

        if (Transmission.privateID == null) Transmission.privateID = "-1";

        //POST�̒��g�̐ݒ�
        dic.Add("id", Transmission.privateID);     //�ŗLID

        StartCoroutine(HttpPost(serverURL, dic));  // POST
    }

    void ScoreUpdate()
    {

        //�X�R�A�X�V���邽�߂ɕK�v�ȏ��𑗐M���鏈��
        //Debug.Log("ScoreUpdate");

        if (Transmission.privateID == null) return;

        serverURL = "http://25.11.163.122/scoreUpdate.php";
        //serverURL = "http://192.168.0.15/scoreUpdate.php";

        // �T�[�o��POST����f�[�^��ݒ� 
        Dictionary<string, string> dic = new Dictionary<string, string>();

        //���Ԃ��擾
        DateTime TodayNow = DateTime.Now;

        string date = TodayNow.Year.ToString() + "-" + TodayNow.Month.ToString() + "-" + TodayNow.Day.ToString() + " " + DateTime.Now.ToLongTimeString();

        //POST�̒��g�̐ݒ�
        dic.Add("id", Transmission.privateID);                //�ŗLID
        dic.Add("score", score.GetComponent<Text>().text);    //�X�R�A
        dic.Add("date", date);                                //���t

        //Debug.Log(Transmission.privateID);
        //Debug.Log(score.GetComponent<Text>().text);
        //Debug.Log(date);

        StartCoroutine(HttpPost(serverURL, dic));  // POST
    }

    void Send()
    {
        //�ڑ�����
        //Debug.Log("Send");

        if (  inputID.GetComponent<InputField>().text.Length <  1 ||
            inputPass.GetComponent<InputField>().text.Length <  1 ||
              inputID.GetComponent<InputField>().text.Length > 10 ||
            inputPass.GetComponent<InputField>().text.Length > 12)
        {
            Debug.Log("�p�X���[�h�܂��͖��O�̕��������͂���Ă��Ȃ��A�܂��͐����傫�����܂��B");
            return;
        }

        // �T�[�o��POST����f�[�^��ݒ� 
        Dictionary<string, string> dic = new Dictionary<string, string>();

        //POST�̒��g�̐ݒ�
        dic.Add("name", inputID.GetComponent<InputField>().text);          //ID
        dic.Add("password", inputPass.GetComponent<InputField>().text);    //�p�X���[�h

        StartCoroutine(HttpPost(serverURL, dic));  // POST
    }

    // HTTP POST ���N�G�X�g
    IEnumerator HttpPost(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();

        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        UnityWebRequest request = UnityWebRequest.Post(url, form);

        //����M�J�n
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            //�G���[
            Debug.Log("�G���[");
            privateID = null;
        }
        else if (request.isDone)
        {
            // �T�[�o����̃��X�|���X��\��
            Debug.Log("�ڑ�");

            //�T�[�o�[����̕ԐM
            resultText = request.downloadHandler.text;

            if (sceneName == "Ranking")
            {
                //���[�U�[�f�[�^�𕪊����đ��
                getUserData = resultText.Split('\n');
                for (int i = 0; i < getUserData.Length - 1; i++) Debug.Log(getUserData[i]);
            }
            else
            if (sceneName == "UserLogin")
            {
                //�ŗLID����
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