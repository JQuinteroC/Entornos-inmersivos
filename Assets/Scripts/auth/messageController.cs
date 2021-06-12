using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    public GameObject panelMessange;
    public Text textMessange;

    public void DisableMessange()
    {
        if (panelMessange != null)
        {
            panelMessange.SetActive(false);
            textMessange.text = "";
        }
    }
}
