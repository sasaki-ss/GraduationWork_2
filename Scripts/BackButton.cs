using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    string sceneName;

    // Start is called before the first frame update
    public bool Flag {set;get;}
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        Flag = false;
    }

    public void Onclick()
    {   //É{É^ÉìÇÃèàóù
        if (sceneName == "Ranking") SceneManager.LoadScene("Title");
        else if (sceneName == "UserLogin" && gameObject.name == "LoginButton") Flag = true;
        else if (sceneName == "UserLogin" && gameObject.name == "CreateButton") SceneManager.LoadScene("UserRegistration");
        else if (sceneName == "UserLogin" && gameObject.name == "ReturnButton") SceneManager.LoadScene("Title");
        else if (sceneName == "UserRegistration" && gameObject.name == "CreateButton") Flag = true;
        else if (sceneName == "UserRegistration" && gameObject.name == "ReturnButton") SceneManager.LoadScene("UserLogin");
    }
}
