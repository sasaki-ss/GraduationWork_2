using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    string sceneName;

    //ボタン以外のUI(ランキング以外)

    GameObject textWorning;
    GameObject inputID;
    GameObject inputPass;
    GameObject inputCon;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        textWorning = GameObject.Find("WarningText");
        inputID = GameObject.Find("InputID");
        inputPass = GameObject.Find("InputPass");
        if (sceneName == "UserRegistration") inputCon = GameObject.Find("InputConfirmation");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
