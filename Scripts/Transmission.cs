using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

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

    //URL��https����Ɠ����܂���̂�http�ɂ��Ă�������
    //�{����URL
    private string serverURL = "";
    //�e�X�g�p��URL
    //private string serverURL = "http://192.168.0.15/a.php";

    //�T�[�o�[�����瑗���Ă���e�L�X�g
    private string resultText;

    //�T�[�o�[�����瑗���Ă���ŗLID
    public string privateID { get; set; }

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

        resultText = null;
        privateID  = null;
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
        serverURL = "http://25.11.163.122/userLogin.php";
        Send();
        _backButton.Flag = false;
    }
    void Registration()
    {
        if (inputCon.GetComponent<InputField>().text != inputPass.GetComponent<InputField>().text)
        {
            Debug.Log("�p�X���[�h����v���܂���");
            return;
        }
        serverURL = "http://25.11.163.122/userSinUp.php";
        Send();
        _backButton.Flag = false;
    }

    void Send()
    {
        if (  inputID.GetComponent<InputField>().text.Length <  1 ||
            inputPass.GetComponent<InputField>().text.Length <  1 ||
              inputID.GetComponent<InputField>().text.Length > 10 ||
            inputPass.GetComponent<InputField>().text.Length > 12 )
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
            resultText = null;
        }
        else if (request.isDone)
        {
            // �T�[�o����̃��X�|���X��\��
            Debug.Log("�ڑ�");
            privateID = request.downloadHandler.text;
            Debug.Log(privateID);
        }
    }
}