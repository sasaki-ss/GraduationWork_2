using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;

    private bool isOpenMenu;

    private void Start()
    {
        isOpenMenu = false;
        ui.SetActive(false);
    }

    private void Update()
    {
        if(!isOpenMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            ui.SetActive(true);
            isOpenMenu = true;
            return;
        }

        if (isOpenMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            ui.SetActive(false);
            isOpenMenu = false;

            Debug.Log("ƒƒjƒ…[•Â‚¶‚Ü‚µ‚½");
        }
    }
}
