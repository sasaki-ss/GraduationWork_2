using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    string sceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    public void Onclick()
    {   //É{É^ÉìÇÃèàóù
        if (sceneName == "Ranking") SceneManager.LoadScene("Title");
        else if(sceneName == "UserLogin" && gameObject.name == "LoginButton")
        {

        }
        else if (sceneName == "UserLogin" && gameObject.name == "CreateButton") SceneManager.LoadScene("UserRegistration");
        else if(sceneName == "UserLogin" && gameObject.name == "ReturnButton") SceneManager.LoadScene("Title");
        else if(sceneName == "UserRegistration" && gameObject.name == "CreateButton")
        {

        }
        else if(sceneName == "UserRegistration" && gameObject.name == "ReturnButton") SceneManager.LoadScene("UserLogin");
    }
}
