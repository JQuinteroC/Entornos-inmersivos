using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject messageUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ClearScrean()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        messageUI.SetActive(false);
    }

    public void LoginScreen()
    {
        ClearScrean();
        loginUI.SetActive(true);
    }

    public void RegisterScreen()
    {
        ClearScrean();
        registerUI.SetActive(true);
    }

    public void MessageScreen()
    {
        messageUI.SetActive(true);
    }

    public void DisableMessage()
    {
        messageUI.SetActive(false);
    }
}